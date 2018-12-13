using CodeTherapy.HttpSecurityChecks;
using CodeTherapy.HttpSecurityChecks.Services;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;

namespace DotnetHttpSecurityCheck
{
    internal sealed class Program
    {
        static Program()
        {
        }

        public static int Main(string[] args)
        {
            using (var services = new ServiceCollection()
                    .AddSingleton<IConsole, PhysicalConsole>()
                    .AddSingleton<IReporter>(provider => new ConsoleReporter(provider.GetService<IConsole>()))
                    .AddSingleton<ISecurityCheckPipeline, SecurityCheckPipeline>()
                    .AddSingleton<UriHelper>()

                    .AddSingleton<ISecurityCheck, XContentTypeOptionsSecurityHeaderCheck>()
                    .AddSingleton<ISecurityCheck, XFrameOptionsSecurityHeaderCheck>()
                    .AddSingleton<ISecurityCheck, XXSSProtectionSecurityHeaderCheck>()
                    .AddSingleton<ISecurityCheck, ReferrerPolicySecurityHeaderCheck>()
                    .AddSingleton<ISecurityCheck, ContentSecurityPolicySecurityHeaderCheck>()
                    .AddSingleton<ISecurityCheck, StrictTransportSecuritySecurityHeaderCheck>()

                    .AddSingleton<ISecurityCheck, XPoweredByDisclosureHeaderSecurityCheck>()
                    .AddSingleton<ISecurityCheck, ServerDisclosureHeaderValueSecurityCheck>()

                    .AddSingleton<ISecurityCheck, ServerCertificateCheck>()
                    .AddSingleton<ISecurityCheck, HttpsCheck>()
                    .BuildServiceProvider())
            {
                var app = new CommandLineApplication<HttpSecuityCheckCommand>();

                app.Conventions
                    .UseDefaultConventions()
                    .UseConstructorInjection(services);

                app.ThrowOnUnexpectedArgument = false;
                app.ExtendedHelpText = GetExtendedHelpText(services);

                return app.Execute(args);
            }
        }

        private static string GetExtendedHelpText(IServiceProvider serviceProvider)
        {
            var checks = serviceProvider.GetServices<ISecurityCheck>();

            if (checks.Any())
            {
                var stringBuilder = new StringBuilder();
                stringBuilder
                    .AppendLine()
                    .AppendLine("All Security Checks:")
                    .AppendLine();

                foreach (var group in checks.GroupBy(c => c.Category))
                {
                    stringBuilder.AppendLine($"  > {group.Key}").AppendLine();
                    var i = 0;
                    foreach (var item in group)
                    {
                        i++;
                        var s = $"    {i}# ";
                        stringBuilder.Append(s).AppendLine($"{item.Name}: {item.Description}");
                        stringBuilder.Append(new string(' ', s.Length)).AppendLine($"{item.Recommendation}").AppendLine();
                    }
                }

                return stringBuilder.ToString();
            }
            return string.Empty;
        }
    }
}
