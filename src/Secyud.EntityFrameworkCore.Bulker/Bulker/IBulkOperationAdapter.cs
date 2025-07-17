using Microsoft.EntityFrameworkCore;

namespace Secyud.Utils.EntityFrameworkCore.Bulks;

public interface IBulkOperationAdapter : IBulkOperationHandler
{
    bool IsThisAdapter(DbContext context);
}