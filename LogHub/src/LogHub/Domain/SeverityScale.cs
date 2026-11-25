namespace LogHub.Domain;

/// <summary>
/// Шкала уровней. Список уровней по-прежнему зашит в код: добавить уровень
/// значит поправить этот файл. Сознательно не выносил в абстракцию, см. отчёт.
/// </summary>
public static class SeverityScale
{
    public static int Rank(string severity) => severity switch
    {
        "Debug" => 0,
        "Info" => 1,
        "Warning" => 2,
        "Error" => 3,
        "Critical" => 4,
        _ => 1
    };

    public static bool IsAlarming(string severity) => severity == "Error" || severity == "Critical";
}
