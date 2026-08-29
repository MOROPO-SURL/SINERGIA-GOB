namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.Options;

public class PdfOptionsModel
{
    public string PageSize { get; set; } = "A4";

    public Enumerations.Orientation Orientation { get; set; } = Enumerations.Orientation.Portrait;

    public MarginsModel Margins { get; set; } = new MarginsModel();

    public System.Collections.Generic.Dictionary<string, string> CustomArguments { get; set; } = [];

    public bool EnableJavaScript { get; set; } = false;

    public int? JavaScriptDelay { get; set; } = 500; // ms

    public bool EnableExternalLinks { get; set; } = false;

    public bool EnableInternalLinks { get; set; } = false;

    public string HeaderHtml { get; set; }

    public string FooterHtml { get; set; }

    public string HeaderUrl { get; set; }

    public string FooterUrl { get; set; }

    public bool PrintBackground { get; set; } = false;

    public string UserStyleSheet { get; set; }

    public string Cookies { get; set; }
}
