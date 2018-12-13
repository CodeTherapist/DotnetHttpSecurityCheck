using CodeTherapy.HttpSecurityChecks.Data;
using System;
using System.Net.Http;

namespace CodeTherapy.HttpSecurityChecks.Services
{
    public interface ISecurityCheckPipeline
    {
        SecurityCheckPiplineResult Analyze(HttpResponseMessage httpResponse);

        SecurityCheckPiplineResult Run(Uri uri, Action<HttpRequestMessage> beforeRequest = null, Action<HttpResponseMessage> afterRequest = null);

    }
}