using Microsoft.EntityFrameworkCore;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public interface IBulkOperationHandler
{
    Task InsertManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task UpdateManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task DeleteManyAsync<TEntity>(DbContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
}