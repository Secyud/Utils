using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Secyud.Database;
using Secyud.EntityFrameworkCore.Options;

namespace Secyud.EntityFrameworkCore.Bulker;

public class SqlServerBulkOperationAdapter(
    IOptions<BulkOptions> options,
    ILoggerFactory loggerFactory,
    IOptions<SqlServerBulkOptions> sqlOptions)
    : BulkOperationAdapterBase(options, loggerFactory), ISqlServerBulkOperationAdapter
{
    public override bool IsThisAdapter(DbContext dbContext)
    {
        return dbContext.Database.IsSqlServer();
    }

    protected override async Task InsertManyAsync<TEntity>(BulkOperationContext context, IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        using var sqlBulkCopy = new SqlBulkCopy(
            context.Connection as SqlConnection,
            sqlOptions.Value.BulkCopyOptions,
            context.Transaction?.GetDbTransaction() as SqlTransaction);
        sqlOptions.Value.ConfigSqlBulkCopy(sqlBulkCopy);

        foreach (var column in context.Table.Columns)
        {
            sqlBulkCopy.ColumnMappings.Add(column.PropertyName, column.ColumnName);
        }

        var dataTable = new DataTable();

        var columns = context.Table.Columns;

        dataTable.Columns.AddRange(columns
            .Select(u => new DataColumn(u.PropertyName, u.Property.ClrType))
            .ToArray());

        foreach (var entity in entities)
        {
            dataTable.Rows.Add(columns.Select(u => u.Get(entity)).ToArray());
        }

        await sqlBulkCopy.WriteToServerAsync(dataTable, cancellationToken);
    }

    protected override async Task MergeTableAsync(BulkOperationContext context, BulkOperationTableInfo source,
        CancellationToken cancellationToken)
    {
        await context.DbContext.Database.ExecuteSqlRawAsync(
            SqlSetIdentityInsert(context, true), cancellationToken);

        await base.MergeTableAsync(context, source, cancellationToken);


        await context.DbContext.Database.ExecuteSqlRawAsync(
            SqlSetIdentityInsert(context, false), cancellationToken);
    }

    protected static string SqlSetIdentityInsert(BulkOperationContext context, bool insert)
    {
        return $"SET IDENTITY_INSERT [{context.Table.TableName}] {(insert ? "ON" : "OFF")};";
    }

    protected override (string sql, IEnumerable<object> parameters) SqlMergeTable(
        BulkOperationContext context, BulkOperationTableInfo source)
    {
        var sb = new SqlServerSqlBuilder();
        List<object> parameters = [];

        var targetTable = sb.GetTableName(context.Table);
        var sourceTable = sb.GetTableName(source);

        sb.Builder.Append(
            $"""
             MERGE {targetTable} WITH (HOLDLOCK) AS T
             USING {sourceTable} AS S 
             """);

        if (context.Table.PrimaryKeys.Count != 0)
        {
            sb.Builder.Append("ON ");
            sb.AppendColumnsSeparatedWithAnd(
                context.Table.PrimaryKeys, "T", "S");
        }

        sb.Builder.Append(" WHEN NOT MATCHED BY TARGET THEN INSERT ");
        if (context.Table.Columns.Count != 0)
        {
            sb.Builder.Append('(');
            sb.AppendColumns(context.Table.Columns);
            sb.Builder.Append(") VALUES (");
            sb.AppendColumns(context.Table.Columns, "S");
            sb.Builder.Append(')');
        }
        else
        {
            sb.Builder.Append("DEFAULT VALUES");
        }


        return (sb.ToString(), parameters);
    }
}