using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Zephyr.Backend.Infrastructure.Conventions;

/// <summary>
/// Конвенция для добавления префиксов всем маршрутам Api
/// </summary>
public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _prefix;
    
    public RoutePrefixConvention(string prefix)
    {
        _prefix = new AttributeRouteModel(new RouteAttribute(prefix));
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        foreach (var selector in controller.Selectors)
            if (selector.AttributeRouteModel != null)
                selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                    _prefix, selector.AttributeRouteModel);
            else
                selector.AttributeRouteModel = _prefix;
    }
}