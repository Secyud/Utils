using Microsoft.EntityFrameworkCore.Metadata;

namespace Secyud.EntityFrameworkCore.Models;

public class BulkOperationNavigation(INavigation navigation)
{
    public INavigation Ref { get; set; } = navigation;
    public string Name { get; set; } = navigation.Name;
}