namespace LogHub.Processing;

/// <summary>
/// Находит в сборке все классы, реализующие интерфейс, и создаёт их.
/// Благодаря этому новый приёмник или формат добавляется одним новым файлом,
/// а список в точке входа не правится.
/// </summary>
public static class PluginCatalog
{
    public static IReadOnlyList<T> Discover<T>() where T : class
    {
        var result = new List<T>();

        foreach (var type in typeof(T).Assembly.GetTypes())
        {
            if (!type.IsClass || type.IsAbstract || !typeof(T).IsAssignableFrom(type))
            {
                continue;
            }

            if (type.GetConstructor(Type.EmptyTypes) is null)
            {
                continue;
            }

            if (Activator.CreateInstance(type) is T instance)
            {
                result.Add(instance);
            }
        }

        return result;
    }
}
