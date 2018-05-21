using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class SearchBooksTableViewModel: MvxViewModel
    {
        private int page;

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

        private bool _hasTwentyResults;

        public bool HasTwentyResults
        {
            get { return _hasTwentyResults; }
            set { _hasTwentyResults = value; RaisePropertyChanged(() => HasTwentyResults); }
        }


        protected readonly IBookService _bookService;

        public SearchBooksTableViewModel(IBookService bookService)
        {
            _bookService = bookService;
            HasTwentyResults = false;
        }

        private async void SearchBooks()
        {
            page = 0;
            SearchResult = await _bookService.SearchBooks(SearchValue, page);
            checkHasTwentyResults(SearchResult);          
        }

        private void checkHasTwentyResults(List<Book> books)
        {
            if (books.Count < ((page+1)*20)) HasTwentyResults = false;
            else HasTwentyResults = true;
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

        public MvxCommand LoadMoreCommand
        {
            get { return new MvxCommand(() => LoadMore()); }
        }

        private async void LoadMore()
        {
            page += 1;
            SearchResult = await _bookService.SearchBooks(SearchValue, page);
            checkHasTwentyResults(SearchResult);
        }
    }
}
