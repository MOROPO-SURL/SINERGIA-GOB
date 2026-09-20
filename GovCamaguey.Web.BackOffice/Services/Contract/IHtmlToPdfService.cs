namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Services.Contract;

public interface IHtmlToPdfService
{
    System.Threading.Tasks.Task<byte[]> GeneratePdfFromHtmlAsync(string htmlContent, Models.Options.PdfOptionsModel options = null);
}