namespace Data.Common.Interfaces;

public interface ISeeder<out T>
{
    T Seed();
}
