namespace Secyud.Data;

public class TreeDemoItem : DemoItemBase
{
    public Guid? ParentId { get; set; }

    public List<TreeDemoItem> Children { get; set; } = [];

    public static List<TreeDemoItem> Generate(int itemCount = 20, bool hierarchy = false)
    {
        var res = new List<TreeDemoItem>();
        for (var i = 0; i < itemCount; i++)
        {
            var item = new TreeDemoItem();
            res.Add(item);
            var index = RandomUtils.Rand(-1, i);
            if (index == -1) continue;
            var parent = res[index];
            item.ParentId = parent.Id;
            parent.Children.Add(item);
        }

        if (hierarchy)
        {
            res = res.Where(u => u.ParentId is null).ToList();
        }

        return res;
    }
}