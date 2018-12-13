using System.Net.Http.Headers;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public abstract class DisclosureHeaderValueSecurityCheck : SingleHeaderValueSecurityCheck
    {
        
        protected override SecurityCheckResult CheckHeaderValue(string value)
        {
            return SecurityCheckResult.Create(SecurityCheckState.Bad, Recommendation, value);
        }
        
        protected override SecurityCheckResult CreateForMissingHeader()
        {
            return SecurityCheckResult.Create(SecurityCheckState.Best);
        }

    }
}
