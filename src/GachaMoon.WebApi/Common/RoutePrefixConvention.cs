using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;

namespace GachaMoon.WebApi.Common;

public class RoutePrefixConvention : IControllerConvention
{
    public bool Apply(IControllerConventionBuilder controller, ControllerModel controllerModel)
    {
        controllerModel.RouteValues["controller"] = $"api/v1/{controllerModel.ControllerName}";
        return true;
    }
}
