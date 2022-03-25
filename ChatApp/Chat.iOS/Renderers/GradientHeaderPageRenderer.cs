using BasicApp.iOS.Renderers;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(GradientHeaderPageRenderer))]
namespace BasicApp.iOS.Renderers
{
    public class GradientHeaderPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetGradientBackground();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
        }

        private void SetGradientBackground()
        {
            if (NavigationController != null)
            {
                Color startColor = Color.FromHex("#3B5A9A");
                Color endColor = Color.FromHex("#7843AB");                

                var gradientLayer = new CAGradientLayer();
                gradientLayer.Bounds = NavigationController.NavigationBar.Bounds;
                gradientLayer.Colors = new CGColor[] { startColor.ToCGColor(), endColor.ToCGColor() };
                gradientLayer.StartPoint = new CGPoint(0.0, 0.5);
                gradientLayer.EndPoint = new CGPoint(1.0, 0.5);

                UIGraphics.BeginImageContext(gradientLayer.Bounds.Size);
                gradientLayer.RenderInContext(UIGraphics.GetCurrentContext());
                UIImage image = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();

                NavigationController.NavigationBar.SetBackgroundImage(image, UIBarMetrics.Default);
            }
        }        
    }
}