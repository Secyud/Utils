using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Secyud.Utils.EntityFrameworkCore.Exceptions;

namespace Secyud.Utils.EntityFrameworkCore.Options;

public class BulkOperationContext(DbContext context, IEntityType entityType)
{
    public DbContext DbContext { get; } = context;

    private readonly Dictionary<Type, BulkEntityType> _entityTypes = [];

    public BulkEntityType GetEntityType<TEntity>()
    {
        return GetEntityType(typeof(TEntity));
    }

    public BulkEntityType GetEntityType(Type type)
    {
        if (!_entityTypes.TryGetValue(type, out var bulkEntityType))
        {
            var entityType = DbContext.Model.FindEntityType(type);
            if (entityType is null)
                throw new EfCoreModelException($"Model {type} is not configured in context {DbContext}.");
            bulkEntityType = new BulkEntityType(entityType);
            _entityTypes[type] = bulkEntityType;
        }

        return bulkEntityType;
    }
}