using BookApp.Core.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using System;
using UIKit;

namespace BookApp.iOS
{
    public partial class BooksTabView : MvxTabBarViewController<BooksTabViewModel>
    {
        private bool _constructed;

        public BooksTabView()
        {
            _constructed = true;
            ViewDidLoad();
        }

        public override void ViewDidLoad()
        {
            if (!_constructed) return;

            base.ViewDidLoad();

            CreateTabs();
        }

        private void CreateTabs()
        {
            var viewControllers = new UIViewController[]
            {
                CreateSingleTab("Bestsellers", ViewModel.BestsellersTableVM),
                CreateSingleTab("Search", ViewModel.SearchBooksTableVM)
            };

            ViewControllers = viewControllers;
            SelectedViewController = ViewControllers[0];
            NavigationItem.Title = SelectedViewController.Title;

            ViewControllerSelected += (o, e) =>
            {
                NavigationItem.Title = TabBar.SelectedItem.Title;
            };
        }

        private UIViewController CreateSingleTab(string tabName, MvxViewModel tabViewModel)
        {
            var viewController =
                this.CreateViewControllerFor(tabViewModel) as UIViewController;
            tabViewModel.Start();
            UIImage Icon = UIImage.FromBundle(tabName + ".png");
            Icon = ResizeUIImage(Icon, 25, 25);
            viewController.Title = tabName;
            viewController.TabBarItem = new UITabBarItem() { Title = tabName, Image = Icon };

            return viewController;
        }

        public UIImage ResizeUIImage(UIImage sourceImage, float widthToScale, float heightToScale)
        {
            var sourceSize = sourceImage.Size;
            var maxResizeFactor = Math.Max(widthToScale / sourceSize.Width, heightToScale / sourceSize.Height);
            if (maxResizeFactor > 1) return sourceImage;
            var width = maxResizeFactor * sourceSize.Width;
            var height = maxResizeFactor * sourceSize.Height;
            UIGraphics.BeginImageContext(new CGSize(width, height));
            sourceImage.Draw(new CGRect(0, 0, width, height));
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
    }
}