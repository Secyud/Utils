using Microsoft.EntityFrameworkCore.Metadata;

namespace Secyud.Utils.EntityFrameworkCore.Options;

public class BulkEntityType(IEntityType type)
{
    private List<IProperty>? _properties;
    private List<IProperty>? _primaryKeys;
    public IEntityType Type { get; } = type;
    public List<IProperty> Properties => _properties ??= Type.GetProperties().ToList();
    public List<IProperty> PrimaryKeys => _primaryKeys ??= Properties.Where(u => u.IsPrimaryKey()).ToList();
}