using CodeTherapy.HttpSecurityChecks.Data;
using CodeTherapy.HttpSecurityChecks.Report;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DotnetHttpSecurityCheck.Report
{
    public sealed class JsonHttpSecurityCheckReportWriter : HttpSecurityCheckReportWriterBase
    {
        private readonly string _path;

        public JsonHttpSecurityCheckReportWriter(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("message", nameof(path));
            }

            _path = path;
        }
        

        protected override void WriteCore(SecurityCheckPiplineResult securityCheckExecutionResults)
        {
            var obj = JsonConvert.SerializeObject(new
            {
                Url = securityCheckExecutionResults.Url,
                DateTime = securityCheckExecutionResults.DateTime.ToString(),
                Results = securityCheckExecutionResults.Select(r => new
                {
                    CheckName = r.SecurityCheck.Name,
                    Category = r.SecurityCheck.Category,
                    HttpsOnly = r.SecurityCheck.HttpsOnly,
                    State = GetText(r.SecurityCheckResult.State),
                    Recommandation = r.SecurityCheckResult.Recommandation,
                    Value = r.SecurityCheckResult.Value,
                    HasError = r.HasError,
                    Error = r.Exception?.ToString()
                })
            }, new JsonSerializerSettings() {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None });

            File.WriteAllText(_path , obj);
        }
    }
}
