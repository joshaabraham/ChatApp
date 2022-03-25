using ChatApp.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.ObjectModel;

namespace ChatApp.Helpers
{
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static void ClearAll()
        {
            AppSettings.Clear();
        }

        public static string Username
        {
            get => AppSettings.GetValueOrDefault(nameof(Username), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Username), value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

        public static bool IsLoggedIn
        {
            get { return AppSettings.GetValueOrDefault(nameof(IsLoggedIn), false); }
            set { AppSettings.AddOrUpdateValue(nameof(IsLoggedIn), value); }
        }

        public static string ChatUserName
        {
            get { return AppSettings.GetValueOrDefault(nameof(ChatUserName), string.Empty); }
            set { AppSettings.AddOrUpdateValue(nameof(ChatUserName), value); }
        }

        public static string Name
        {
            get => AppSettings.GetValueOrDefault(nameof(Name), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Name), value);
        }

        public static string Location
        {
            get => AppSettings.GetValueOrDefault(nameof(Location), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Location), value);
        }

        public static ObservableCollection<ChatMessage> GetMessageHistory(string key)
        {
            ObservableCollection<ChatMessage> list;
            string value = AppSettings.GetValueOrDefault(key, string.Empty);

            if (string.IsNullOrEmpty(value))
            {
                list = new ObservableCollection<ChatMessage>();
            }
            else
            {
                list = JsonConvert.DeserializeObject<ObservableCollection<ChatMessage>>(value);
            }

            return list;
        }

        public static void SetMessageHistory(string key, ObservableCollection<ChatMessage> value)
        {
            string msgList = JsonConvert.SerializeObject(value);
            AppSettings.AddOrUpdateValue(key, msgList);
        }

    }
}
