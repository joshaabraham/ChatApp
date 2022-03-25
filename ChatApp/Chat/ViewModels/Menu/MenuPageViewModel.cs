using ChatApp.Helpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }

        public string ProfilePhoto { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public MenuPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            LoginCommand = new DelegateCommand(LoginCommandAction);

            this.Name = "Your Name Goes Here";
            this.Location = "Your Location Goes Here";
        }

        private async void LoginCommandAction()
        {
            await NavigationService.NavigateAsync("/MenuPage/NavigationPage/LoginPage");
        }
    }
}
