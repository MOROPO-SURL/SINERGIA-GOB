namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models
{
    public class AuthenticatedSessionModel
    {
        public string Token { get; set; }

        public string Type { get; set; }

        public int ExpiresInSecond { get; set; }

        public Display.UserDisplayModel User { get; set; }
    }
}
