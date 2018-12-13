using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class XPoweredByDisclosureHeaderSecurityCheckTests : DisclosureHeaderSecurityCheckTestBase<XPoweredByDisclosureHeaderSecurityCheck>
    {
        public override string HeaderName => "x-powered-by";


        [Fact]
        public void CheckEmptyValue()
        {
            AssertSecurityCheckState(HeaderName, string.Empty, SecurityCheckState.Best);
        }


    }
}
