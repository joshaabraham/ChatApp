using Chat.ViewModels;
using Chat.Views.Account;
using ChatApp.Helpers;
using ChatApp.Models;
using ChatApp.ViewModels;
using ChatApp.Views.Account;
using ChatApp.Views.Chat;
using ChatApp.Views.Menu;
using Plugin.LocalNotification;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ChatApp
{
    public partial class App
    {
        // Global variables
        public static bool IsChatPageInitialize { get; set; }
        public static ObservableCollection<ChatUser> OnlineChatUsers { get; set; }

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }        

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
           
            NotificationCenter.Current.NotificationTapped += LoadPageFromNotification;

            NavigationService.NavigateAsync("MenuPage/NavigationPage/LoginPage");
        }        

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();                                        
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatPage, ChatPageViewModel>();            
            containerRegistry.RegisterForNavigation<ChatUserListPage, ChatUserListPageViewModel>();           
            containerRegistry.RegisterForNavigation<ForgotPasswordPage, ForgotPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
        }

        private async void LoadPageFromNotification(NotificationTappedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
            {
                return;
            }

            var serializer = new ObjectSerializer<List<string>>();
            var list = serializer.DeserializeObject(e.Data);
            if (list.Count != 5)
            {
                return;
            }
            if (list[0] != typeof(ChatPage).FullName)
            {
                return;
            }
            var connectionId = list[1];
            var userId = list[2];
            var photo = list[3];
            var name = list[4];

            await NavigationService.NavigateAsync("NavigationPage/ChatPage", new NavigationParameters($"userId={userId}&connectionId={connectionId}&name={name}&photo={photo}"));
        }
    }
}
