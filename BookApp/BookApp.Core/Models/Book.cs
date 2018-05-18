using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookApp.Core.Models
{
    
    public class ReadingModes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    public class ImageLinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    public class PanelizationSummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class VolumeInfo
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public List<string> authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public string description { get; set; }
        public ReadingModes readingModes { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public ImageLinks imageLinks { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
        public PanelizationSummary panelizationSummary { get; set; }
    }

    public class ListPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class RetailPrice
    {
        public double amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class ListPrice2
    {
        public string amountInMicros { get; set; }
        public string currencyCode { get; set; }
    }

    public class RetailPrice2
    {
        public string amountInMicros { get; set; }
        public string currencyCode { get; set; }
    }

    public class Offer
    {
        public int finskyOfferType { get; set; }
        public ListPrice2 listPrice { get; set; }
        public RetailPrice2 retailPrice { get; set; }
    }

    public class SaleInfo
    {
        public string country { get; set; }
        public ListPrice listPrice { get; set; }
        public RetailPrice retailPrice { get; set; }
        public string buyLink { get; set; }
        public List<Offer> offers { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class AccessInfo
    {
        public string country { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string accessViewStatus { get; set; }
    }

    public class SearchInfo
    {
        public string textSnippet { get; set; }
    }

    public class Book
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public VolumeInfo volumeInfo { get; set; }
        public SaleInfo saleInfo { get; set; }
        public AccessInfo accessInfo { get; set; }
        public SearchInfo searchInfo { get; set; }
    }

    public class RootObjectBooks
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        [JsonProperty("items")]
        public List<Book> books { get; set; }
    }
}
