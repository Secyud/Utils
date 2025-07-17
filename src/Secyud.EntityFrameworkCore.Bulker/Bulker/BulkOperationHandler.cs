using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Secyud.Utils.EntityFrameworkCore.Exceptions;
using Secyud.Utils.EntityFrameworkCore.Options;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public class BulkOperationHandler(IOptions<BulkOptions> options) : IBulkOperationHandler
{
    protected virtual IBulkOperationAdapter GetAdapter(DbContext context)
    {
        var adapter = options.Value.Handlers.FirstOrDefault(u => u.IsThisAdapter(context));
        if (adapter is null)
            throw new BulkOperationAdapterNotRegisteredException(context);

        return adapter;
    }

    public Task InsertManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return GetAdapter(context).InsertManyAsync(context, entities, cancellationToken);
    }

    public Task UpdateManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return GetAdapter(context).UpdateManyAsync(context, entities, cancellationToken);
    }

    public Task DeleteManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return GetAdapter(context).DeleteManyAsync(context, entities, cancellationToken);
    }
}