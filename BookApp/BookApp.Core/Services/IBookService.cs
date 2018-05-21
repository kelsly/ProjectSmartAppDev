using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.Models;

namespace BookApp.Core.Services
{
    public interface IBookService
    {
        Task<List<Book>> SearchBooks(string keyword, int index);
        Task<List<Book>> GetBestsellers();
        Task<Book> GetBookById(string bookId);
        List<Book> GetLibrary();
        bool CheckInLibrary(Book mybook);
        void AddBookToLibrary(Book mybook);
    }
}