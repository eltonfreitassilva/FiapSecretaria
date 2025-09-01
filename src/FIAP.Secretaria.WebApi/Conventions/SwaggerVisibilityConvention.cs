using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FIAP.Secretaria.WebApi.Conventions;

public class SwaggerVisibilityConvention : ApiVisibilityConvention
{
    private readonly IWebHostEnvironment _enviroment;
    private readonly string CONTROLLER_SOBRE = "Sobre";

    public SwaggerVisibilityConvention(IWebHostEnvironment enviroment)
    {
        _enviroment = enviroment;
    }

    protected override bool ShouldApply(ActionModel action)
    {
        if (action.Controller.ControllerName == CONTROLLER_SOBRE)
            action.ApiExplorer.IsVisible = true;
        else
            action.ApiExplorer.IsVisible = !_enviroment.IsProduction();

        return base.ShouldApply(action);
    }
}