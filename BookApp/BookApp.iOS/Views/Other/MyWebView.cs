using BookApp.Core.ViewModels;
using Foundation;
using MvvmCross.iOS.Views;
using System;
using UIKit;

namespace BookApp.iOS
{
    public partial class MyWebView : MvxViewController<MyWebViewModel>
    {
        public MyWebView (IntPtr handle) : base (handle)
        {
        }

        public MyWebView()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIWebView webView = new UIWebView(View.Bounds);
            View.AddSubview(webView);
            webView.LoadRequest(new NSUrlRequest(new NSUrl("https://developer.nytimes.com/")));
        }

        public override void ViewWillAppear(bool animated)
        {
            this.NavigationController.SetNavigationBarHidden(false, false);
            this.NavigationController.PreferredStatusBarStyle();
            base.ViewWillAppear(animated);
        }
    }
}