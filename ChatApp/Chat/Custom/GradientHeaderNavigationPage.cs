using System;
using Xamarin.Forms;

namespace ChatApp.Custom
{
    public class GradientHeaderNavigationPage : NavigationPage
    {
        public GradientHeaderNavigationPage(Page page) : base(page)
        {
            BarTextColor = Color.White;
        }
    }
}