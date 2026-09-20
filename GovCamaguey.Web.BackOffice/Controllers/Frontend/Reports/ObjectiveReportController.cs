using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Enumerations;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.Reports;

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

    [Route("book/web")]
    public IActionResult Book()
    {
        return View(GetObjectivesOverviewModel());
    }

    [Route("table/web")]
    public IActionResult Table()
    {
        return View(GetObjectivesOverviewModel());
    }

    [Route("book/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintBookAsync()
    {
        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/Book.cshtml",
                GetObjectivesOverviewModel()
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

        return File(pdfBytes, "application/pdf", $"Libro Resumen de Objetivos - {System.DateTime.Now.Ticks}.pdf");
    }

    [Route("table/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintTableSync()
    {
        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/Table.cshtml",
                GetObjectivesOverviewModel()
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

        return File(pdfBytes, "application/pdf", $"Tabla Resumen de Objetivos - {System.DateTime.Now.Ticks}.pdf");
    }

    [Route("book/{key}/web")]
    public IActionResult BookByObjective(System.Guid key)
    {
        return View(GetObjectiveSummaryModel(key));
    }

    [Route("table/{key}/web")]
    public IActionResult TableByObjective(System.Guid key)
    {
        return View(GetObjectiveSummaryModel(key));
    }

    [Route("book/{key}/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintBookByObjectiveAsync(System.Guid key)
    {
        var model = GetObjectiveSummaryModel(key);

        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/BookByObjective.cshtml",
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

        return File(pdfBytes, "application/pdf", $"Libro Resumen de Objetivo {model.Index} - {System.DateTime.Now.Ticks}.pdf");
    }

    [Route("table/{key}/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintTableByObjectiveAsync(System.Guid key)
    {
        var model = GetObjectiveSummaryModel(key);

        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/ObjectiveReport/TableByObjective.cshtml",
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

        return File(pdfBytes, "application/pdf", $"Tabla Resumen de Objetivo {model.Index} - {System.DateTime.Now.Ticks}.pdf");
    }

    private Models.View.ObjectiveSummaryViewModel[] GetObjectivesOverviewModel()
    {
        return objectiveRepository.FindBy(entity => !entity.Archived).OrderBy(entity => entity.Index).ToArray().Select(entity => new Models.View.ObjectiveSummaryViewModel(entity)).ToArray();
    }

    private Models.View.ObjectiveSummaryViewModel GetObjectiveSummaryModel(System.Guid key)
    {
        return new Models.View.ObjectiveSummaryViewModel(objectiveRepository.Get(key));
    }
}
