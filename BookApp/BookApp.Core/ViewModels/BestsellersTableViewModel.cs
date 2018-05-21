using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class BestsellersTableViewModel : MvxViewModel
    {
        protected readonly IBookService _bookService;

        private List<Book> _bestsellersList;

        public List<Book> BestsellersList
        {
            get { return _bestsellersList; }
            set { _bestsellersList = value; RaisePropertyChanged(() => BestsellersList); }
        }


        public BestsellersTableViewModel(IBookService bookService)
        {
            _bookService = bookService;

            loadData();
        }

        private async void loadData()
        {
            BestsellersList = await _bookService.GetBestsellers(true);
        }

        private BooksTabViewModel _parentViewModel;
        public BooksTabViewModel ParentViewModel
        {
            get { return _parentViewModel; }
            set
            {
                _parentViewModel = value;
                RaisePropertyChanged(() => ParentViewModel);
            }
        }

        public MvxCommand NavigateToWebCommand
        {
            get
            {
                return new MvxCommand(
                        () =>
                        {
                            try
                            {
                                ShowViewModel<MyWebViewModel>(new { });
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }

                        }
                    );
            }
        }
    }
}
