namespace Data.Common.Interfaces;

public interface IContextInitializer
{
	Task InitialiseAsync();
	Task SeedAsync();
}
