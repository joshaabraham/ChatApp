using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ChatApp;
using FFImageLoading.Forms.Platform;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.LocalNotification;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;

namespace BDMarriage.Droid
{
    [Activity(Label = "ChatApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Chat.Droid.Resource.Style.MainTheme);

            TabLayoutResource = Chat.Droid.Resource.Layout.Tabbar;
            ToolbarResource = Chat.Droid.Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, bundle);
            CachedImageRenderer.Init(true);            
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            ImageCircleRenderer.Init();            

            // Must create a Notification Channel when API >= 26
            // you can created multiple Notification Channels with different names.
            NotificationCenter.CreateNotificationChannel(new Plugin.LocalNotification.Platform.Droid.NotificationChannelRequest
            {
                Sound = Chat.Droid.Resource.Raw.good_things_happen.ToString()
            });

            LoadApplication(new App(new AndroidInitializer()));

            // Change the top bar color
            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(120, 42, 177));

            NotificationCenter.NotifyNotificationTapped(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

