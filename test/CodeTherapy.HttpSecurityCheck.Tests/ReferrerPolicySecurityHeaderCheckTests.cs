using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class ReferrerPolicySecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<ReferrerPolicySecurityHeaderCheck>
    {
        public override string HeaderName => "referrer-policy";
        
        [Theory]
        [InlineData("strict-origin-when-cross-origin", SecurityCheckState.Best)]
        [InlineData("strict-origin", SecurityCheckState.Best)]
        [InlineData("no-referrer", SecurityCheckState.Best)]
        [InlineData("origin-when-cross-origin", SecurityCheckState.Good)]
        [InlineData("origin", SecurityCheckState.Good)]
        [InlineData("no-referrer-when-downgrade", SecurityCheckState.Good)]
        [InlineData("same-origin", SecurityCheckState.Good)]
        [InlineData("unsafe-url", SecurityCheckState.Bad)]
        public void CheckValidValues(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }
    }
    
}
