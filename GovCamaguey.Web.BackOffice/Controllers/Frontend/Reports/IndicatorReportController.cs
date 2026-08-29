using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Enumerations;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.Reports;

[Route("report/indicators")]
public class IndicatorReportController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
{
    private readonly Services.Contract.IHtmlToPdfService htmlToPdfService;
    private readonly Services.Contract.IViewRenderService viewRenderService;
    private readonly IEntityRepository<Objective, System.Guid> objectiveRepository;
    private readonly IEntityRepository<GlobalIndicator, System.Guid> globalIndicatorRepository;
    private readonly IEntityRepository<GlobalIndicatorResult, long> globalIndicatorResultRepository;
    private readonly IEntityRepository<ObjectiveIndicatorResult, long> objectiveIndicatorResultRepository;
    private readonly IEntityRepository<ProyectionIndicatorResult, long> proyectionIndicatorResultRepository;
    private readonly IEntityRepository<ActionIndicatorResult, long> actionIndicatorResultRepository;

    public IndicatorReportController(
        IEntityRepository<ActionIndicatorResult, long> actionIndicatorResultRepository,
        IEntityRepository<ProyectionIndicatorResult, long> proyectionIndicatorResultRepository,
        IEntityRepository<ObjectiveIndicatorResult, long> objectiveIndicatorResultRepository,
        IEntityRepository<GlobalIndicatorResult, long> globalIndicatorResultRepository,
        IEntityRepository<GlobalIndicator, System.Guid> globalIndicatorRepository,
        IEntityRepository<Objective, System.Guid> objectiveRepository,
        Services.Contract.IViewRenderService viewRenderService,
        Services.Contract.IHtmlToPdfService htmlToPdfService,
        Microsoft.Extensions.Localization.IStringLocalizer<IndicatorReportController> localizer, 
        ILogger<IndicatorReportController> logger) : base(localizer, logger)
    {
        this.htmlToPdfService = htmlToPdfService;
        this.viewRenderService = viewRenderService;
        this.objectiveRepository = objectiveRepository;
        this.globalIndicatorRepository = globalIndicatorRepository;
        this.globalIndicatorResultRepository = globalIndicatorResultRepository;
        this.objectiveIndicatorResultRepository = objectiveIndicatorResultRepository;
        this.proyectionIndicatorResultRepository = proyectionIndicatorResultRepository;
        this.actionIndicatorResultRepository = actionIndicatorResultRepository;
    }

    [Route("stats/reference/{referenceTicks}/web")]
    public IActionResult Stats(long referenceTicks)
    {
        return View(GetObjectivesIndicatorsStatsReportModel(new System.DateTime(referenceTicks)));
    }

    [Route("stats/reference/{referenceTicks}/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintStatsAsync(long referenceTicks)
    {
        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/IndicatorReport/Stats.cshtml",
                GetObjectivesIndicatorsStatsReportModel(new System.DateTime(referenceTicks))
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

        return File(pdfBytes, "application/pdf", $"Estado de Indicadores - {System.DateTime.Now.Ticks}.pdf");
    }

    [Route("stats/{key}/reference/{referenceTicks}/web")]
    public IActionResult StatsByObjective(System.Guid key, long referenceTicks)
    {
        return View(GetObjectiveStatsModel(key, new System.DateTime(referenceTicks)));
    }

    [Route("stats/{key}/reference/{referenceTicks}/print")]
    public async System.Threading.Tasks.Task<IActionResult> PrintStatsByObjectiveAsync(System.Guid key, long referenceTicks)
    {
        System.DateTime referenceAt = new(referenceTicks);

        ObjectiveIndicatorsStatsReportViewModel model = new()
        {
            ReferenceAt = referenceAt,
            Objective = GetObjectiveStatsModel(key, referenceAt)
        };

        var htmlContent = await viewRenderService.RenderViewToStringAsync(
                "~/Views/IndicatorReport/StatsByObjective.cshtml",
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

        return File(pdfBytes, "application/pdf", $"Resumen de Objetivo {model.Objective.Index} - {System.DateTime.Now.Ticks}.pdf");
    }

    private ObjectivesIndicatorsStatsReportViewModel GetObjectivesIndicatorsStatsReportModel(System.DateTime referenceAt)
    {
        ObjectivesIndicatorsStatsReportViewModel model = new()
        {
            ReferenceAt = referenceAt,
            Objectives = GetObjectivesStatsReportModel(referenceAt)
        };

        GlobalIndicator[] indicators = [.. globalIndicatorRepository.FindBy(entity => !entity.Archived).OrderBy(entity => entity.Priority)];

        List<CommonIndicatorStatsViewModel> globalIndicatorStats = [];

        foreach (GlobalIndicator indicator in indicators)
        {
            GlobalIndicatorResult referenceResult = globalIndicatorResultRepository
                .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At <= referenceAt)
                .OrderByDescending(_entity => _entity.At)
                .Take(1)
                .FirstOrDefault();

            GlobalIndicatorResult lastResult = globalIndicatorResultRepository
                .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At > referenceAt)
                .OrderByDescending(_entity => _entity.At)
                .Take(1)
                .FirstOrDefault();

            System.DateTime now = System.DateTime.Now;

            GlobalIndicatorMontlyPlan plan = indicator.MontlyPlans.Where(item => item.Year == now.Year && item.Month >= now.Month).OrderBy(item => item.Month).FirstOrDefault();

            globalIndicatorStats.Add(new CommonIndicatorStatsViewModel
            {
                Name = indicator.Name,
                Amount = indicator.Amount,
                UnitMeasurement = indicator.UnitMeasurement,
                ReferenceResult = referenceResult?.Amount ?? 0,
                LastResult = lastResult?.Amount ?? referenceResult?.Amount ?? 0,
                Context = lastResult?.Description,
                Plan = plan == null ? null : new IndicatorPlanViewModel
                {
                    Month = plan.Month,
                    Year = plan.Year,
                    Amount = plan.Amount
                }
            });
        }

        model.Indicators = [.. globalIndicatorStats];

        return model;
    }

    private ObjectiveStatsViewModel[] GetObjectivesStatsReportModel(System.DateTime referenceAt)
    {
        return objectiveRepository.GetAll().OrderBy(entity => entity.Index).ToArray().Select(entity => GetObjectiveStatsModel(entity.Id, referenceAt)).ToArray();
    }

    private ObjectiveStatsViewModel GetObjectiveStatsModel(System.Guid key, System.DateTime referenceAt)
    {
        System.DateTime now = System.DateTime.Now; 
        
        Objective entity = objectiveRepository.Get(key);

        ObjectiveStatsViewModel model = new()
        {
            Index = entity.Index,
            Name = entity.Name
        };

        List<CommonIndicatorStatsViewModel> objectiveIndicatorStats = [];

        ObjectiveIndicator[] objectiveIndicators = [.. entity.Indicators.OrderBy(_entity => _entity.Priority)];

        foreach (ObjectiveIndicator indicator in objectiveIndicators)
        {
            if (indicator.Archived) continue;

            ObjectiveIndicatorResult referenceResult = objectiveIndicatorResultRepository
                .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At <= referenceAt)
                .OrderByDescending(_entity => _entity.At)
                .Take(1)
                .FirstOrDefault();

            ObjectiveIndicatorResult lastResult = objectiveIndicatorResultRepository
                .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At > referenceAt)
                .OrderByDescending(_entity => _entity.At)
                .Take(1)
                .FirstOrDefault();

            ObjectiveIndicatorMontlyPlan plan = indicator.MontlyPlans.Where(item => item.Year == now.Year && item.Month >= now.Month).OrderBy(item => item.Month).FirstOrDefault();

            objectiveIndicatorStats.Add(new CommonIndicatorStatsViewModel
            {
                Name = indicator.Name,
                Amount = indicator.Amount,
                UnitMeasurement = indicator.UnitMeasurement,
                ReferenceResult = referenceResult?.Amount ?? 0,
                LastResult = lastResult?.Amount ?? referenceResult?.Amount ?? 0,
                Context = lastResult?.Description,
                Plan = plan == null ? null : new IndicatorPlanViewModel
                {
                    Month = plan.Month,
                    Year = plan.Year,
                    Amount = plan.Amount
                }
            });
        }

        List<ProyectionStatsViewModel> proyectionStats = [];

        Proyection[] proyections = [.. entity.Proyections.OrderBy(_entity => _entity.Index)];

        foreach (Proyection _entity in proyections)
        {
            ProyectionStatsViewModel _model = new()
            {
                Index = _entity.Index,
                Name = _entity.Name
            };

            List<CommonIndicatorStatsViewModel> proyectionIndicatorStats = [];

            ProyectionIndicator[] proyectionIndicators = [.. _entity.Indicators.OrderBy(_entity => _entity.Priority)];

            foreach (var indicator in proyectionIndicators)
            {
                if (indicator.Archived) continue;

                ProyectionIndicatorResult referenceResult = proyectionIndicatorResultRepository
                    .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At <= referenceAt)
                    .OrderByDescending(_entity => _entity.At)
                    .Take(1)
                    .FirstOrDefault();

                ProyectionIndicatorResult lastResult = proyectionIndicatorResultRepository
                    .FindBy(_entity => _entity.IndicatorId == indicator.Id && _entity.At > referenceAt)
                    .OrderByDescending(_entity => _entity.At)
                    .Take(1)
                    .FirstOrDefault();

                ProyectionIndicatorMontlyPlan _plan = indicator.MontlyPlans.Where(item => item.Year == now.Year && item.Month >= now.Month).OrderBy(item => item.Month).FirstOrDefault();

                proyectionIndicatorStats.Add(new CommonIndicatorStatsViewModel
                {
                    Name = indicator.Name,
                    Amount = indicator.Amount,
                    UnitMeasurement = indicator.UnitMeasurement,
                    ReferenceResult = referenceResult?.Amount ?? 0,
                    LastResult = lastResult?.Amount ?? referenceResult?.Amount ?? 0,
                    Context = lastResult?.Description,
                    Plan = _plan == null ? null : new IndicatorPlanViewModel
                    {
                        Month = _plan.Month,
                        Year = _plan.Year,
                        Amount = _plan.Amount
                    }
                });
            }

            _model.Indicators = [.. proyectionIndicatorStats];

            List<ActionStatsViewModel> actionStats = [];

            Action[] actions = [.. _entity.Actions.OrderBy(__entity => __entity.Index)];

            foreach (var __entity in actions)
            {
                ActionStatsViewModel __model = new()
                {
                    Index = __entity.Index,
                    Name = __entity.Name
                };

                List<CommonIndicatorStatsViewModel> actionIndicatorStats = [];

                ActionIndicator[] actionIndicators = [.. __entity.Indicators.OrderBy(_entity => _entity.Priority)];

                foreach (var _indicator in actionIndicators)
                {
                    if (_indicator.Archived) continue;

                    ActionIndicatorResult referenceResult = actionIndicatorResultRepository
                        .FindBy(_entity => _entity.IndicatorId == _indicator.Id && _entity.At <= referenceAt)
                        .OrderByDescending(_entity => _entity.At)
                        .Take(1)
                        .FirstOrDefault();

                    ActionIndicatorResult lastResult = actionIndicatorResultRepository
                        .FindBy(_entity => _entity.IndicatorId == _indicator.Id && _entity.At > referenceAt)
                        .OrderByDescending(_entity => _entity.At)
                        .Take(1)
                        .FirstOrDefault();

                    ActionIndicatorMontlyPlan __plan = _indicator.MontlyPlans.Where(item => item.Year == now.Year && item.Month >= now.Month).OrderBy(item => item.Month).FirstOrDefault();

                    actionIndicatorStats.Add(new CommonIndicatorStatsViewModel
                    {
                        Name = _indicator.Name,
                        Amount = _indicator.Amount,
                        UnitMeasurement = _indicator.UnitMeasurement,
                        ReferenceResult = referenceResult?.Amount ?? 0,
                        LastResult = lastResult?.Amount ?? referenceResult?.Amount ?? 0,
                        Context = lastResult?.Description,
                        Plan = __plan == null ? null : new IndicatorPlanViewModel
                        {
                            Month = __plan.Month,
                            Year = __plan.Year,
                            Amount = __plan.Amount
                        }
                    });
                }

                __model.Indicators = [.. actionIndicatorStats];

                actionStats.Add(__model);
            }

            _model.Actions = [.. actionStats];

            proyectionStats.Add(_model);
        }

        model.Indicators = [.. objectiveIndicatorStats];
        model.Proyections = [.. proyectionStats];

        return model;
    }
}