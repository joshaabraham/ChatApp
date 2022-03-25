using ChatApp.Helpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class ForgotPasswordPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService dialogService;
        public INavigationService navigationService;
        public ICommand SendPasswordCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public string Username { get; set; }

        public ForgotPasswordPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;

            SendPasswordCommand = new DelegateCommand(SendPasswordCommandAction);
            LoginCommand = new DelegateCommand(LoginCommandAction);
        }

        private async void SendPasswordCommandAction()
        {
            await this.dialogService.DisplayAlertAsync("", "Your Password: " + Settings.Password, "Ok");
        }

        private async void LoginCommandAction()
        {
            await NavigationService.NavigateAsync("/MenuPage/NavigationPage/LoginPage");
        }
    }
}
