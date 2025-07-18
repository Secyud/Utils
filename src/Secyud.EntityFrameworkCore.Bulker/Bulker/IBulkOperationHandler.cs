using Microsoft.EntityFrameworkCore;

namespace Secyud.EntityFrameworkCore.Bulker;

public interface IBulkOperationHandler
{
    Task InsertManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task UpdateManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    Task DeleteManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
}