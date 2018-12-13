using System.Collections.Generic;
using System.Linq;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public abstract class MissingHeaderValueSecurityCheckBase : SingleHeaderValueSecurityCheck
    {
        public abstract IReadOnlyCollection<HeaderValueCheck> HeaderValueChecks { get; }

        
        protected override SecurityCheckResult CheckHeaderValue(string value)
        {
            var trimedValue = value.Trim();
            var headerValueCheck = HeaderValueChecks.FirstOrDefault(check => check.Matches(trimedValue));

            if (!(headerValueCheck is null))
            {
                return SecurityCheckResult.Create(headerValueCheck.SecurityCheckState, headerValueCheck.Recommendation, value);
            }
            else
            {
                return CreateForValue(value);
            }
        }

        protected override SecurityCheckResult CreateForMissingHeader()
        {
            return SecurityCheckResult.Create(SecurityCheckState.Bad, $"The header {HeaderName} is missing. {Recommendation}");
        }

        protected virtual SecurityCheckResult CreateForValue(string value)
        {
            return SecurityCheckResult.Create(SecurityCheckState.Bad, $"Invalid or unknown value for {HeaderName}. {Recommendation}", value);
        }
    }


}
