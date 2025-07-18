using Microsoft.EntityFrameworkCore;

namespace Secyud.EntityFrameworkCore.Bulker;

public interface IBulkOperationAdapter : IBulkOperationHandler
{
    bool IsThisAdapter(DbContext dbContext);
}