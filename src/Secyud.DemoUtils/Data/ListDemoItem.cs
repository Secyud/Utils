namespace Secyud.Data;

public class ListDemoItem : DemoItemBase
{
    public static List<ListDemoItem> Generate(int count = 20)
    {
        var res = new List<ListDemoItem>();
        for (var i = 0; i < count; i++)
        {
            res.Add(new ListDemoItem());
        }

        return res;
    }
}