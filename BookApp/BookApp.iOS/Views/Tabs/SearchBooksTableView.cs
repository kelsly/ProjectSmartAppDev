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
    public partial class SearchBooksTableView : MvxViewController<SearchBooksTableViewModel>
    {
        private BestsellersTableViewSource _tableViewSource;

        public SearchBooksTableView()
        {

        }
        

        public SearchBooksTableView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            _tableViewSource = new BestsellersTableViewSource(MyTableView);

            base.ViewDidLoad();

            MyTableView.Source = _tableViewSource;
            MyTableView.ReloadData();

            MvxFluentBindingDescriptionSet<SearchBooksTableView, SearchBooksTableViewModel> set = this.CreateBindingSet<SearchBooksTableView, SearchBooksTableViewModel>();
            set.Bind(txtSearch)
                .To(vm => vm.SearchValue);
            set.Bind(_tableViewSource)
                .To(vm => vm.SearchResult);
            set.Bind(_tableViewSource)
                .For(src => src.SelectionChangedCommand)
                .To(vm => vm.ParentViewModel.NavigateToDetailCommand);
            set.Bind(btnSearch)
                .To(vm => vm.SearchBooksCommand);
            set.Apply();

        }

        public override void ViewWillAppear(bool animated)
        {
            this.NavigationController.SetNavigationBarHidden(true, false);
            base.ViewWillAppear(animated);
        }
    }
}