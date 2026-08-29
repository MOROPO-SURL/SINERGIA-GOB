namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models
{
    public class UserPasswordChangeRequestModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string OldPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string NewPassword { get; set; }
    }
}
