namespace Secyud.EntityFrameworkCore.Bulker;

public interface ITableInfo
{
    string TableName { get; set; }
    string? Schema { get; set; }
}