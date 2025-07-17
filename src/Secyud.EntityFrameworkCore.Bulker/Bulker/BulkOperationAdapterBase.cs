using Microsoft.EntityFrameworkCore;
using Secyud.Utils.EntityFrameworkCore.Exceptions;
using Secyud.Utils.EntityFrameworkCore.Options;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public abstract class BulkOperationAdapterBase : IBulkOperationAdapter
{
    protected virtual Task SaveChangesAsync(DbContext context, CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public virtual Task InsertManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return SaveChangesAsync(context, cancellationToken);
    }

    public virtual Task UpdateManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return SaveChangesAsync(context, cancellationToken);
    }

    public virtual Task DeleteManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        return SaveChangesAsync(context, cancellationToken);
    }


    public abstract bool IsThisAdapter(DbContext context);
}