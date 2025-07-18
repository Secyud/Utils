using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Secyud.EntityFrameworkCore.Options;

namespace Secyud.EntityFrameworkCore.Bulker;

public class BulkOperationHandler(IOptions<BulkOptions> options, IServiceProvider provider) : IBulkOperationHandler
{
    protected virtual IBulkOperationAdapter GetAdapter(DbContext context)
    {
        var services = options.Value.Adapters.Select(provider.GetRequiredService);
        foreach (var service in services)
        {
            if (service is IBulkOperationAdapter adapter &&
                adapter.IsThisAdapter(context))
                return adapter;
        }

        throw new InvalidOperationException($"no adaptor is fit for context {context.Database.ProviderName}.");
    }

    public Task InsertManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        return GetAdapter(dbContext).InsertManyAsync(dbContext, entities, cancellationToken);
    }

    public Task UpdateManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        return GetAdapter(dbContext).UpdateManyAsync(dbContext, entities, cancellationToken);
    }

    public Task DeleteManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        return GetAdapter(dbContext).DeleteManyAsync(dbContext, entities, cancellationToken);
    }
}