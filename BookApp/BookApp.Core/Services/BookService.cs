using BookApp.Core.Models;
using BookApp.Core.Repositories;
using MvvmCross.Plugins.File;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBestsellerRepository _bestsellerRepository;
        private readonly IMvxFileStore _fileStore;
        private List<Book> _myLibrary;
        //private List<Book> _searchedBooks;
        private List<Book> _error = new List<Book>() { new Book() { volumeInfo = new VolumeInfo() { title = "Something went wrong", authors = new List<string>() { "Please come back later" }, imageLinks = new ImageLinks() { thumbnail = "error" } }, id = "error" } };

        public BookService(IBookRepository bookRepository, IBestsellerRepository bestsellerRepository, IMvxFileStore fileStore)
        {
            _bookRepository = bookRepository;
            _bestsellerRepository = bestsellerRepository;
            _fileStore = fileStore;
        }

        public async Task<List<Book>> SearchBooks(string keyword, int page)
        {
            try
            {
                //RootObjectBooks rb = await _bookRepository.SearchBooks(keyword, page*20);

                //if (page == 0) _searchedBooks = rb.books;
                //else _searchedBooks.AddRange(rb.books);

                //return _searchedBooks;
                List<Book> b = new List<Book>();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    for (int i = 0; i <= page; i++)
                    {
                        RootObjectBooks rb = await _bookRepository.SearchBooks(keyword, i * 20);
                        b.AddRange(rb.books);
                    }
                }

                return b;
            }
            catch (Exception e)
            {
                return _error;
            }
        }

        public async Task<List<Book>> GetBestsellers()
        {
            try
            {
                List<Book> books = new List<Book>();
                RootObjectBestsellers rootbs;
                RootObjectBooks b;

                rootbs = await _bestsellerRepository.GetBestsellers("hardcover-fiction");

                foreach (Bestseller bs in rootbs.bestsellers)
                {
                    for (int i = 0; i < bs.isbns.Count; i++)
                    {
                        b = await _bookRepository.GetBookByISBN(bs.isbns[i].isbn13);
                        if (b.totalItems == 0)
                        {
                            b = await _bookRepository.GetBookByISBN(bs.isbns[i].isbn10);
                        }

                        if (b.totalItems == 1)
                        {
                            books.Add(b.books[0]);
                            i = bs.isbns.Count;
                        }
                    }
                }

                return books;
            }
            catch (Exception e)
            {
                return _error;
            }
        }

        public async Task<Book> GetBookById(string bookId)
        {
            return await _bookRepository.GetBookByID(bookId);
        }

        public void AddBookToLibrary(Book mybook)
        {
            try
            {
                FillLibrary();

                if (_myLibrary == null)
                {
                    _myLibrary = new List<Book>();
                    _myLibrary.Add(mybook);
                }
                else
                {
                    if (CheckInLibrary(mybook))
                    {
                        _myLibrary.Remove(mybook);
                        if (CheckInLibrary(mybook))
                        {
                            List<Book> l = _myLibrary;
                            _myLibrary = new List<Book>();
                            foreach(Book b in l)
                            {
                                if (b.id != mybook.id)
                                {
                                    _myLibrary.Add(b);
                                }
                            }
                        }
                    }
                    else
                    {
                        _myLibrary.Add(mybook);
                    }
                }

                if (!_fileStore.FolderExists("MyBookApp"))
                    _fileStore.EnsureFolderExists("MyBookApp");
                _fileStore.WriteFile("MyBookApp/Library", JsonConvert.SerializeObject(_myLibrary));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckInLibrary(Book mybook)
        {
            FillLibrary();
            bool inLibrary = false;

            if (_myLibrary != null)
            {               
                foreach (Book b in _myLibrary)
                {
                    if (b.id == mybook.id)
                    {
                        inLibrary = true;
                    }
                }
            }

            return inLibrary;
        }

        public List<Book> GetLibrary()
        {
            FillLibrary();
                        
            return _myLibrary;
        }

        private void FillLibrary()
        {
            if (_myLibrary == null)
            {
                string contents = string.Empty;
                _fileStore.TryReadTextFile("MyBookApp/Library", out contents);

                if (contents != null)
                {
                    _myLibrary = new List<Book>();
                    _myLibrary = JsonConvert.DeserializeObject<List<Book>>(contents);
                }
            }
        }
    }
}
