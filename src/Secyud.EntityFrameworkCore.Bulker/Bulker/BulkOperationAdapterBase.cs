using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Secyud.EntityFrameworkCore.Options;

namespace Secyud.EntityFrameworkCore.Bulker;

public abstract class BulkOperationAdapterBase : IBulkOperationAdapter
{
    private readonly Lazy<ILogger> _logger;
    protected IOptions<BulkOptions> Options { get; }

    protected BulkOperationAdapterBase(IOptions<BulkOptions> options, ILoggerFactory loggerFactory)
    {
        Options = options;
        _logger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(GetType()));
    }

    protected ILogger Logger => _logger.Value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected BulkOperationContext CreateBulkContext<TEntity>(DbContext dbContext)
    {
        var type = typeof(TEntity);

        var entityType = dbContext.Model.FindEntityType(type);

        if (entityType is null)
        {
            throw new InvalidOperationException(
                $"DbContext does not contain EntitySet for Type: {type.Name}");
        }

        var table = new BulkOperationTable(type, entityType);
        var entityOptions = Options.Value.EntityOptionsBuilder.BuildOptions<TEntity>();
        var context = new BulkOperationContext(dbContext, table, entityOptions);

        return context;
    }

    protected virtual async Task InsertManyAsync<TEntity>(BulkOperationContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await Task.CompletedTask;
    }

    public virtual async Task InsertManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await dbContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var context = CreateBulkContext<TEntity>(dbContext);

            await InsertManyAsync(context, entities, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Bulk insert failed!");
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync().ConfigureAwait(false);
        }
    }

    protected virtual async Task UpdateManyAsync<TEntity>(BulkOperationContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await Task.CompletedTask;
    }

    public virtual async Task UpdateManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await dbContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var context = CreateBulkContext<TEntity>(dbContext);

            await UpdateManyAsync(context, entities, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Bulk insert failed!");
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync().ConfigureAwait(false);
        }
    }

    protected virtual async Task DeleteManyAsync<TEntity>(BulkOperationContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await Task.CompletedTask;
    }

    public virtual async Task DeleteManyAsync<TEntity>(DbContext dbContext, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class
    {
        await dbContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var context = CreateBulkContext<TEntity>(dbContext);

            await DeleteManyAsync(context, entities, cancellationToken);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Bulk insert failed!");
        }
        finally
        {
            await dbContext.Database.CloseConnectionAsync().ConfigureAwait(false);
        }
    }


    protected virtual async Task MergeTableAsync(BulkOperationContext context, BulkOperationTableInfo source,
        CancellationToken cancellationToken)
    {
        var (sql, parameters) = SqlMergeTable(context, source);
        await context.DbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }


    protected abstract (string sql, IEnumerable<object> parameters) SqlMergeTable(
        BulkOperationContext context, BulkOperationTableInfo source);



    public abstract bool IsThisAdapter(DbContext dbContext);
}