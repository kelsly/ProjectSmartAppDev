using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookApp.Core.Models;
using BookApp.Core.Services;
using BookApp.Core.ViewModels;
using BookApp.Tests.Plumbing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Test.Core;
using Newtonsoft.Json;

namespace BookApp.Tests
{
    [TestClass]
    public class BestsellersTableViewModelTests : MvxIoCSupportingTest
    {
        protected MockDispatcher MockDispatcher;

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
        }

        [TestMethod]
        public void Bestsellers_Property_Return_All_Bestsellers()
        {
            //Arrange
            List<Book> books = new List<Book>();
            books.Add(new Book() { kind = "books#volume", id = "Zr-QswEACAAJ", etag = "Ac1dU0yihV8", selfLink = "https://www.googleapis.com/books/v1/volumes/Zr-QswEACAAJ", volumeInfo = new VolumeInfo() { title = "The 17th Suspect", authors = new List<string>() { "James Patterson", "Maxine Paetro" } }});
            books.Add(new Book() { kind = "books#volume", id = "S0g4tAEACAAJ", etag = "Ac1dU0yihV8", selfLink = "https://www.googleapis.com/books/v1/volumes/S0g4tAEACAAJ", volumeInfo = new VolumeInfo() { title = "The Fallen", authors = new List<string>() { "David Baldacci" } } });

            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(bs => bs.GetBestsellers()).Returns(Task.FromResult(books));

            var vm = new BestsellersTableViewModel(mockBookService.Object);

            //Act
            var allBestsellers = vm.BestsellersList;

            //Assert
            Assert.IsNotNull(allBestsellers);
            Assert.IsTrue(allBestsellers.Count == 2);
        }
    }
}
