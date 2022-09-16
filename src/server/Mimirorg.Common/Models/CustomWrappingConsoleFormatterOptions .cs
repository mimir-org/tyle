using Microsoft.Extensions.Logging.Console;

namespace Mimirorg.Common.Models
{
    public class CustomWrappingConsoleFormatterOptions : ConsoleFormatterOptions
    {
        public string CustomPrefix { get; set; }
        public string CustomSuffix { get; set; }
    }
}