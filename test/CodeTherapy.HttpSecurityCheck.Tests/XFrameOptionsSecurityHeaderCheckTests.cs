using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class XFrameOptionsSecurityHeaderCheckTests : MissingHeaderSecurityCheckTestBase<XFrameOptionsSecurityHeaderCheck>
    {
        public override string HeaderName => "x-frame-options";

        [Theory]
        [InlineData("deny", SecurityCheckState.Best)]
        [InlineData("sameorigin", SecurityCheckState.Good)]
        [InlineData("allow-from https://example.com/", SecurityCheckState.Good)]
        [InlineData("", SecurityCheckState.Bad)]
        [InlineData("A invalid value", SecurityCheckState.Bad)]
        public void CheckValue(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }
        
    }
}
