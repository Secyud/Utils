using Microsoft.Extensions.DependencyInjection;
using Secyud.Utils.EntityFrameworkCore.Options;

namespace Secyud.Utils.EntityFrameworkCore;

public static class BulkExtensions
{
    public static void AddSqlServerBulkHandler(this IServiceCollection services, Action<BulkOptions>? options = null)
    {
        if (options is not null)
        {
            services.Configure(options);
        }
    }
}