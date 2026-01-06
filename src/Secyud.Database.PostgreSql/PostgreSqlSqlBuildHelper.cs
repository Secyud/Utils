namespace Secyud.Database;

public class PostgreSqlSqlBuildHelper : SqlBuildHelperBase
{
    protected override string NormalizeIdentifier(string identifier)
    {
        return  $"\"{identifier}\"";
    }
}