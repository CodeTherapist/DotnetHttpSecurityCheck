using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class StrictTransportSecuritySecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<StrictTransportSecuritySecurityHeaderCheck>
    {
        public override string HeaderName => "strict-transport-security";
        
        [Theory]
        [InlineData("max-age=31536000; includeSubDomains", SecurityCheckState.Best)]
        [InlineData("max-age=31536000", SecurityCheckState.Good)]
        [InlineData("max-age=31536000; preload", SecurityCheckState.Good)]
        public void CheckValidValues(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }

    }


}
