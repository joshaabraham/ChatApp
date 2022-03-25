using ChatApp.Models;
using ChatApp.ViewModels;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp.Views.Chat
{
    public partial class ChatPage : ContentPage
    {
        ChatPageViewModel vm;
        
        public ChatPage()
        {
            InitializeComponent();
            vm = BindingContext as ChatPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ChatPageViewModel>(this, "SCROLL_BOTTOM", (obj) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Task.Delay(100);
                    if (vm.ChatMessageList.Count > 0)
                    {
                        lvChat.ScrollTo(vm.ChatMessageList[vm.ChatMessageList.Count - 1], ScrollToPosition.End, false);
                    }
                });
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ChatPageViewModel>(this, "SCROLL_BOTTOM");
            base.OnDisappearing();
        }
    }
}
