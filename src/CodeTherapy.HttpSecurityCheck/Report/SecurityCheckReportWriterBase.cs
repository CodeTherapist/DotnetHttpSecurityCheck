using CodeTherapy.HttpSecurityChecks.Data;
using System;

namespace CodeTherapy.HttpSecurityChecks.Report
{
    public abstract class HttpSecurityCheckReportWriterBase : IHttpSecurityCheckReportWriter
    {
        public HttpSecurityCheckReportWriterBase()
        {
        }

        public void Write(SecurityCheckPiplineResult securityCheckExecutionResults)
        {
            if (securityCheckExecutionResults is null)
            {
                throw new ArgumentNullException(nameof(securityCheckExecutionResults));
            }
            WriteCore(securityCheckExecutionResults);
        }

        protected abstract void WriteCore(SecurityCheckPiplineResult securityCheckExecutionResults);
        
        protected string GetText(SecurityCheckState securityCheckState)
        {
            switch (securityCheckState)
            {
                case SecurityCheckState.None:
                    return "None";
                case SecurityCheckState.Skipped:
                    return "Skipped";
                case SecurityCheckState.Bad:
                    return "Bad";
                case SecurityCheckState.Good:
                    return "Good";
                case SecurityCheckState.Best:
                    return "Best Practice";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}