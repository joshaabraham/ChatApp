using ChatApp.Helpers;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class ChatUserListPageViewModel : ViewModelBase
	{
        HubConnection hubConnection;
        private readonly IPageDialogService dialogService;
        public INavigationService navigationService;
        private ObservableCollection<ChatUser> _chatUserList = new ObservableCollection<ChatUser>();
        public ObservableCollection<ChatUser> ChatUserList
        {
            get
            {
                return _chatUserList;
            }
            set
            {
                _chatUserList = value;
                RaisePropertyChanged();
            }
        }
        public ICommand SelectCommand { get; set; }
        public Command DisconnectCommand { get; }

        public ObservableCollection<ChatMessage> ChatMessageList { get; set; }

        public ChatUserListPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;

            ChatUserList = new ObservableCollection<ChatUser>();
            SelectCommand = new DelegateCommand<ChatUser>(SelectExecute);
            DisconnectCommand = new Command(async () => await ChatHelper.Disconnect(Settings.ChatUserName));

            //LoadSampleData();
        }

        public async void LoadConnectedUsers()
        {
            try
            {
                hubConnection = ChatHelper.GetInstanse(Settings.ChatUserName);
                await ChatHelper.Connect(Settings.ChatUserName);

                await hubConnection.InvokeAsync("GetConnectedUsers");
                hubConnection.On<ObservableCollection<ChatUser>>("ChatUserList", (chatUserList) =>
                {
                    chatUserList = new ObservableCollection<ChatUser>(chatUserList);

                    ObservableCollection<ChatUser> formatedList = new ObservableCollection<ChatUser>();
                    foreach (ChatUser item in chatUserList)
                    {
                        formatedList.Add(GetFormatedChatUser(item.ConnectionId, item.ChatUsername));
                    }

                    App.OnlineChatUsers = this.ChatUserList = formatedList;
                });

                // Connectd chat user list
                await hubConnection.InvokeAsync("GetConnectedUsers");
                hubConnection.On<ObservableCollection<ChatUser>>("ChatUserList", (chatUserList) =>
                {
                    chatUserList = new ObservableCollection<ChatUser>(chatUserList);

                    ObservableCollection<ChatUser> formatedList = new ObservableCollection<ChatUser>();
                    foreach (ChatUser item in chatUserList)
                    {
                        formatedList.Add(GetFormatedChatUser(item.ConnectionId, item.ChatUsername));
                    }

                    App.OnlineChatUsers = this.ChatUserList = formatedList;
                });
            }
            catch { }

        }

        private ChatUser GetFormatedChatUser(string connectionId, string chatUsername)
        {
            ChatUser formatedChatUser = new ChatUser();

            string username = String.Empty;
            string name = String.Empty;
            string location = String.Empty;
            string photo = String.Empty;

            int count = 1;
            string[] datas = chatUsername.Split('_');
            foreach (string data in datas)
            {
                switch (count)
                {
                    case 1:
                        username = data;
                        break;
                    case 2:
                        name = data;
                        break;
                    case 3:
                        photo = data;
                        break;
                    case 4:
                        location = data;
                        break;
                    default:
                        break;
                }

                count++;
            } // End foreach

            formatedChatUser.ConnectionId = connectionId;
            formatedChatUser.ChatUsername = username;
            formatedChatUser.Name = name;
            formatedChatUser.Location = location;
            formatedChatUser.PhotoPath = photo;
            formatedChatUser.IsOnline = true;

            return formatedChatUser;
        }

        private async void SelectExecute(ChatUser chatUser)
        {
            await NavigationService.NavigateAsync("ChatPage", new NavigationParameters($"userId={chatUser.Username}&connectionId={chatUser.ConnectionId}&name={chatUser.Name}&photo={chatUser.PhotoPath}"));
        }
        
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public void LoadSampleData()
        {
            var chatUserList = new ObservableCollection<ChatUser>();
            chatUserList.Add(new ChatUser { Name = "Danieal Gonzalez", Location = "California, United States", IsOnline = true, PhotoPath = "male2.png" });
            chatUserList.Add(new ChatUser { Name = "Meaghan Sarah", Location = "Sacramento, California", IsOnline = true, PhotoPath = "female1.png" });
            chatUserList.Add(new ChatUser { Name = "William Russel", Location = "Houston, Texas", IsOnline = false, PhotoPath = "male3.png" });
            chatUserList.Add(new ChatUser { Name = "Jasper Margaret", Location = "Copenhagen, Denmark", IsOnline = true, PhotoPath = "male1.png" });
            chatUserList.Add(new ChatUser { Name = "Emma kidman", Location = "London, United Kingdom", IsOnline = false, PhotoPath = "female3.png" });
            chatUserList.Add(new ChatUser { Name = "Amelia Sophia", Location = "Northern Ireland", IsOnline = true, PhotoPath = "female2.png" });

            App.OnlineChatUsers = this.ChatUserList = chatUserList;
        }

    }
}
