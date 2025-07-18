using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Secyud.EntityFrameworkCore.Options;

namespace Secyud.EntityFrameworkCore.Bulker;

public class BulkOperationContext(DbContext dbContext, BulkOperationTable table, BulkEntityOptions options)
{
    public BulkEntityOptions Options { get; } = options;
    public BulkOperationTable Table { get; } = table;
    public DbContext DbContext { get; } = dbContext;
    public DbConnection Connection { get; } = dbContext.Database.GetDbConnection();
    public IDbContextTransaction? Transaction { get; } = dbContext.Database.CurrentTransaction;
}