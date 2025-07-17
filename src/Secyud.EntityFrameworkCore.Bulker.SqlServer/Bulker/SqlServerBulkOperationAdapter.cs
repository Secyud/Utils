using Microsoft.EntityFrameworkCore;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public class SqlServerBulkOperationAdapter : BulkOperationAdapterBase
{
    public override bool IsThisAdapter(DbContext context)
    {
        return context.Database.IsSqlServer();
    }
}