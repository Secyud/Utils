namespace Secyud.EntityFrameworkCore.Options;

public class BulkEntityOptionsBuilder
{
    private readonly Dictionary<Type, List<Action<BulkEntityOptions>>> _options = [];
    private readonly List<Action<BulkEntityOptions>> _globalOptions = [];

    private List<Action<BulkEntityOptions>> GetOrAddOptions(Type type)
    {
        if (!_options.TryGetValue(type, out var list))
        {
            list = [];
            _options[type] = list;
        }

        return list;
    }

    public void ConfigureEntity<TEntity>(Action<BulkEntityOptions> options)
    {
        var list = GetOrAddOptions(typeof(TEntity));
        list.Add(options);
    }

    public void ConfigureGlobal(Action<BulkEntityOptions> options)
    {
        _globalOptions.Add(options);
    }

    public BulkEntityOptions BuildOptions<TEntity>()
    {
        return BuildOptions(typeof(TEntity));
    }

    public BulkEntityOptions BuildOptions(Type type)
    {
        BulkEntityOptions options = new();
        foreach (var action in _globalOptions)
        {
            action(options);
        }

        if (_options.TryGetValue(type, out var list))
        {
            foreach (var action in list)
            {
                action(options);
            }
        }

        return options;
    }
}