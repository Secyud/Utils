using Secyud.Utils.EntityFrameworkCore.Bulks;

namespace Secyud.Utils.EntityFrameworkCore.Options;

public class BulkOptions
{
    public List<IBulkOperationAdapter> Handlers { get; } = [];

    public BulkEntityOptionsBuilder EntityOptionsBuilder { get; } = new();
}