using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class BooksTabViewModel : MvxViewModel
    {
        private readonly IBookService _bookService;

        private readonly Lazy<BestsellersTableViewModel> _bestsellersTableViewModel;
        private readonly Lazy<SearchBooksTableViewModel> _searchBooksTableViewModel;

        public BestsellersTableViewModel BestsellersTableVM => _bestsellersTableViewModel.Value;
        public SearchBooksTableViewModel SearchBooksTableVM => _searchBooksTableViewModel.Value;

        public BooksTabViewModel(IBookService bookService)
        {
            _bookService = bookService;

            _bestsellersTableViewModel = new Lazy<BestsellersTableViewModel>(Mvx.IocConstruct<BestsellersTableViewModel>);
            _searchBooksTableViewModel = new Lazy<SearchBooksTableViewModel>(Mvx.IocConstruct<SearchBooksTableViewModel>);

            BestsellersTableVM.ParentViewModel = this;
            SearchBooksTableVM.ParentViewModel = this;
        }

        public MvxCommand<Book> NavigateToDetailCommand
        {
            get
            {
                return new MvxCommand<Book>(
                        selectedBook =>
                        {
                            try
                            {
                                ShowViewModel<BookDetailsViewModel>(new { bookId = selectedBook.id });
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
