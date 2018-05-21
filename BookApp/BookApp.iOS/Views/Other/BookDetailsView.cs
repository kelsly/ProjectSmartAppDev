using BookApp.Core.Models;
using BookApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using NMCT.Resto.iOS.Converters;
using System;
using UIKit;

namespace BookApp.iOS
{
    [MvxFromStoryboard(StoryboardName = "Main")]
    public partial class BookDetailsView : MvxViewController<BookDetailsViewModel>
    {
        

        public BookDetailsView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MvxFluentBindingDescriptionSet<BookDetailsView, BookDetailsViewModel> set = this.CreateBindingSet<BookDetailsView, BookDetailsViewModel>();
            set.Bind(lblTitle)
                .To(vm => vm.BookContent.volumeInfo.title);
            set.Bind(imgBook)
                .To(vm => vm.BookContent.volumeInfo.imageLinks.thumbnail)
                .WithConversion<StringToImageConverter>();
            set.Bind(lblDescription)
                .To(vm => vm.BookDescription);
            set.Bind(lblAuthor)
                .To(vm => vm.BookContent.volumeInfo.authors[0]);
            set.Bind(btnAddToLibrary)
                .For("Title")
                .To(vm => vm.ButtonLibraryText);
            set.Bind(btnAddToLibrary)
                .To(vm => vm.SaveToLibraryCommand);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            this.NavigationController.SetNavigationBarHidden(false, false);
            this.NavigationController.PreferredStatusBarStyle();
            base.ViewWillAppear(animated);
        }
    }
}