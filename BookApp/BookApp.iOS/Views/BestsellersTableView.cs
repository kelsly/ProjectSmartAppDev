using BookApp.Core.ViewModels;
using BookApp.iOS.TableViewSources;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using System;
using UIKit;

namespace BookApp.iOS
{
    [MvxFromStoryboard(StoryboardName = "Main")]
    public partial class BestsellersTableView : MvxTableViewController<BestsellersTableViewModel>
    {
        BestsellersTableViewSource _bestsellersTableViewSource;

        public BestsellersTableView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            _bestsellersTableViewSource = new BestsellersTableViewSource(this.TableView);

            base.ViewDidLoad();

            this.TableView.Source = _bestsellersTableViewSource;
            this.TableView.ReloadData();

            MvxFluentBindingDescriptionSet<BestsellersTableView, BestsellersTableViewModel> set = this.CreateBindingSet<BestsellersTableView, BestsellersTableViewModel>();
            set.Bind(_bestsellersTableViewSource).To(vm => vm.BestsellersList);
            set.Bind(_bestsellersTableViewSource)
                .For(src => src.SelectionChangedCommand)
                .To(vm => vm.ParentViewModel.NavigateToDetailCommand);
            set.Apply();
        }
    }
}