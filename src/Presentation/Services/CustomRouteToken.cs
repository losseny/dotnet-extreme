using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Presentation.Services;

public class CustomRouteToken : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var hasAttributeRouteModels = controller.Selectors
                .Any(selector => selector.AttributeRouteModel != null);

            if (!hasAttributeRouteModels)
            {
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                {
                    Template = $"api/{controller.ControllerName.ToLower()}"
                };
            }
        }
    }
}
