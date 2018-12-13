using System.Net.Http;
using CodeTherapy.HttpSecurityChecks.Data;

namespace CodeTherapy.HttpSecurityChecks
{
    public interface ISecurityCheck
    {
        string Name { get; }

        string Description { get; }

        string Category { get; }

        string Recommendation { get; }

        bool HttpsOnly { get; }

        SecurityCheckResult Check(HttpResponseMessage httpRequestMessage);
        
    }
}
