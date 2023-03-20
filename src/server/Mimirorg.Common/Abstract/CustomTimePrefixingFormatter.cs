using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;

namespace Mimirorg.Common.Abstract;

public sealed class CustomTimePrefixingFormatter : ConsoleFormatter, IDisposable
{
    private readonly IDisposable _optionsReloadToken;
    private CustomWrappingConsoleFormatterOptions _formatterOptions;

    public CustomTimePrefixingFormatter(IOptionsMonitor<CustomWrappingConsoleFormatterOptions> options) : base(nameof(CustomTimePrefixingFormatter)) =>
        (_optionsReloadToken, _formatterOptions) = (options.OnChange(ReloadLoggerOptions), options.CurrentValue);

    private void ReloadLoggerOptions(CustomWrappingConsoleFormatterOptions options) => _formatterOptions = options;

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
    {
        if (logEntry.Formatter != null)
        {
            var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
            WritePrefix(textWriter, logEntry.LogLevel);
            textWriter.Write(message);
        }

        WriteSuffix(textWriter);
    }

    private void WritePrefix(TextWriter textWriter, LogLevel logLevel)
    {
        var now = _formatterOptions.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
        textWriter.Write($"{_formatterOptions.CustomPrefix} # [{logLevel} {now.ToString(_formatterOptions.TimestampFormat)}]");
    }

    private void WriteSuffix(TextWriter textWriter) => textWriter.WriteLine($" {_formatterOptions.CustomSuffix}");

    public void Dispose() => _optionsReloadToken?.Dispose();
}