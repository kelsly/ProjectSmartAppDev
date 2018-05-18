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

        public BookService(IBookRepository bookRepository, IBestsellerRepository bestsellerRepository, IMvxFileStore fileStore)
        {
            _bookRepository = bookRepository;
            _bestsellerRepository = bestsellerRepository;
            _fileStore = fileStore;
        }

        public async Task<List<Book>> SearchBooks(string keyword, int index)
        {
            RootObjectBooks rb = await _bookRepository.SearchBooks(keyword, index);
            return rb.books;
        }

        public async Task<List<Book>> GetBestsellers(bool fiction)
        {
            List<Book> books = new List<Book>();
            RootObjectBestsellers rootbs;
            RootObjectBooks b;

            if (fiction) rootbs = await _bestsellerRepository.GetBestsellers("combined-print-fiction");
            else rootbs = await _bestsellerRepository.GetBestsellers("combined-print-nonfiction");

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
                        foreach (Book b in _myLibrary)
                        {

                            if (b.id == mybook.id)
                            {
                                _myLibrary.Remove(b);
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
