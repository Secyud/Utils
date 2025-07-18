namespace Secyud.EntityFrameworkCore.Bulker;

public class SqlServerBulkSqlBuilder : BulkSqlBuilderBase
{
    public override string GetTableName(ITableInfo table)
    {
        return $"{table.Schema}{(table.Schema is null ? "" : ".")}[{table.TableName}]";
    }

    public override string GetColumnName(IColumnInfo column)
    {
        return $"[{column.ColumnName}]";
    }
}