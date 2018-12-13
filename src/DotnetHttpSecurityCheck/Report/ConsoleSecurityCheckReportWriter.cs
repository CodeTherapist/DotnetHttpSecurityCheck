using CodeTherapy.HttpSecurityChecks.Data;
using CodeTherapy.HttpSecurityChecks.Report;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;

namespace DotnetHttpSecurityCheck
{
    public sealed class ConsoleSecurityCheckReportWriter : HttpSecurityCheckReportWriterBase
    {
        public ConsoleSecurityCheckReportWriter(IConsole console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console));
        }

        private IConsole Console { get; }

        protected override void WriteCore(SecurityCheckPiplineResult securityCheckExecutionResult)
        {
            Console.WriteLine();
            Console.WriteIntent(2);
            Console.WriteLine("Report Summary");

            var checkNameColumnSize = securityCheckExecutionResult.Max(s => s.SecurityCheck.Name.Length);
            var valueColumnSize = securityCheckExecutionResult.Max(s => s.SecurityCheckResult.Value.Length);

            var groupedByType = securityCheckExecutionResult.GroupBy(r => r.SecurityCheck.Category);

            foreach (var group in groupedByType)
            {
                Console.WriteLine();
                Console.WriteIntent(4);
                Console.WriteLine($"> {group.Key} checks");
                Console.WriteLine();
                foreach (var item in group)
                {
                    Write(item, valueColumnSize, checkNameColumnSize);
                }
            }
        }

        private void Write(SecurityCheckExecutionResult executionResult, int typeIntent, int nameIntent)
        {
            if (executionResult is null)
            {
                throw new ArgumentNullException(nameof(executionResult));
            }

            Console.WriteIntent(8);
            Console.WriteLine($"Check {executionResult.SecurityCheck.Name.PadRight(nameIntent)} : {executionResult.SecurityCheckResult.Value}");
            Console.WriteIntent(8);
            if (!executionResult.HasError)
            {
                var (Text, Color) = GetTextAndColor(executionResult.SecurityCheckResult.State);
                Console.WriteIntent(6 + nameIntent);
                Console.Write($" -> {Text}", Color);
                if (executionResult.SecurityCheckResult.HasRecommandation)
                {
                    Console.Write($": {executionResult.SecurityCheckResult.Recommandation}", ConsoleColor.White);
                }
            }
            else
            {
                Console.Write($"Exception occured! {Environment.NewLine}{executionResult.Exception.ToString()}", ConsoleColor.Yellow);
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private (string Text, ConsoleColor Color) GetTextAndColor(SecurityCheckState securityCheckState)
        {
            switch (securityCheckState)
            {
                case SecurityCheckState.None:
                    return (GetText(securityCheckState), Console.ForegroundColor);
                case SecurityCheckState.Skipped:
                    return (GetText(securityCheckState), ConsoleColor.Blue);
                case SecurityCheckState.Bad:
                    return (GetText(securityCheckState), ConsoleColor.Red);
                case SecurityCheckState.Good:
                    return (GetText(securityCheckState), ConsoleColor.Green);
                case SecurityCheckState.Best:
                    return (GetText(securityCheckState), ConsoleColor.Green);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}