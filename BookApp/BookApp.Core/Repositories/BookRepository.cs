using BookApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Core.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        private const string _BASEURL = "https://www.googleapis.com/books/v1/volumes";
        private const string _APIKEY = "?key=AIzaSyC3ARaY0Yg5BlDiOAwPjA3SjMtkq7xhuss";
        private const string _PROJECTION = "&projection=lite";

        public Task<RootObjectBooks> GetBookByISBN(string isbn)
        {
            //https://www.googleapis.com/books/v1/volumes?q=isbn:9781538728741
            string url = String.Format("{0}{1}{2}{3}", _BASEURL, _APIKEY, "&q=isbn:", isbn, _PROJECTION);
            return GetAsync<RootObjectBooks>(url);
        }

        public Task<RootObjectBooks> SearchBooks(string keyword, int index)
        {
            string url = String.Format("{0}{1}{2}{3}{4}{5}{6}", _BASEURL, _APIKEY, "&q=", keyword, _PROJECTION, "&printType=books&maxResults=20&startIndex=", index);
            return GetAsync<RootObjectBooks>(url);
        }

        public Task<Book> GetBookByID(string id)
        {
            string url = String.Format("{0}{1}{2}{3}{4}", _BASEURL, "/", id, _APIKEY, _PROJECTION);
            return GetAsync<Book>(url);
        }
    }
}
