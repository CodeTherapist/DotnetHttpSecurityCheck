using CodeTherapy.HttpSecurityChecks.Data;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public class HttpsSecurityHeaderCheckTests : SecurityCheckTestBase<HttpsCheck>
    {


        [Theory]
        [InlineData(true, SecurityCheckState.Best)]
        [InlineData(false, SecurityCheckState.Bad)]
        public void CheckState(bool https, SecurityCheckState expectedSecurityCheckState)
        {
            var result = RunCheck(https);
            Assert.Equal(expectedSecurityCheckState, result.State);
        }
    }
}
