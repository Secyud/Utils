namespace Secyud.EntityFrameworkCore.Options;

public class BulkEntityOptions
{
    public bool IncludeShadowProperties { get; set; }
    public bool IncludeOwnedProperties { get; set; }
    public bool IncludeGraph { get; set; }
}