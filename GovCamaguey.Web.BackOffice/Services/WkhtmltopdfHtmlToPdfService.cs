using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Services.Contract;
using System.IO;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Services;

internal class WkhtmltopdfHtmlToPdfService : IHtmlToPdfService
{
    private readonly IConfiguration configuration;
    private readonly ILogger<WkhtmltopdfHtmlToPdfService> logger;

    public WkhtmltopdfHtmlToPdfService(IConfiguration configuration, ILogger<WkhtmltopdfHtmlToPdfService> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public async System.Threading.Tasks.Task<byte[]> GeneratePdfFromHtmlAsync(string htmlContent, Models.Options.PdfOptionsModel options = null)
    {
        options ??= new Models.Options.PdfOptionsModel();

        var tempDirectoryPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData), "Decisor", "PdfTemp");

        if (!Directory.Exists(tempDirectoryPath))
        {
            Directory.CreateDirectory(tempDirectoryPath);
        }

        // Crear archivo temporal para el HTML
        string tempHtmlFile = Path.Combine(tempDirectoryPath, $"source_{System.DateTime.Now.Ticks}.html");
        string tempPdfFile = Path.ChangeExtension(tempHtmlFile, ".pdf");

        try
        {
            // Guardar HTML en archivo temporal
            await File.WriteAllTextAsync(tempHtmlFile, htmlContent, System.Text.Encoding.UTF8);

            // Generar PDF
            string arguments = BuildArguments(tempHtmlFile, tempPdfFile, options);

            await ExecuteWkHtmlToPdf(arguments);

            // Leer y retornar PDF
            return await File.ReadAllBytesAsync(tempPdfFile);
        }
        finally
        {
            // Limpiar archivos temporales
            SafeDeleteFile(tempHtmlFile);
            SafeDeleteFile(tempPdfFile);
        }
    }

    private static string BuildArguments(string input, string output, Models.Options.PdfOptionsModel options)
    {
        var args = new System.Text.StringBuilder();

        // Opciones básicas
        args.Append($"--page-size {options.PageSize} ");
        args.Append($"--orientation {options.Orientation} ");
        args.Append($"--margin-top {options.Margins.Top}mm ");
        args.Append($"--margin-right {options.Margins.Right}mm ");
        args.Append($"--margin-bottom {options.Margins.Bottom}mm ");
        args.Append($"--margin-left {options.Margins.Left}mm ");

        // JavaScript
        if (options.EnableJavaScript)
        {
            args.Append("--enable-javascript ");

            if (options.JavaScriptDelay.HasValue)
            {
                args.Append($"--javascript-delay {options.JavaScriptDelay} ");
            }
        }
        else
        {
            args.Append("--disable-javascript ");
        }

        // Enlaces
        args.Append(options.EnableExternalLinks ? "--enable-external-links " : "--disable-external-links ");
        args.Append(options.EnableInternalLinks ? "--enable-internal-links " : "--disable-internal-links ");

        // Fondo
        args.Append(options.PrintBackground ? "--print-media-type " : "--no-background ");

        // Header/Footer
        if (!string.IsNullOrEmpty(options.HeaderHtml))
        {
            string headerFile = Path.GetTempFileName() + ".html";

            File.WriteAllText(headerFile, options.HeaderHtml);

            args.Append($"--header-html \"{headerFile}\" ");
        }
        else if (!string.IsNullOrEmpty(options.HeaderUrl))
        {
            args.Append($"--header-html \"{options.HeaderUrl}\" ");
        }

        if (!string.IsNullOrEmpty(options.FooterHtml))
        {
            string footerFile = Path.GetTempFileName() + ".html";

            File.WriteAllText(footerFile, options.FooterHtml);

            args.Append($"--footer-html \"{footerFile}\" ");
        }
        else if (!string.IsNullOrEmpty(options.FooterUrl))
        {
            args.Append($"--footer-html \"{options.FooterUrl}\" ");
        }

        // Cookies
        if (!string.IsNullOrEmpty(options.Cookies))
        {
            args.Append($"--cookie \"{options.Cookies}\" ");
        }

        // Estilos personalizados
        if (!string.IsNullOrEmpty(options.UserStyleSheet))
        {
            string styleFile = Path.GetTempFileName() + ".css";

            File.WriteAllText(styleFile, options.UserStyleSheet);

            args.Append($"--user-style-sheet \"{styleFile}\" ");
        }

        // Argumentos personalizados
        foreach (var arg in options.CustomArguments)
        {
            args.Append($"--{arg.Key} {arg.Value} ");
        }

        // Quiet mode
        args.Append("--quiet ");

        args.Append("--enable-local-file-access ");

        //args.Append("--disable-smart-shrinking ");

        // Manejar errores de carga (IMPORTANTE)
        //args.Append("--load-error-handling ignore ");
        //args.Append("--load-media-error-handling ignore ");

        // Deshabilitar plugins que pueden causar problemas
        //args.Append("--disable-plugins ");

        // Input y output
        args.Append($"\"{input}\" \"{output}\"");

        return args.ToString();
    }

    private async System.Threading.Tasks.Task ExecuteWkHtmlToPdf(string arguments)
    {
        try
        {
            var processStartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = configuration.GetValue<string>("wkhtmltopdf"),
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            };

            using var process = new System.Diagnostics.Process { StartInfo = processStartInfo };

            process.Start();

            // Leer salida para debugging
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                logger.LogError("wkhtmltopdf error: {Error}, Output: {Output}", error, output);

                throw new System.Exception($"Error generando PDF: {error}");
            }

            logger.LogDebug("wkhtmltopdf ejecutado exitosamente");
        }
        catch (System.Exception ex)
        {
            logger.LogError(ex, "Error ejecutando wkhtmltopdf");

            throw;
        }
    }

    private void SafeDeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        }
        catch (System.Exception ex)
        {
            logger.LogWarning(ex, "No se pudo eliminar el archivo temporal: {FilePath}", filePath);
        }
    }
}
