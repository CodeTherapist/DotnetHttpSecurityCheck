using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class XXSSProtectionSecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<XXSSProtectionSecurityHeaderCheck>
    {
        public override string HeaderName => "x-xss-protection";
        
        [Theory]
        [InlineData("1; mode=block", SecurityCheckState.Best)]
        [InlineData("1", SecurityCheckState.Good)]
        [InlineData("1; report=http://example.com/report_URI", SecurityCheckState.Bad)]
        [InlineData("0", SecurityCheckState.Bad)]
        public void CheckValidValues(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }
    }
    
}
