using System.Globalization;

namespace Application.Common.Behaviours;

public class ChronosBehaviour
{
    private static ChronosBehaviour? Chronos { get; set; }

    public static ChronosBehaviour GetInstance() => Chronos ??= new ChronosBehaviour();
    public DateOnly ConvertWeekToDate(int week) => DateOnly.FromDateTime(ISOWeek.ToDateTime(DateTime.Now.Year, week, DayOfWeek.Monday));

}
