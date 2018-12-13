using CodeTherapy.HttpSecurityChecks.Data;
using CodeTherapy.HttpSecurityChecks.Report;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DotnetHttpSecurityCheck
{
    public sealed class TextHttpSecurityCheckReportWriter : HttpSecurityCheckReportWriterBase
    {
        public TextHttpSecurityCheckReportWriter(string path)
            : this(() => new StreamWriter(path, append: false, Encoding.UTF8))
        {

        }

        public TextHttpSecurityCheckReportWriter(Func<TextWriter> textWriterFactoy)
        {
            TextWriterFactory = textWriterFactoy ?? throw new ArgumentNullException(nameof(textWriterFactoy));
        }

        private Func<TextWriter> TextWriterFactory { get; }

        protected override void WriteCore(SecurityCheckPiplineResult securityCheckExecutionResult)
        {
            using (var textWriter = TextWriterFactory())
            {
                textWriter.WriteLine(securityCheckExecutionResult.Url);
                textWriter.WriteLine(securityCheckExecutionResult.DateTime);
                textWriter.WriteLine("Report Summary");

                var maxNameLenght = securityCheckExecutionResult.Max(r => r.SecurityCheck.Name.Length);
                var groupedByType = securityCheckExecutionResult.GroupBy(r => r.SecurityCheck.Category);

                foreach (var group in groupedByType)
                {
                    textWriter.WriteLine();
                    textWriter.WriteLine($"> {group.Key} checks");
                    textWriter.WriteLine();
                    foreach (var item in group)
                    {
                        Write(textWriter, item, maxNameLenght);
                    }
                }
                textWriter.Flush();

            }
  
        }

        private void Write(TextWriter textWriter,  SecurityCheckExecutionResult executionResult, int nameIntent)
        {
            if (executionResult is null)
            {
                throw new ArgumentNullException(nameof(executionResult));
            }

            textWriter.WriteLine($"Check {executionResult.SecurityCheck.Name.PadRight(nameIntent)} : {executionResult.SecurityCheckResult.Value}");
            if (!executionResult.HasError)
            {
                var text = GetText(executionResult.SecurityCheckResult.State);
                textWriter.Write($"-> {text}");
                if (executionResult.SecurityCheckResult.HasRecommandation)
                {
                    textWriter.Write($": {executionResult.SecurityCheckResult.Recommandation}", ConsoleColor.White);
                }
            }
            else
            {
                textWriter.Write($"Exception occured! {Environment.NewLine}{executionResult.Exception.ToString()}", ConsoleColor.Yellow);
            }

            textWriter.WriteLine();
            textWriter.WriteLine();
        }

        

    }
}