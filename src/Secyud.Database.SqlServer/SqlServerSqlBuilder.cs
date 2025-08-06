namespace Secyud.Database;

public class SqlServerSqlBuilder : SqlBuilderBase
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