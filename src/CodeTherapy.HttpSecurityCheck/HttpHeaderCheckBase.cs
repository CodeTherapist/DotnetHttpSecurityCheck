using System.Net.Http;
using System.Net.Http.Headers;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public abstract class HttpHeaderCheckBase : HttpSecurityCheckBase
    {
        public abstract string HeaderName { get; }

        public override string Category => "Header";

        public override string Description => $"Checks the response header value of the '{HeaderName}' header.";
        
        protected override SecurityCheckResult CheckCore(HttpResponseMessage httpResponseMessage)
        {
            return CheckHeader(httpResponseMessage.Headers);
        }

        protected abstract SecurityCheckResult CheckHeader(HttpResponseHeaders headers);
    }
}
