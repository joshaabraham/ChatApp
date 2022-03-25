using ChatApp.Helpers;
using ChatApp.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace ChatApp.Views.Chat
{
    public partial class ChatUserListPage : ContentPage
    {
        ChatUserListPageViewModel vm;
        
        public ChatUserListPage()
        {
            InitializeComponent();
            vm = BindingContext as ChatUserListPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadConnectedUsers();
        }
    }
}
