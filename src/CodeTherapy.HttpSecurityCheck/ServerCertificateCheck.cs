using System;
using System.Net.Http;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class ServerCertificateCheck : HttpSecurityCheckBase
    {
        public override string Name => "Server Certificate";

        public override string Description => "Checks for server certificate validation errors.";

        public override string Category => "Request";

        public override bool HttpsOnly => true;

        public override string Recommendation => "Recommended is a valid certificate that has no errors.";

        protected override SecurityCheckResult CheckCore(HttpResponseMessage httpResponseMessage)
        {
            var request = httpResponseMessage.RequestMessage;

            if (request.Properties.TryGetValue(nameof(ServerCertificateInfo), out object obj))
            {
                var serverCertificateValidation = (ServerCertificateInfo)obj;

                if (serverCertificateValidation.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                {
                    return SecurityCheckResult.Create(SecurityCheckState.Best, value: serverCertificateValidation.X509CertificateName);
                }
                else
                {
                    return SecurityCheckResult.Create(SecurityCheckState.Bad, $"The server certificate has '{serverCertificateValidation.SslPolicyErrors}'. Ensure that the certificate has no erros.", serverCertificateValidation.X509CertificateName);
                }

            }
            else
            {
                throw new InvalidOperationException($"No {nameof(ServerCertificateInfo)} found on the request.");
            }
        }
    }
}
