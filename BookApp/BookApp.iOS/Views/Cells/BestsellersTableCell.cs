using BookApp.Core.Models;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using NMCT.Resto.iOS.Converters;
using System;
using UIKit;

namespace BookApp.iOS
{
    [MvxFromStoryboard(StoryboardName = "Main")]
    public partial class BestsellersTableCell : MvxTableViewCell
    {
        internal static readonly NSString Identifier = new NSString("BestsellersCell");

        public BestsellersTableCell (IntPtr handle) : base (handle)
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            MvxFluentBindingDescriptionSet<BestsellersTableCell, Book> set = new MvxFluentBindingDescriptionSet<BestsellersTableCell, Book>(this);
            set.Bind(lblTitle).To(res => res.volumeInfo.title);
            set.Bind(lblAuthor).To(res => res.volumeInfo.authors[0]);
            set.Bind(imgCover)
                .For(img => img.Image)
                .To(res => res.volumeInfo.imageLinks.smallThumbnail)
                .WithConversion<StringToImageConverter>();
            set.Apply();
        }
    }
}