using CodeTherapy.HttpSecurityChecks.Data;
using System.Net;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public abstract class HeaderSecurityCheckTestBase<TSecurityCheck> : SecurityCheckTestBase<TSecurityCheck> where TSecurityCheck : HttpHeaderCheckBase
    {

        public abstract string HeaderName { get; }


        [Fact]
        public void DoesNotThrow()
        {
            var exception = Record.Exception(() => RunCheck());
            Assert.Null(exception);
        }

        [Fact]
        public void HeaderNameIsNotNull()
        {
            var check = CreateSecurityCheck();
            Assert.NotNull(check.HeaderName);
        }

        

        protected void AssertSecurityCheckState(string headerName, string value, SecurityCheckState securityCheckState)
        {
            var securityCheck = CreateSecurityCheck();
            var response = CreateResponseMessage();
            response.Headers.Add(headerName, value);
            var result = securityCheck.Check(response);
            Assert.True(result.State == securityCheckState, $"{nameof(securityCheckState)}:{result.State}");
        }

        protected void AssertSecurityCheckStateWhenNoHeaderIsSet(string headerName, SecurityCheckState securityCheckState = SecurityCheckState.Bad)
        {
            var securityCheck = CreateSecurityCheck();
            var response = CreateResponseMessage();
            var result = securityCheck.Check(response);
            Assert.True(result.State == securityCheckState, $"{nameof(securityCheckState)}:{result.State}");
        }




    }
}
