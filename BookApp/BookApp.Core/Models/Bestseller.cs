using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.Models
{
    public class Isbn
    {
        public string isbn10 { get; set; }
        public string isbn13 { get; set; }
    }

    public class Bestseller
    {
        public List<Isbn> isbns { get; set; }
    }

    public class RootObjectBestsellers
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public int num_results { get; set; }
        public DateTime last_modified { get; set; }
        [JsonProperty("results")]
        public List<Bestseller> bestsellers { get; set; }
    }
}
