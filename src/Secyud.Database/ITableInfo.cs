namespace Secyud.Database;

public interface ITableInfo
{
    string TableName { get; set; }
    string? Schema { get; set; }
}