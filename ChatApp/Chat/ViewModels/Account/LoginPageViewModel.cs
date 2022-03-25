using Acr.UserDialogs;
using ChatApp.Helpers;
using ChatApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
	{
        private readonly IPageDialogService dialogService;
        public INavigationService navigationService;        
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }

        public User User { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            
            RegisterCommand = new DelegateCommand(RegisterCommandAction);
            LoginCommand = new DelegateCommand(LoginCommandAction);
            ForgotPasswordCommand = new DelegateCommand(ForgotPasswordCommandAction);

            this.User = new User();            
        }

        private async void ForgotPasswordCommandAction()
        {
            await NavigationService.NavigateAsync("/MenuPage/NavigationPage/ForgotPasswordPage");
        }

        private async void LoginCommandAction()
        {
            // Validation
            if (String.IsNullOrEmpty(this.User.Username))
            {
                await dialogService.DisplayAlertAsync("", "Enter your username!", "OK");
                return;
            }
            else if (String.IsNullOrEmpty(this.User.Password))
            {
                await dialogService.DisplayAlertAsync("", "Enter your password!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Please Wait..."))
            {
                if(Settings.Username == this.User.Username && Settings.Password == this.User.Password)
                {
                    // Profile image
                    string imageName = "photo.png";

                    // Connect to chat hub
                    string chatUsername = this.User.Username + "_" + Settings.Name + "_" + imageName + "_" + Settings.Location;
                    await ChatHelper.Connect(chatUsername);

                    // Navigate to chat user list page
                    await NavigationService.NavigateAsync("/MenuPage/NavigationPage/ChatUserListPage");
                }
                else
                {
                    await dialogService.DisplayAlertAsync("", "Invalid username or password!", "OK");
                }
            }
        }

        private void RegisterCommandAction()
        {
            NavigationService.NavigateAsync("/MenuPage/NavigationPage/RegisterPage");
        }
    }
}
