namespace Secyud.Database;

public interface IIndexInfo
{
    string IndexName { get; set; }
    string[] IndexColumns { get; set; }
}