namespace Application.Common.Extensions;

public static class DateOnlyExtensions
{
    public static IEnumerable<DateOnly> GenerateDaysInWeek(this DateOnly date)
    {
        var dayOfWeek = date.DayOfWeek;

        return dayOfWeek switch
        {
            DayOfWeek.Monday => WeekDatesFunc(() => date),
            DayOfWeek.Sunday => WeekDatesFunc(() => date.AddDays(-6)),
            _ => WeekDatesFunc(() => date.AddDays(-((int)dayOfWeek - 1)))
        };

        IEnumerable<DateOnly> WeekDatesFunc(Func<DateOnly> operation)
        {
            var firstDayOfWeek = operation();
            return Enumerable.Range(0, 7)
                .Select(i => firstDayOfWeek.AddDays(i));
        }
    }
}
