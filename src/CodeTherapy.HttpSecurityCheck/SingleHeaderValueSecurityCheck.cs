using System.Linq;
using System.Net.Http.Headers;
using CodeTherapy.HttpSecurityChecks.Core;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public abstract class SingleHeaderValueSecurityCheck : HttpHeaderCheckBase
    {

        protected override SecurityCheckResult CheckHeader(HttpResponseHeaders headers)
        {
            if (headers.TryGetHeaderValue(HeaderName, out string value))
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return CheckHeaderValue(value);
                }
            }
            return CreateForMissingHeader();
        }

        protected abstract SecurityCheckResult CheckHeaderValue(string value);

        protected abstract SecurityCheckResult CreateForMissingHeader();

        
    }


}
