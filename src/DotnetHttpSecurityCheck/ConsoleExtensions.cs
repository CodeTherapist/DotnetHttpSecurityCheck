using McMaster.Extensions.CommandLineUtils;
using System;

namespace DotnetHttpSecurityCheck
{
    public static class ConsoleExtensions
    {

        public static void Write(this IConsole console, string value, ConsoleColor color)
        {
            WriteInternal(console, color, () => console.Write(value));
        }

        public static void WriteLine(this IConsole console, string value, ConsoleColor color)
        {
            WriteInternal(console, color, () => console.WriteLine(value));
        }
        public static void WriteIntent(this IConsole console, int intent = 2)
        {
            console.Write(new string(' ', intent));
        }

        public static void Write(this IConsole console, string value, ConsoleColor? color)
        {
            ConsoleColor? tempColor = null;
            if (color.HasValue)
            {
                tempColor = console.ForegroundColor;
                console.ForegroundColor = color.Value;
            }
            console.Write(value);

            if (tempColor.HasValue)
            {
                console.ForegroundColor = tempColor.Value;
            }
        }

        public static void WriteInternal(IConsole console, ConsoleColor foregroundColor, Action action)
        {
            ConsoleColor tempColor = console.ForegroundColor;
            console.ForegroundColor = foregroundColor;
            action();
            console.ForegroundColor = tempColor;
        }


    }

}
