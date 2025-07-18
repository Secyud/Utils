using Secyud.EntityFrameworkCore.Bulker;

namespace Secyud.EntityFrameworkCore.Options;

public class BulkOptions
{
    public List<Type> Adapters { get; } = [];

    public BulkEntityOptionsBuilder EntityOptionsBuilder { get; } = new();
}