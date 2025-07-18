namespace Secyud.EntityFrameworkCore.Bulker;

public class BulkOperationTableInfo : ITableInfo
{
    public string? Schema { get; set; }
    public required string TableName { get; set; }
    
    public int EntitiesCount { get; set; }
}