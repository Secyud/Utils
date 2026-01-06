using System.Text;

namespace Secyud.Database;

public abstract class SqlBuildHelperBase
{
    public virtual void AppendColumns<TColumnInfo>(
        StringBuilder sb,
        List<TColumnInfo> columns,
        string? prefixTable = null,
        string? equalsTable = null,
        string splitString = ", ")
        where TColumnInfo : IColumnInfo
    {
        var appendComma = false;
        foreach (var column in columns)
        {
            if (appendComma)
                sb.Append(splitString);
            else
                appendComma = true;

            var columnName = GetColumnName(column);

            if (prefixTable is not null)
            {
                sb.Append(prefixTable);
                sb.Append('.');
            }

            sb.Append(columnName);

            if (equalsTable is not null)
            {
                sb.Append(" = ");
                if (equalsTable != "")
                {
                    sb.Append(equalsTable);
                    sb.Append('.');
                }

                sb.Append(columnName);
            }
        }
    }

    public virtual void AppendColumnsSeparatedWithAnd<TColumnInfo>(
        StringBuilder sb,
        List<TColumnInfo> columns,
        string? prefixTable = null,
        string? equalsTable = null)
        where TColumnInfo : IColumnInfo
    {
        AppendColumns(sb, columns, prefixTable, equalsTable, " AND ");
    }

    public virtual string GetTableIdentifier(ITableInfo table)
    {
        return string.IsNullOrEmpty(table.Schema)
            ? $"{GetTableSchema(table)}.{GetTableName(table)}"
            : GetTableName(table);
    }

    public virtual string GetTableSchema(ITableInfo table)
    {
        return NormalizeIdentifier(table.Schema!);
    }

    public virtual string GetTableName(ITableInfo table)
    {
        return NormalizeIdentifier(table.TableName);
    }

    public virtual string GetColumnName(IColumnInfo column)
    {
        return NormalizeIdentifier(column.ColumnName);
    }

    protected virtual string NormalizeIdentifier(string identifier)
    {
        return identifier;
    }
}