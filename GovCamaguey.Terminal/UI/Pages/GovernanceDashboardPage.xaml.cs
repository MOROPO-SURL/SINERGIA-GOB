using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages;

public partial class GovernanceDashboardPage : Microsoft.Maui.Controls.ContentPage
{
    private static SKColor[] COLORS = new SKColor[]
    {
        SKColors.Blue, SKColors.Red, SKColors.Green, SKColors.Orange, SKColors.Purple,
        SKColors.Teal, SKColors.Brown, SKColors.Coral, SKColors.Indigo, SKColors.Pink,
        SKColors.Lime, SKColors.Cyan, SKColors.Magenta, SKColors.Gold, SKColors.Silver,
        SKColors.Maroon, SKColors.Olive, SKColors.Navy, SKColors.Turquoise, SKColors.Violet
    };

    public GovernanceDashboardPage()
	{
		InitializeComponent();

        GlobalObjectivesStatusPieChart.SetAppTheme<LiveChartsCore.Drawing.IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext>>(LiveChartsCore.SkiaSharpView.Maui.PieChart.LegendTextPaintProperty, new SolidColorPaint(SkiaSharp.SKColors.Black), new SolidColorPaint(SkiaSharp.SKColors.White));
        GlobalObjectivesProgressCartesianChart.SetAppTheme<LiveChartsCore.Drawing.IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext>>(LiveChartsCore.SkiaSharpView.Maui.CartesianChart.LegendTextPaintProperty, new SolidColorPaint(SkiaSharp.SKColors.Black), new SolidColorPaint(SkiaSharp.SKColors.White));
        ProyectionDistributionPieChart.SetAppTheme<LiveChartsCore.Drawing.IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext>>(LiveChartsCore.SkiaSharpView.Maui.PieChart.LegendTextPaintProperty, new SolidColorPaint(SkiaSharp.SKColors.Black), new SolidColorPaint(SkiaSharp.SKColors.White));
        ActionDistributionPieChart.SetAppTheme<LiveChartsCore.Drawing.IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext>>(LiveChartsCore.SkiaSharpView.Maui.PieChart.LegendTextPaintProperty, new SolidColorPaint(SkiaSharp.SKColors.Black), new SolidColorPaint(SkiaSharp.SKColors.White));
        IndicatorDistributionPieChart.SetAppTheme<LiveChartsCore.Drawing.IPaint<LiveChartsCore.SkiaSharpView.Drawing.SkiaSharpDrawingContext>>(LiveChartsCore.SkiaSharpView.Maui.PieChart.LegendTextPaintProperty, new SolidColorPaint(SkiaSharp.SKColors.Black), new SolidColorPaint(SkiaSharp.SKColors.White));

        BindingContext = new Models.Page.GovernanceDashboardPageModel()
        {
            GlobalObjectivesStatusChart = new Models.View.ChartViewModel(),
            GlobalObjectivesProgressChart = new Models.View.ChartViewModel
            {
                YAxes = 
                [
                    new Axis
                    {
                        Labeler = value => $"{(value * 100):F2}%",
                        MaxLimit = 1.0d,
                        MinLimit = 0.0d
                    }
                ]
            },
            MonthlyObjectivesProgressChart = new Models.View.ChartViewModel
            {
                YAxes = [
                    new Axis
                    {
                        Labels = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
                        ForceStepToMin = true,
                        MinStep = 1
                    }
                ]
            },
            ProyectionDistributionChart = new Models.View.ChartViewModel(),
            ActionDistributionChart = new Models.View.ChartViewModel(),
            IndicatorDistributionChart = new Models.View.ChartViewModel()
        };

        IsEnabled = false;
    }

    private Models.Page.GovernanceDashboardPageModel ConcreteBindingContext { get => BindingContext as Models.Page.GovernanceDashboardPageModel; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (IsEnabled) return;

        IsEnabled = true;

        ReloadToolbarItem_Clicked(this, System.EventArgs.Empty);
    }

    private void ReloadToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        if (ConcreteBindingContext.IsBusy) return;

        var glyphs = new char[] { '\uF05D', '\uF05C', '\uF045', '\uF042', '\uF84E' };

        var randomizer = new System.Random();

        ConcreteBindingContext.IsBusy = true;

        var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

        var objectivesAPIConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(httpClientHelperProvider);
        var objectiveIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ObjectiveIndicatorResultsAPIConnectClient(httpClientHelperProvider);
        var proyectionIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ProyectionIndicatorResultsAPIConnectClient(httpClientHelperProvider);
        var actionIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ActionIndicatorResultsAPIConnectClient(httpClientHelperProvider);

        objectivesAPIConnectClient.GetAsync().ContinueWith(async task =>
        {
            if (task.Exception != null || task.IsCanceled)
            {
                Dispatcher.Dispatch(() =>
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);

                    ConcreteBindingContext.IsBusy = false;
                });
            }
            else
            {
                try
                {
                    // TODO Take indicator for current year only and dismiss local filter specifications !!!
                    var objectiveIndicatorResults = (await objectiveIndicatorResultsAPIConnectClient.GetAsync()).OrderByDescending(model => model.At).ToArray();
                    var proyectionIndicatorResults = (await proyectionIndicatorResultsAPIConnectClient.GetAsync()).OrderByDescending(model => model.At).ToArray();
                    var actionIndicatorResults = (await actionIndicatorResultsAPIConnectClient.GetAsync()).OrderByDescending(model => model.At).ToArray();

                    var gaugeItems = new List<GaugeItem>();

                    var weightedPoints = new List<LiveChartsCore.Defaults.WeightedPoint>();

                    var monthlyObjectivesProgressChartXAxe = new Axis
                    {
                        LabelsRotation = -15,
                        Labels = [],
                        ForceStepToMin = true,
                        MinStep = 1
                    };

                    var objectiveOverviews = new List<Models.View.ObjectiveOverviewViewModel>();

                    var now = System.DateTime.Now;

                    int year = now.Year;
                    int dayOfYear = now.DayOfYear;

                    var globalObjectiveProgressSeries = new List<LineSeries<double?>>();

                    var indexer = 0;

                    foreach (var obj in task.Result)
                    {
                        var projectionCount = 0;
                        var actionCount = 0;
                        var indicatorCount = 0;

                        double?[] indicatorProgressionValues = new double?[dayOfYear];

                        var indicatorCompletitionLevels = new List<double>();

                        foreach (var objectiveIndicator in obj.Indicators)
                        {
                            indicatorCount++;

                            var targetObjectiveIndicatorResults = objectiveIndicatorResults.Where(_model => _model.IndicatorId == objectiveIndicator.Id && _model.At.Year == year).ToArray();

                            for (int index = 0; index < targetObjectiveIndicatorResults.Length; index++)
                            {
                                var target = targetObjectiveIndicatorResults[index];

                                var level = (target?.Amount ?? 0) / objectiveIndicator.Amount;

                                var dayLevel = indicatorProgressionValues[target.At.DayOfYear - 1];

                                if (dayLevel == null)
                                {
                                    indicatorProgressionValues[target.At.DayOfYear - 1] = level;
                                }

                                if (index == 0)
                                {
                                    indicatorCompletitionLevels.Add(level);
                                }
                            }
                        }

                        foreach (var proyection in obj.Proyections)
                        {
                            projectionCount++;

                            foreach (var proyectionIndicator in proyection.Indicators)
                            {
                                indicatorCount++;

                                var targetProyectionIndicatorResults = proyectionIndicatorResults.Where(_model => _model.IndicatorId == proyectionIndicator.Id && _model.At.Year == year).ToArray();

                                for (int index = 0; index < targetProyectionIndicatorResults.Length; index++)
                                {
                                    var target = targetProyectionIndicatorResults[index];

                                    var level = (target?.Amount ?? 0) / proyectionIndicator.Amount;

                                    var dayLevel = indicatorProgressionValues[target.At.DayOfYear - 1];

                                    if (dayLevel == null)
                                    {
                                        indicatorProgressionValues[target.At.DayOfYear - 1] = level;
                                    }

                                    if (index == 0)
                                    {
                                        indicatorCompletitionLevels.Add(level);
                                    }
                                }
                            }

                            foreach (var action in proyection.Actions)
                            {
                                actionCount++;

                                foreach (var actionIndicator in action.Indicators)
                                {
                                    indicatorCount++;

                                    var targetActionIndicatorResults = actionIndicatorResults.Where(_model => _model.IndicatorId == actionIndicator.Id && _model.At.Year == year).ToArray();

                                    for (int index = 0; index < targetActionIndicatorResults.Length; index++)
                                    {
                                        var target = targetActionIndicatorResults[index];

                                        var level = (target?.Amount ?? 0) / actionIndicator.Amount;

                                        var dayLevel = indicatorProgressionValues[target.At.DayOfYear - 1];

                                        if (dayLevel == null)
                                        {
                                            indicatorProgressionValues[target.At.DayOfYear - 1] = level;
                                        }

                                        if (index == 0)
                                        {
                                            indicatorCompletitionLevels.Add(level);
                                        }
                                    }
                                }
                            }
                        }

                        var totality = 0d;

                        indicatorCompletitionLevels.ForEach(x => totality += x);

                        var title = $"Obj. {obj.Index}";
                        var value = totality == 0 ? 0d : totality / indicatorCompletitionLevels.Count * 100;

                        monthlyObjectivesProgressChartXAxe.Labels.Add(title);

                        gaugeItems.Add(new GaugeItem(value, series =>
                        {
                            series.Name = title;
                            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Start;
                            series.DataLabelsFormatter = point => $"{point.Context.Series.Name} - {point.Coordinate.PrimaryValue:F2}%";
                            series.InnerRadius = 24;
                            series.RelativeOuterRadius = 4;
                            series.RelativeInnerRadius = 4;
                        }));

                        objectiveOverviews.Add(new Models.View.ObjectiveOverviewViewModel
                        {
                            Index = obj.Index,
                            Title = obj.Name,
                            ProyectionCount = projectionCount,
                            ActionCount = actionCount,
                            IndicatorCount = indicatorCount,
                            ProgressChart = new Models.View.ChartViewModel
                            {
                                Series = GaugeGenerator.BuildSolidGauge(new GaugeItem(value, series =>
                                {
                                    series.MaxRadialColumnWidth = 50;
                                    series.DataLabelsSize = 50;
                                })).ToArray()
                            },
                            ProgressGlyph = glyphs[randomizer.Next(0, 4)]
                        });

                        var colorStrokePaint = new SolidColorPaint
                        {
                            Color = COLORS[indexer++],
                            StrokeCap = SKStrokeCap.Square,
                            StrokeThickness = 8.0f
                        };

                        globalObjectiveProgressSeries.Add(new LineSeries<double?>
                        {
                            Fill = null,
                            Stroke = colorStrokePaint,
                            GeometryStroke = colorStrokePaint,
                            LineSmoothness = 1.0d,
                            Values = indicatorProgressionValues,
                            EnableNullSplitting = false,
                        });

                        for (int j = 0; j < 12; j++)
                        {
                            weightedPoints.Add(new LiveChartsCore.Defaults.WeightedPoint(obj.Index, j, randomizer.NextDouble() * 100));
                        }
                    }

                    gaugeItems.Add(new GaugeItem(GaugeItem.Background, series =>
                    {
                        series.InnerRadius = 20;
                    }));

                    var byObjectiveProyections = new List<int>();
                    var byObjectiveActions = new List<int>();
                    var byObjectiveIndicators = new List<int>();

                    foreach (var item in objectiveOverviews)
                    {
                        byObjectiveProyections.Add(item.ProyectionCount);
                        byObjectiveActions.Add(item.ActionCount);
                        byObjectiveIndicators.Add(item.IndicatorCount);
                    }

                    Dispatcher.Dispatch(() =>
                    {
                        ConcreteBindingContext.ObjectiveOverviews.Clear();

                        foreach (var item in objectiveOverviews)
                        {
                            ConcreteBindingContext.ObjectiveOverviews.Add(item);
                        }

                        ConcreteBindingContext.GlobalObjectivesStatusChart.Series = GaugeGenerator.BuildSolidGauge([.. gaugeItems]).ToArray();

                        ConcreteBindingContext.GlobalObjectivesProgressChart.Series = globalObjectiveProgressSeries.ToArray();

                        ConcreteBindingContext.MonthlyObjectivesProgressChart.XAxes = [monthlyObjectivesProgressChartXAxe];
                        ConcreteBindingContext.MonthlyObjectivesProgressChart.Series =
                        [
                            new HeatSeries<LiveChartsCore.Defaults.WeightedPoint> { Values = weightedPoints.ToArray() }
                        ];

                        ConcreteBindingContext.ProyectionCount = 21;
                        ConcreteBindingContext.ActionCount = ConcreteBindingContext.ProyectionCount * 4;
                        ConcreteBindingContext.IndicatorCount = ConcreteBindingContext.ActionCount * 2;

                        indexer = 0;

                        ConcreteBindingContext.ProyectionDistributionChart.Series = byObjectiveProyections.AsPieSeries((value, series) =>
                        {
                            series.Fill = new SolidColorPaint(COLORS[indexer]);
                            series.Name = monthlyObjectivesProgressChartXAxe.Labels[indexer++ % monthlyObjectivesProgressChartXAxe.Labels.Count];
                            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                            series.DataLabelsSize = 12;
                            series.DataLabelsPaint = new SolidColorPaint(new SkiaSharp.SKColor(30, 30, 30));
                            series.DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue} ({point.Coordinate.PrimaryValue / point.StackedValue.Total * 100:F0}%)";
                        }).ToArray();

                        indexer = 0;

                        ConcreteBindingContext.ActionDistributionChart.Series = byObjectiveActions.AsPieSeries((value, series) =>
                        {
                            series.Fill = new SolidColorPaint(COLORS[indexer]);
                            series.Name = monthlyObjectivesProgressChartXAxe.Labels[indexer++ % monthlyObjectivesProgressChartXAxe.Labels.Count];
                            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                            series.DataLabelsSize = 12;
                            series.DataLabelsPaint = new SolidColorPaint(new SkiaSharp.SKColor(30, 30, 30));
                            series.DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue} ({point.Coordinate.PrimaryValue / point.StackedValue.Total * 100:F0}%)";
                        }).ToArray();

                        indexer = 0;

                        ConcreteBindingContext.IndicatorDistributionChart.Series = byObjectiveIndicators.AsPieSeries((value, series) =>
                        {
                            series.Fill = new SolidColorPaint(COLORS[indexer]);
                            series.Name = monthlyObjectivesProgressChartXAxe.Labels[indexer++ % monthlyObjectivesProgressChartXAxe.Labels.Count];
                            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                            series.DataLabelsSize = 12;
                            series.DataLabelsPaint = new SolidColorPaint(new SkiaSharp.SKColor(30, 30, 30));
                            series.DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue} ({point.Coordinate.PrimaryValue / point.StackedValue.Total * 100:F0}%)";
                        }).ToArray();

                        ConcreteBindingContext.IsBusy = false;
                    });
                }
                catch (System.Exception exception)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);

                        ConcreteBindingContext.IsBusy = false;
                    });
                }
            }
        });
    }
}