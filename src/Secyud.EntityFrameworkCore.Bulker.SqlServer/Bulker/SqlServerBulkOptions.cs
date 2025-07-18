using Microsoft.Data.SqlClient;

namespace Secyud.EntityFrameworkCore.Bulker;

public class SqlServerBulkOptions
{
    public SqlBulkCopyOptions BulkCopyOptions { get; set; } = SqlBulkCopyOptions.Default;

    public event Action<SqlBulkCopy>? BulkCopyConfig;

    public void ConfigSqlBulkCopy(SqlBulkCopy sqlBulkCopy)
    {
        BulkCopyConfig?.Invoke(sqlBulkCopy);
    }
}