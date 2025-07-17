using Microsoft.Extensions.DependencyInjection;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public static class SqlServerBulkExtensions
{
    public static void AddSqlServerBulkHandler(this IServiceCollection service, Action<SqlServerBulkOptions>? options = null)
    {
        
    }
}