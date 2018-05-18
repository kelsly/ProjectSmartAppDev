using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class SearchBooksTableViewModel: MvxViewModel
    {
        private int index;

        private string _searchvalue;

        public string SearchValue
        {
            get { return _searchvalue; }
            set
            {
                _searchvalue = value;
                RaisePropertyChanged(() => SearchValue);
                //SearchBooks();
            }
        }

        private List<Book> _searchResult;

        public List<Book> SearchResult
        {
            get { return _searchResult; }
            set { _searchResult = value; RaisePropertyChanged(() => SearchResult); }
        }

        protected readonly IBookService _bookService;

        public SearchBooksTableViewModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        private async void SearchBooks()
        {
            index = 0;
            if (SearchValue.Length > 2)
            {
                SearchResult = await _bookService.SearchBooks(SearchValue, index);
            } else
            {
                SearchResult = null;
            }
                        
        }

        public MvxCommand SearchBooksCommand
        {
            get { return new MvxCommand(() => SearchBooks()); }
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
    }
}
