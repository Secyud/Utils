using Serilog.Core;
using Serilog.Events;

namespace Secyud.Serilog.Sinks.PostgreSql;

public class PostgreSqlSink : IBatchedLogEventSink, IDisposable
{
    public void Dispose()
    {
    }

    public async Task EmitBatchAsync(IReadOnlyCollection<LogEvent> batch)
    {
    }
}