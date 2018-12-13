using System;

namespace CodeTherapy.HttpSecurityChecks.Data
{
    public sealed class SecurityCheckExecutionResult
    {
        public SecurityCheckExecutionResult(ISecurityCheck securityCheck, SecurityCheckResult securityCheckInfo, Exception exception = null)
        {
            SecurityCheck = securityCheck ?? throw new ArgumentNullException(nameof(securityCheck));
            SecurityCheckResult = securityCheckInfo ?? throw new ArgumentNullException(nameof(securityCheckInfo));
            Exception = exception;
        }

        public ISecurityCheck SecurityCheck { get; }

        public SecurityCheckResult SecurityCheckResult { get; }
 
        public bool HasError => !(Exception is null);

        public Exception Exception { get; }
    }
}
