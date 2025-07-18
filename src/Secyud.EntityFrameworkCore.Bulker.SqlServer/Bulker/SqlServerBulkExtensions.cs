using Microsoft.Extensions.DependencyInjection;

namespace Secyud.EntityFrameworkCore.Bulker;

public static class SqlServerBulkExtensions
{
    public static void AddSqlServerBulkHandler(this IServiceCollection service, Action<SqlServerBulkOptions>? options = null)
    {
        service.AddBulkOperationAdapter<ISqlServerBulkOperationAdapter, SqlServerBulkOperationAdapter>();
        if (options is not null)
        {
            service.Configure(options);
        }
    }
}