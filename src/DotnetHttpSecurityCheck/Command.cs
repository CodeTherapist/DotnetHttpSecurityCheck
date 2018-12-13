using CodeTherapy.HttpSecurityChecks.Report;
using CodeTherapy.HttpSecurityChecks.Services;
using DotnetHttpSecurityCheck.Report;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace DotnetHttpSecurityCheck
{
    [Command(
        Name = "http-security-check",
        Description = "A dotnet tool to do a http request/response security assessment.")]
    [HelpOption()]
    [VersionOptionFromMember(MemberName = nameof(GetVersion))]
    public sealed class HttpSecuityCheckCommand
    {

        public HttpSecuityCheckCommand(ISecurityCheckPipeline securityCheckPipline, IReporter reporter, UriHelper uriHelper)
        {
            SecurityCheckPipeline = securityCheckPipline ?? throw new ArgumentNullException(nameof(securityCheckPipline));
            Reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
            UriHelper = uriHelper ?? throw new ArgumentNullException(nameof(uriHelper));
        }

        [Argument(0, Description = "A absolute URL for the security assessment (required).")]
        [Required(AllowEmptyStrings = false)]
        public (bool HasValue, Uri Value) Url { get; set; }

        [Option(CommandOptionType.SingleOrNoValue,
            Template = "-o|--output <value>",
            Description = "The report output file path (optional).")]
        [LegalFilePath()]
        public (bool HasValue, string Value) ReportOutput { get; set; }

        [Option(CommandOptionType.SingleOrNoValue,
            Template = "-f|--format <value>",
            Description = "Format of the report (optional). Default is Text.")]
        public (bool HasValue, ReportFormat Value) ReportFormat { get; set; }

        [Option(CommandOptionType.SingleValue,
            Template = "-v|--verbose <value>",
            Description = "Set the console verbosity level (optional). Default is normal. Allowed values are n[normal], q[uiet], d[etailed].")]
        public VerbosityLevel Verbosity { get; set; }

        private IReporter Reporter { get; }

        private ISecurityCheckPipeline SecurityCheckPipeline { get; }

        private UriHelper UriHelper { get; }

        public int OnExecute()
        {
            if (ReportFormat.HasValue)
            {
                if (!ReportOutput.HasValue)
                {
                    Reporter.Error("Option 'output' is required when the report format is not the console.");
                    return 1;
                }
            }
            
            if (Url.HasValue)
            {
                Uri uri = Url.Value;
                string message;
                if (UriHelper.ValidateUri(uri, out message))
                {
                    Reporter.Verbose($"Starting assessment for {uri.AbsoluteUri}.");
                    var result = SecurityCheckPipeline.Run(uri, afterRequest: OnAfterRequest);
                    var securityCheckReportWriter = CreateReportWriter();
                    
                    Reporter.Verbose("Generating report.");
                    securityCheckReportWriter.Write(result);
                    if (ReportOutput.HasValue && ReportFormat.HasValue)
                    {
                        Reporter.Output($"Report written to '{Path.GetFullPath(ReportOutput.Value)}'.");
                    }
                    return 0;
                }
                else
                {
                    Reporter.Error(message);
                }
            }

            return 1;
        }


        private void OnAfterRequest(HttpResponseMessage response)
        {
            var request = response.RequestMessage;
            var message = $"GET {request.RequestUri.AbsoluteUri} {(int)response.StatusCode}: {response.ReasonPhrase}";
            if (response.IsSuccessStatusCode)
            {
                Reporter.Output(message);
            }
            else
            {
                Reporter.Error(message);
            }
        }

        private IHttpSecurityCheckReportWriter CreateReportWriter()
        {
            if (ReportOutput.HasValue)
            {
                var filePath = ReportOutput.Value;
                switch (ReportFormat.Value)
                {
                    case DotnetHttpSecurityCheck.ReportFormat.Json:
                        return new JsonHttpSecurityCheckReportWriter(filePath);
                    case DotnetHttpSecurityCheck.ReportFormat.Text:
                    default:
                        return new TextHttpSecurityCheckReportWriter(filePath);
                }
            }
            return new ConsoleSecurityCheckReportWriter(PhysicalConsole.Singleton);
        }


        private static string GetVersion() => typeof(Program)
                                .Assembly
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                .InformationalVersion;


    }

}
