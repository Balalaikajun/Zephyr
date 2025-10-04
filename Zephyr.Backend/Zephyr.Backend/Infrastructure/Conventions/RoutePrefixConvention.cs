using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Zephyr.Backend.Infrastructure.Conventions;

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
                // Объединяем существующий маршрут с префиксом
                selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                    _prefix, selector.AttributeRouteModel);
            else
                // Если маршрута нет, просто ставим префикс
                selector.AttributeRouteModel = _prefix;
    }
}