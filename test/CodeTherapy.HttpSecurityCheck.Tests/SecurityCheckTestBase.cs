using CodeTherapy.HttpSecurityChecks.Data;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace CodeTherapy.HttpSecurityChecks.Test
{
    public abstract class SecurityCheckTestBase<TSecurityCheck> where TSecurityCheck : class, ISecurityCheck
    {
        protected TSecurityCheck CreateSecurityCheck()
        {
            return Activator.CreateInstance<TSecurityCheck>();
        }



        [Fact]
        public void RecommendationIsNotNull()
        {
            var check = CreateSecurityCheck();
            Assert.NotNull(check.Recommendation);
        }

        [Fact]
        public void NameIsNotNull()
        {
            var check = CreateSecurityCheck();
            Assert.NotNull(check.Name);
        }


        [Fact]
        public void CategoryIsNotNull()
        {
            var check = CreateSecurityCheck();
            Assert.NotNull(check.Category);
        }


        [Fact]
        public void DescriptionIsNotNull()
        {
            var check = CreateSecurityCheck();
            Assert.NotNull(check.Description);
        }


        [Fact]
        public void CheckIsEquals()
        {
            var check1 = CreateSecurityCheck();
            var check2 = CreateSecurityCheck();

            Assert.NotSame(check1, check2);
            Assert.Equal(check1, check2);
        }

        [Fact]
        public void NullEquality()
        {
            var check1 = CreateSecurityCheck();
            Assert.False(check1.Equals(null));
        }

        [Fact]
        public void RefenceEquality()
        {
            var check1 = CreateSecurityCheck();
            var check2 = check1;
            Assert.True(check1.Equals(check2));
        }


        [Fact]
        public void ThrowsWhenParameterIsNull()
        {
            var check = CreateSecurityCheck();


           var exception = Record.Exception(() => check.Check(null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }



        protected SecurityCheckResult RunCheck(bool https = true)
        {
            var response = CreateResponseMessage(https);
            return RunCheck(response);
        }

        protected SecurityCheckResult RunCheck(HttpResponseMessage httpResponseMessage)
        {
            var securityCheck = CreateSecurityCheck();
            var result = securityCheck.Check(httpResponseMessage);
            return result;
        }


        protected HttpResponseMessage CreateResponseMessage(bool https = true)
        {
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, https ? "https://example.com" : "http://example.com")
            };

            return httpResponseMessage;
        }


    }



}
