namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel target)
        {
            if (target.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-account");

            if (target.Controller == "roles") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-account-group");

            if (target.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-foot-print");

            if (target.Controller == "apikeys") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-key");

            if (target.Controller == "countries") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-flag");

            if (target.Controller == "states") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-sign-direction");

            if (target.Controller == "municipalities") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-city");

            if (target.Controller == "regions") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-earth-box");

            if (target.Controller == "unitmeasurements") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-scale");

            if (target.Controller == "guidelines") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-marker-check");

            if (target.Controller == "persons") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-account-box");

            if (target.Controller == "organizations") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-office-building");

            if (target.Controller == "objectives") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-check-decagram");

            if (target.Controller == "proyections") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-checkbox-marked");

            if (target.Controller == "actions") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-check-bold");

            if (target.Controller == "indicators") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-speedometer");

            if (target.Controller == "results") return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-chart-areaspline");

            return new Microsoft.AspNetCore.Html.HtmlString("mdi mdi-toy-brick-outline");
        }
    }
}
