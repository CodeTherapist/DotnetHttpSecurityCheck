using CodeTherapy.HttpSecurityChecks.Data;
using System;

namespace CodeTherapy.HttpSecurityChecks.Report
{
    public interface IHttpSecurityCheckReportWriter
    {
        void Write(SecurityCheckPiplineResult securityCheckExecutionResults);
    }
}