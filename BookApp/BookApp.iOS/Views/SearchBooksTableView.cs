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
        //private UISearchBar searchBar;

        public SearchBooksTableView()
        {

        }
        

        public SearchBooksTableView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            _tableViewSource = new BestsellersTableViewSource(MyTableView);

            //searchBar = new UISearchBar();
            //searchBar.SizeToFit();
            //searchBar.SearchBarStyle = UISearchBarStyle.Prominent;

            base.ViewDidLoad();

            MyTableView.Source = _tableViewSource;
            //this.TableView.TableHeaderView = searchBar;
            MyTableView.ReloadData();

            MvxFluentBindingDescriptionSet<SearchBooksTableView, SearchBooksTableViewModel> set = this.CreateBindingSet<SearchBooksTableView, SearchBooksTableViewModel>();
            set.Bind(txtSearch)
                //.For(s => s.Text)
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
    }
}