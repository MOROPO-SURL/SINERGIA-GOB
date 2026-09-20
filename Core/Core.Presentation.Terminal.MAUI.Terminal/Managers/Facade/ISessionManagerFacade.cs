namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Managers.Facade
{
    public interface ISessionManagerFacade
	{
		public Interop.API.Common.Models.AuthenticatedSessionModel CurrentSession { get; }

        public System.Threading.Tasks.Task SignInAsync(string login, string password);

        public System.Threading.Tasks.Task ChangePasswordAsync(string oldPassword, string newPassword);

        public System.Threading.Tasks.Task SignOutAsync();
    }
}
