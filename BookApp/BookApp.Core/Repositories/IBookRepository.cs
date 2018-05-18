using System.Threading.Tasks;
using BookApp.Core.Models;

namespace BookApp.Core.Repositories
{
    public interface IBookRepository
    {
        Task<RootObjectBooks> SearchBooks(string keyword, int index);
        Task<RootObjectBooks> GetBookByISBN(string isbn);
        Task<Book> GetBookByID(string id);
    }
}