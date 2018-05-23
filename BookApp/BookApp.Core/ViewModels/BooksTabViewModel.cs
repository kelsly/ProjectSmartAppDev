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
        private readonly Lazy<LibraryTableViewModel> _libraryTableViewModel;

        public BestsellersTableViewModel BestsellersTableVM => _bestsellersTableViewModel.Value;
        public SearchBooksTableViewModel SearchBooksTableVM => _searchBooksTableViewModel.Value;
        public LibraryTableViewModel LibraryTableVM => _libraryTableViewModel.Value;

        public BooksTabViewModel(IBookService bookService)
        {
            try
            {
                _bookService = bookService;

                _bestsellersTableViewModel = new Lazy<BestsellersTableViewModel>(Mvx.IocConstruct<BestsellersTableViewModel>);
                _searchBooksTableViewModel = new Lazy<SearchBooksTableViewModel>(Mvx.IocConstruct<SearchBooksTableViewModel>);
                _libraryTableViewModel = new Lazy<LibraryTableViewModel>(Mvx.IocConstruct<LibraryTableViewModel>);

                BestsellersTableVM.ParentViewModel = this;
                SearchBooksTableVM.ParentViewModel = this;
                LibraryTableVM.ParentViewModel = this;
            } catch (Exception e)
            {
                throw e;
            }
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
                                if (selectedBook.id != "error") ShowViewModel<BookDetailsViewModel>(new { bookId = selectedBook.id });
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
