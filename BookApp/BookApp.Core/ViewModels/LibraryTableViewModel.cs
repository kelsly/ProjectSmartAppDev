using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class LibraryTableViewModel : MvxViewModel
    {
        protected readonly IBookService _bookService;

        private List<Book> _myLibrary;

        public List<Book> MyLibrary
        {
            get { return _myLibrary; }
            set {
                _myLibrary = value;
                RaisePropertyChanged(() => MyLibrary);
            }
        }

        public LibraryTableViewModel(IBookService bookService)
        {
            _bookService = bookService;

            loadData();
        }

        private void loadData()
        {
            MyLibrary = _bookService.GetLibrary();
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

        public IMvxCommand<int> RemoveBookCommand
        {
            get
            {
                return new MvxCommand<int>(RemoveBook);
            }
        }

        private void RemoveBook(int index)
        {
            Book b = MyLibrary[index];
            _bookService.AddBookToLibrary(b);
            loadData();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            loadData();
        }
    }
}
