using Microsoft.EntityFrameworkCore;

namespace Secyud.Utils.EntityFrameworkCore.Exceptions;

public class BulkOperationAdapterNotRegisteredException : Exception
{
    public BulkOperationAdapterNotRegisteredException(DbContext dbContext) : base(
        $"No adapter registered for {dbContext.Database.ProviderName}")
    {
    }
}