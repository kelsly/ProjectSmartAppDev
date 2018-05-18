using System.Threading.Tasks;
using BookApp.Core.Models;

namespace BookApp.Core.Repositories
{
    public interface IBestsellerRepository
    {
        Task<RootObjectBestsellers> GetBestsellers(string listName);
    }
}