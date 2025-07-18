using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        dataTable.Columns.AddRange(context.Table.Columns
            .Select(u => new DataColumn(u.PropertyName, u.Property.ClrType))
            .ToArray());

        foreach (var entity in entities)
        {
            dataTable.Rows.Add();

        }
    }
}