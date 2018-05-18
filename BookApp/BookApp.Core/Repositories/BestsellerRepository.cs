using BookApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.Repositories
{
    public class BestsellerRepository : BaseRepository, IBestsellerRepository
    {
        //http://api.nytimes.com/svc/books/v3/lists.json?api-key=95bcd6cc585a4bd7a5df68c1a573b9c5&list=combined-print-fiction
        private const string _BASEURL = "http://api.nytimes.com/svc/books/v3/lists.json?api-key=95bcd6cc585a4bd7a5df68c1a573b9c5&list=";

        public Task<RootObjectBestsellers> GetBestsellers(String listName)
        {
            string url = string.Format("{0}{1}", _BASEURL, listName);
            return GetAsync<RootObjectBestsellers>(url);
        }
    }
}
