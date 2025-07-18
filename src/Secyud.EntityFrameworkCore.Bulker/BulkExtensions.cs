using Microsoft.Extensions.DependencyInjection;
using Secyud.EntityFrameworkCore.Bulker;
using Secyud.EntityFrameworkCore.Options;

namespace Secyud.EntityFrameworkCore;

public static class BulkExtensions
{
    public static void AddSecyudBulker(this IServiceCollection services, Action<BulkOptions>? options = null)
    {
        if (options is not null)
        {
            services.Configure(options);
        }

        services.AddTransient<IBulkOperationHandler, BulkOperationHandler>();
    }

    public static void AddBulkOperationAdapter<TService, TImplementation>(this IServiceCollection services)
        where TImplementation : class, TService where TService : class, IBulkOperationAdapter
    {
        services.AddTransient<TService, TImplementation>();

        services.Configure<BulkOptions>(options =>
        {
            var type = typeof(TService);
            if (!options.Adapters.Contains(type))
                options.Adapters.Add(typeof(TService));
        });
    }

    public static void AddBulkOperationAdapter<TService>(this IServiceCollection services)
        where TService : class, IBulkOperationAdapter
    {
        services.AddBulkOperationAdapter<TService, TService>();
    }
}