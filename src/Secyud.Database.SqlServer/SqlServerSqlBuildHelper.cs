namespace Secyud.Database;

public class SqlServerSqlBuildHelper : SqlBuildHelperBase
{
    protected override string NormalizeIdentifier(string identifier)
    {
        return $"[{identifier}]";
    }
}