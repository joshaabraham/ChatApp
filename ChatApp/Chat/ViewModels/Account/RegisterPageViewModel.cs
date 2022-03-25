using ChatApp.Helpers;
using ChatApp.Models;
using ChatApp.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;

namespace Chat.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public INavigationService navigationService;
        private IPageDialogService dialogService;
        public User User { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            RegisterCommand = new DelegateCommand(RegisterCommandAction);
            LoginCommand = new DelegateCommand(LoginCommandAction);

            this.User = new User();
        }

        private async void RegisterCommandAction()
        {
            if (String.IsNullOrEmpty(User.Name))
            {
                await dialogService.DisplayAlertAsync("", "Enter your name!", "Ok");
                return;
            }
            else if(String.IsNullOrEmpty(User.Location))
            {
                await dialogService.DisplayAlertAsync("", "Enter your location!", "Ok");
                return;
            }
            else if (String.IsNullOrEmpty(User.Username))
            {
                await dialogService.DisplayAlertAsync("", "Enter your username!", "Ok");
                return;
            }
            else if (String.IsNullOrEmpty(User.Password))
            {
                await dialogService.DisplayAlertAsync("", "Enter your password!", "Ok");
                return;
            }

            // For simplisity, save the data into local repository
            Settings.Name = User.Name;
            Settings.Location = User.Location;
            Settings.Username = User.Username;
            Settings.Password = User.Password;

            await this.dialogService.DisplayAlertAsync("", "Account Created Successfully!", "Ok");

            await NavigationService.NavigateAsync("/MenuPage/NavigationPage/LoginPage");
        }

        private async void LoginCommandAction()
        {
            await NavigationService.NavigateAsync("/MenuPage/NavigationPage/LoginPage");
        }
    }
}
