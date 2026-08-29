using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Enumerations;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.Reports;

//[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("report/objectives")]
public class ObjectiveReportController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
{
    private readonly Services.Contract.IHtmlToPdfService htmlToPdfService;
    private readonly Services.Contract.IViewRenderService viewRenderService;
    private readonly IEntityRepository<Objective, System.Guid> objectiveRepository;

    public ObjectiveReportController(Services.Contract.IHtmlToPdfService htmlToPdfService, Services.Contract.IViewRenderService viewRenderService, IEntityRepository<Objective, System.Guid> objectiveRepository, Microsoft.Extensions.Localization.IStringLocalizer<ObjectiveReportController> localizer, Microsoft.Extensions.Logging.ILogger<ObjectiveReportController> logger) : base(localizer, logger)
    {
        this.htmlToPdfService = htmlToPdfService;
        this.viewRenderService = viewRenderService;
        this.objectiveRepository = objectiveRepository;
    }

    [Route("summary/web")]
    public IActionResult Summary()
    {
        return View(GetObjectivesSummaryModel());
    }

    [Route("summary/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintSummarySync()
    {
        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/Summary.cshtml",
                GetObjectivesSummaryModel()
            );

        // Opciones para el PDF
        var pdfOptions = new Models.Options.PdfOptionsModel
        {
            PageSize = "Letter",
            Orientation = Orientation.Portrait,
            Margins = new Models.MarginsModel { Top = 10, Right = 10, Bottom = 10, Left = 10 },
            EnableJavaScript = false,
            PrintBackground = false
        };

        // Generar PDF
        var pdfBytes = await htmlToPdfService.GeneratePdfFromHtmlAsync(htmlContent, pdfOptions);

        return File(pdfBytes, "application/pdf", $"Resumen de Objetivos - {System.DateTime.Now.Ticks}.pdf");
    }

    [Route("summary/{key}/web")]
    public IActionResult SummaryByObjective(System.Guid key)
    {
        return View(GetObjectiveSummaryModel(key));
    }

    [Route("summary/{key}/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintSummaryByObjectiveAsync(System.Guid key)
    {
        var model = GetObjectiveSummaryModel(key);

        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/SummaryByObjective.cshtml",
                model
            );

        // Opciones para el PDF
        var pdfOptions = new Models.Options.PdfOptionsModel
        {
            PageSize = "Letter",
            Orientation = Orientation.Portrait,
            Margins = new Models.MarginsModel { Top = 10, Right = 10, Bottom = 10, Left = 10 },
            EnableJavaScript = false,
            PrintBackground = false
        };

        // Generar PDF
        var pdfBytes = await htmlToPdfService.GeneratePdfFromHtmlAsync(htmlContent, pdfOptions);

        return File(pdfBytes, "application/pdf", $"Resumen de Objetivo {model.Index} - {System.DateTime.Now.Ticks}.pdf");
    }

    private Models.View.ObjectiveSummaryViewModel[] GetObjectivesSummaryModel()
    {
        return objectiveRepository.FindBy(entity => !entity.Archived).OrderBy(entity => entity.Index).ToArray().Select(entity => new Models.View.ObjectiveSummaryViewModel(entity)).ToArray();
    }

    private Models.View.ObjectiveSummaryViewModel GetObjectiveSummaryModel(System.Guid key)
    {
        return new Models.View.ObjectiveSummaryViewModel(objectiveRepository.Get(key));
    }
}
