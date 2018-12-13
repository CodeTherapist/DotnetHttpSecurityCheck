using CodeTherapy.HttpSecurityChecks;
using CodeTherapy.HttpSecurityChecks.Data;
using System;
using System.Net.Http;
using Xunit;

namespace CodeTherapy.HttpSecurityCheck.Tests
{
    public class SecurityCheckExecutionResultTests
    {
        public void MustThrowWhenSecurityCheckIsNull()
        {
           var ex = Record.Exception(() => new SecurityCheckExecutionResult(securityCheck: null, SecurityCheckResult.Empty));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        public void MustThrowWhenSecurityCheckResultIsNull()
        {
            var ex = Record.Exception(() => new SecurityCheckExecutionResult(new FakeSecurityCheck(), null));
            Assert.NotNull(ex);
            Assert.IsType<ArgumentNullException>(ex);
        }

        private class FakeSecurityCheck : ISecurityCheck
        {
            public string Name { get; }
            public string Description { get; }
            public string Category { get; }
            public string Recommendation { get; }
            public bool HttpsOnly { get; }

            public SecurityCheckResult Check(HttpResponseMessage httpRequestMessage)
            {
                throw new NotImplementedException();
            }
        }

    }
}
