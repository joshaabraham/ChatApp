using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Flex;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Prism;
using Prism.Ioc;
using UIKit;
using ChatApp;

namespace BDMarriage.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new iOSInitializer()));
            Plugin.InputKit.Platforms.iOS.Config.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            ImageCircleRenderer.Init();
            FlexButton.Init();
            return base.FinishedLaunching(app, options);
        }

        public override void WillEnterForeground(UIApplication uiApplication)
        {
            Plugin.LocalNotification.NotificationCenter.ResetApplicationIconBadgeNumber(uiApplication);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
