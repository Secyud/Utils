using System.Text;

namespace Secyud.Database;

public abstract class SqlBuilderBase
{
    public StringBuilder Builder { get; } = new();

    public virtual void AppendColumns<TColumnInfo>(
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
                Builder.Append(splitString);
            else
                appendComma = true;

            var columnName = GetColumnName(column);

            if (prefixTable is not null)
            {
                Builder.Append(prefixTable);
                Builder.Append('.');
            }

            Builder.Append(columnName);

            if (equalsTable is not null)
            {
                Builder.Append(" = ");
                if (equalsTable != "")
                {
                    Builder.Append(equalsTable);
                    Builder.Append('.');
                }

                Builder.Append(columnName);
            }
        }
    }

    public virtual void AppendColumnsSeparatedWithAnd<TColumnInfo>(
        List<TColumnInfo> columns,
        string? prefixTable = null,
        string? equalsTable = null)
        where TColumnInfo : IColumnInfo
    {
        AppendColumns(columns, prefixTable, equalsTable, " AND ");
    }

    public virtual string GetTableName(ITableInfo table)
    {
        return $"{table.Schema}{(table.Schema is null ? "" : ".")}[{table.TableName}]";
    }

    public virtual string GetColumnName(IColumnInfo column)
    {
        return $"\"{column.ColumnName}\"";
    }

    public override string ToString()
    {
        return Builder.ToString();
    }
}