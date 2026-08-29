using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Services.Contract;
using System.IO;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Services;

internal sealed class DefaultViewRenderService : IViewRenderService
{
    private readonly Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine razorViewEngine;
    private readonly ITempDataProvider tempDataProvider;
    private readonly System.IServiceProvider serviceProvider;
    private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor;

    public DefaultViewRenderService(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, System.IServiceProvider serviceProvider, ITempDataProvider tempDataProvider, Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine razorViewEngine)
    {
        this.razorViewEngine = razorViewEngine;
        this.tempDataProvider = tempDataProvider;
        this.serviceProvider = serviceProvider;
        this.httpContextAccessor = httpContextAccessor;
    }

    public System.Threading.Tasks.Task<string> RenderPartialViewToStringAsync(string viewName, object model)
    {
        throw new System.NotImplementedException();
    }

    public async System.Threading.Tasks.Task<string> RenderViewToStringAsync(string viewName, object model)
    {
        var actionContext = GetActionContext();
        var view = FindView(actionContext, viewName);

        using var stringWriter = new StringWriter();

        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary())
            {
                Model = model
            },
            new TempDataDictionary(
                actionContext.HttpContext,
                tempDataProvider),
            stringWriter,
            new HtmlHelperOptions()
        );

        await view.RenderAsync(viewContext);

        return stringWriter.ToString();
    }

    private Microsoft.AspNetCore.Mvc.ActionContext GetActionContext()
    {
        var httpContext = httpContextAccessor.HttpContext;

        httpContext ??= new Microsoft.AspNetCore.Http.DefaultHttpContext { RequestServices = serviceProvider };

        return new Microsoft.AspNetCore.Mvc.ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
    }

    private Microsoft.AspNetCore.Mvc.ViewEngines.IView FindView(Microsoft.AspNetCore.Mvc.ActionContext actionContext, string viewName)
    {
        var getViewResult = razorViewEngine.GetView(null, viewName, false);

        if (getViewResult.Success)
        {
            return getViewResult.View;
        }

        var findViewResult = razorViewEngine.FindView(actionContext, viewName, false);

        if (findViewResult.Success)
        {
            return findViewResult.View;
        }

        throw new System.InvalidOperationException($"Unable to find view '{viewName}'");
    }
}