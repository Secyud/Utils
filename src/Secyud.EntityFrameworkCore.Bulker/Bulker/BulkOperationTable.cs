using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Secyud.EntityFrameworkCore.Bulker;

public class BulkOperationTable : ITableInfo
{
    private List<BulkOperationColumn>? _columns;
    private List<BulkOperationNavigation>? _navigations;
    private List<BulkOperationColumn>? _primaryKeys;

    public BulkOperationTable(Type type, IEntityType entityType)
    {
        Type = type;
        EntityType = entityType;
        Schema = entityType.GetSchema();
        TableName = entityType.GetTableName() ??
                    throw new InvalidOperationException($"Cannot find table name for Type: {entityType.Name}");
        ObjectIdentifier = StoreObjectIdentifier.Table(TableName, Schema);
    }

    public Type Type { get; }
    public IEntityType EntityType { get; }

    public string? Schema { get; set; }
    public string TableName { get; set; }

    public StoreObjectIdentifier ObjectIdentifier { get; }

    public List<BulkOperationColumn> Columns
    {
        get
        {
            if (_columns is null)
            {
                _columns = [];
                foreach (var property in EntityType.GetProperties())
                {
                    var columnName = property.GetColumnName(ObjectIdentifier);
                    if (columnName is null) continue;
                    _columns.Add(new BulkOperationColumn(property, columnName));
                }
            }

            return _columns;
        }
    }

    public List<BulkOperationColumn> PrimaryKeys => _primaryKeys ??= Columns.Where(u => u.IsPrimaryKey).ToList();

    public List<BulkOperationNavigation> Navigations => _navigations ??=
        EntityType.GetNavigations().Select(u => new BulkOperationNavigation(u)).ToList();
}