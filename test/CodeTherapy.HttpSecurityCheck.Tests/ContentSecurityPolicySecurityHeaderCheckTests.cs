using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class ContentSecurityPolicySecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<ContentSecurityPolicySecurityHeaderCheck>
    {
        public override string HeaderName => "content-security-policy";

        [Theory]
        [InlineData("default-src 'self'", SecurityCheckState.Best)]
        [InlineData("default-src 'self'; script-src https://example.com", SecurityCheckState.Best)]
        public void CheckValue(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }


    }
}
