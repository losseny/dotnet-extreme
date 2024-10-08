namespace Domain.Common.Extensions;

public static class CollectionExtensions
{
    public static void AddAll<TClass>(this ICollection<TClass> list, IEnumerable<TClass> toBeAdded) where TClass : class
    {
        foreach (var type in toBeAdded)
        {
            list.Add(type);
        }
    }
    public static ICollection<TClass> AddAll<TClass>(this ICollection<TClass> list, IEnumerable<TClass> toBeAdded, Func<ICollection<TClass>, TClass, bool> func) where TClass : class
    {
        var notAdded = new List<TClass>();
        foreach (var type in toBeAdded)
        {
            if (func(list, type))
            {
                notAdded.Add(type);
            }
            else
            {
                list.Add(type);
            }
        }
        return notAdded;
    }
}
