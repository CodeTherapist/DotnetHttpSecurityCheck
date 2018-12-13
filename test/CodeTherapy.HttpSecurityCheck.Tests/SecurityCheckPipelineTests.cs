using CodeTherapy.HttpSecurityChecks.Data;
using CodeTherapy.HttpSecurityChecks.Services;
using System;
using System.Net.Http;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class SecurityCheckPipelineTests
    {

        [Fact]
        public void RunDoesNotAcceptNull()
        {
            var pipeline = new SecurityCheckPipeline(Array.Empty<ISecurityCheck>());

            Assert.Throws<ArgumentNullException>(() => pipeline.Run(null));
        }

        [Fact]
        public void AnalyzeDoesNotAcceptNull()
        {
            var pipeline = new SecurityCheckPipeline(Array.Empty<ISecurityCheck>());
            Assert.Throws<ArgumentNullException>(() => pipeline.Analyze(null));
        }

        [Fact]
        public void NoSecurityChecksDoesNotThrow()
        {
            var pipeline = new SecurityCheckPipeline(Array.Empty<ISecurityCheck>());

            HttpResponseMessage response = CreateResponse();

            var result = pipeline.Analyze(response);
            Assert.DoesNotContain(result, r => r.HasError);
        }

        private static HttpResponseMessage CreateResponse()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.example.com");
            var response = new HttpResponseMessage() { RequestMessage = request };
            return response;
        }

        [Fact]
        public void HasExceptionFromSecurrityCheck()
        {
            var pipeline = new SecurityCheckPipeline(new[] { new ActionSecurityCheck((e) => throw new Exception())}); 
            var response = CreateResponse();

            var result = pipeline.Analyze(response);
            Assert.Contains(result, r => r.HasError);
        }


        [Theory]
        [InlineData("file:///c:/path/to/the%20file.txt")]
        [InlineData("/relative/uri")]
        [InlineData(@"\\Unc\path\")]
        public void UriCannotBeAFile(string uri)
        {
            var pipeline = new SecurityCheckPipeline(Array.Empty<ISecurityCheck>());
            var ex = Record.Exception(() => pipeline.Run(new Uri(uri, UriKind.RelativeOrAbsolute)));
            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
        }


        private class ActionSecurityCheck : ISecurityCheck
        {
            private readonly Func<HttpResponseMessage, SecurityCheckResult> _check;

            public ActionSecurityCheck(Func<HttpResponseMessage, SecurityCheckResult> check)
            {
                _check = check ?? throw new ArgumentNullException(nameof(check));
            }

            public string Name => "Name";
            public string Description => "Description";
            public string Category => "Category";
            public string Recommendation => "Recommendation";
            public bool HttpsOnly => false;

            public SecurityCheckResult Check(HttpResponseMessage httpRequestMessage)
            {
                return _check(httpRequestMessage);
            }
        }


    }

}
