namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models
{
    public class AuthenticationIntentModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string User { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; }
    }
}
