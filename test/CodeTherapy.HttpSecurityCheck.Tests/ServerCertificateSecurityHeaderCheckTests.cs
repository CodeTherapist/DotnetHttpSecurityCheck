using CodeTherapy.HttpSecurityChecks.Data;
using CodeTherapy.HttpSecurityChecks.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class ServerCertificateSecurityHeaderCheckTests : SecurityCheckTestBase<ServerCertificateCheck>
    {


        [Theory]
        [InlineData(SslPolicyErrors.None, SecurityCheckState.Best)]
        [InlineData(SslPolicyErrors.RemoteCertificateChainErrors, SecurityCheckState.Bad)]
        [InlineData(SslPolicyErrors.RemoteCertificateNameMismatch, SecurityCheckState.Bad)]
        [InlineData(SslPolicyErrors.RemoteCertificateNotAvailable, SecurityCheckState.Bad)]
        public void NoCertificateError(SslPolicyErrors sslPolicyErrors, SecurityCheckState expected)
        {
            var response = CreateResponseMessage();
            AddServerCertificateInfo(response, sslPolicyErrors);

            var result = RunCheck(response);
            Assert.Equal(expected, result.State);
        }

        private static void AddServerCertificateInfo(HttpResponseMessage response, SslPolicyErrors sslPolicyErrors)
        {
            response.RequestMessage.Properties[nameof(ServerCertificateInfo)]
                            = new ServerCertificateInfo("X509CertificateName",
                            X509Chain.Create(),
                            sslPolicyErrors);
        }


        [Theory, Trait("Category", "Integration")]
        [InlineData("https://expired.badssl.com/", SecurityCheckState.Bad)]
        [InlineData("https://wrong.host.badssl.com/", SecurityCheckState.Bad)]
        [InlineData("https://self-signed.badssl.com/", SecurityCheckState.Bad)]
        [InlineData("https://untrusted-root.badssl.com/", SecurityCheckState.Bad)]
        [InlineData("https://revoked.badssl.com/", SecurityCheckState.Bad)]
        public void BadSSLTests(string uri, SecurityCheckState expected)
        {
            var pipeline = new SecurityCheckPipeline(new[] { CreateSecurityCheck() });
            var result = pipeline.Run(new System.Uri(uri));
            Assert.Equal(expected, result.First().SecurityCheckResult.State);
        }
        
        [Fact]
        public void NoServerCertificateInfoThrowsException()
        {
            var response = CreateResponseMessage();
            var ex = Record.Exception(() => RunCheck(response));
            Assert.NotNull(ex);
            Assert.IsType<InvalidOperationException>(ex);
        }
    }
}
