using BookApp.Core.Models;
using BookApp.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.File;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.ViewModels
{
    public class BookDetailsViewModel : MvxViewModel
    {
        protected readonly IBookService _bookService;

        private Book _bookContent;

        public Book BookContent
        {
            get { return _bookContent; }
            set { _bookContent = value; RaisePropertyChanged(() => BookContent); }
        }

        private string _bookDescription;

        public string BookDescription
        {
            get { return _bookDescription; }
            set { _bookDescription = value; RaisePropertyChanged(() => BookDescription); }
        }

        private string _buttonLibraryText;

        public string ButtonLibraryText
        {
            get { return _buttonLibraryText; }
            set { _buttonLibraryText = value; RaisePropertyChanged(() => ButtonLibraryText); }
        }


        public async void Init(string bookId)
        {
            BookContent = await _bookService.GetBookById(bookId);
            makeDescriptionReadable();
            CheckInLibrary();
        }

        private void CheckInLibrary()
        {
            if (_bookService.CheckInLibrary(BookContent))
            {
                ButtonLibraryText = "Remove From Library";
            }
            else
            {
                ButtonLibraryText = "Add To Library";
            }
        }

        public MvxCommand SaveToLibraryCommand
        {
            get
            {
                return new MvxCommand(() => SaveToLibrary());
            }
        }

        private void SaveToLibrary()
        {
            _bookService.AddBookToLibrary(BookContent);
            CheckInLibrary();
        }

        public BookDetailsViewModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        private void makeDescriptionReadable()
        {
            if (BookContent.volumeInfo.description != null)
            {
                string des = BookContent.volumeInfo.description;

                while (des.Contains("<"))
                {
                    if (des.IndexOf("<") == 0)
                    {
                        des = des.Substring(des.IndexOf(">") + 1);
                    }
                    else
                    {
                        des = des.Substring(0, des.IndexOf("<")) + des.Substring(des.IndexOf(">") + 1);
                    }
                }

                BookDescription = des;
            }
            else BookDescription = "NO DESCRIPTION AVAILABLE";
        }
    }
}
