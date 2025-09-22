namespace Secyud.Database;

public abstract class SqlGeneratorBase
{
    public abstract string GenerateCreateTableSql(ITableInfo tableInfo,
        IColumnInfo[] columnInfos, IIndexInfo[] indexInfos);

    public abstract string GenerateDeleteTableSql(ITableInfo tableInfo);
    
    public abstract string GenerateInsertDataSql(ITableInfo tableInfo,
        IColumnInfo[] columnInfos, IEnumerable<object[]> data);

    public abstract string GenerateUpdateDataSql(ITableInfo tableInfo,
        IColumnInfo[] columnInfos, IEnumerable<object[]> data, string condition);

    public abstract string GenerateDeleteDataSql(ITableInfo tableInfo, string condition);
}