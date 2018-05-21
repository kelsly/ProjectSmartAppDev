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
    public partial class LibraryTableView : MvxTableViewController<LibraryTableViewModel>
    {
        BestsellersTableViewSource _tableViewSource;

        public LibraryTableView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            _tableViewSource = new BestsellersTableViewSource(MyTableView);

            base.ViewDidLoad();

            this.TableView.Source = _tableViewSource;
            this.TableView.ReloadData();

            MvxFluentBindingDescriptionSet<LibraryTableView, LibraryTableViewModel> set = new MvxFluentBindingDescriptionSet<LibraryTableView, LibraryTableViewModel>(this);
            set.Bind(_tableViewSource).To(vm => vm.MyLibrary);
            set.Bind(_tableViewSource)
                .For(src => src.SelectionChangedCommand)
                .To(vm => vm.ParentViewModel.NavigateToDetailCommand);
            //set.Bind(_tableViewSource)
            //    .For(s => s.RemoveRowCommand)
            //    .To(vm => vm.RemoveBookCommand);
            set.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            this.NavigationController.SetNavigationBarHidden(true, false);
            base.ViewWillAppear(animated);
        }
    }
}