using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public abstract class DisclosureHeaderSecurityCheckTestBase<TSecurityCheck> : HeaderSecurityCheckTestBase<TSecurityCheck> where TSecurityCheck : HttpHeaderCheckBase
    { 


        [Theory]
        [InlineData("Value", SecurityCheckState.Bad)]
        public void CheckInvalidValue(string value, SecurityCheckState securityCheckState)
        {
            AssertSecurityCheckState(HeaderName, value, securityCheckState);
        }

        [Fact]
        public void CheckNoValue()
        {
            AssertSecurityCheckStateWhenNoHeaderIsSet(HeaderName, SecurityCheckState.Best);
        }
    }
}
