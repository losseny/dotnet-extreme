using Presentation.Services.Interfaces;

namespace Presentation.Services;

public class ApplicationContext: IApplicationContext
{
    public Guid UserId { get; set; } = Guid.Parse("a67facde-c944-4d27-a8c6-0a2bbaa3a0cb");
}
