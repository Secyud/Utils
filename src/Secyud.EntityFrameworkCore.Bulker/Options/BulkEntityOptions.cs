namespace Secyud.Utils.EntityFrameworkCore.Options;

public class BulkEntityOptions(Type type)
{
    public Type Type { get; } = type;
    public List<Func<object, IEnumerable<object>>> Navigation { get; } = [];
}