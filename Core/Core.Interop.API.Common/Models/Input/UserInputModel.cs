namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Input
{
    public class UserInputModel
    {
        public string Id { get; set; }

        public bool Enabled { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public string NewPassword { get; set; }
    }
}
