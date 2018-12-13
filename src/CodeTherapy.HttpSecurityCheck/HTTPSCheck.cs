using System;
using System.Net.Http;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public sealed class HttpsCheck : HttpSecurityCheckBase
    {
        public override string Name => "HTTPS";

        public override string Description => "Checks that the request/response is HTTPS.";

        public override string Category => "Request";

        public override string Recommendation => "Recommended is to use HTTPS.";

        protected override SecurityCheckResult CheckCore(HttpResponseMessage httpResponseMessage)
        {
            if (string.Equals(httpResponseMessage.RequestMessage.RequestUri.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            {
                return SecurityCheckResult.Create(SecurityCheckState.Best, string.Empty, httpResponseMessage.RequestMessage.RequestUri.Scheme);
            }
            return SecurityCheckResult.Create(SecurityCheckState.Bad, Recommendation, Uri.UriSchemeHttp);
        }
    }
}
