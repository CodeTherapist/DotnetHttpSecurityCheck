using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeTherapy.HttpSecurityChecks.Data
{
    public sealed class SecurityCheckPiplineResult : IEnumerable<SecurityCheckExecutionResult>
    {
        public static SecurityCheckPiplineResult Empty => new SecurityCheckPiplineResult();

        public SecurityCheckPiplineResult()
        {
            Results = new List<SecurityCheckExecutionResult>();
        }

        public DateTimeOffset DateTime { get; set; }
        
        public string Url { get; set; }

        private List<SecurityCheckExecutionResult> Results { get; }
        
        public int Count => Results.Count;


        public void Add(ISecurityCheck securityCheck, SecurityCheckResult info)
        {
            Results.Add(new SecurityCheckExecutionResult(securityCheck, info));
        }

        public void Add(ISecurityCheck securityCheck, Exception exception)
        {
            Results.Add(new SecurityCheckExecutionResult(securityCheck, SecurityCheckResult.Empty, exception));
        }

        public IEnumerator<SecurityCheckExecutionResult> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
