using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TellYourFriends.Controllers;
using TellYourFriends.Models.Business_Logic.Interfaces;
using TellYourFriends.Models.Entity;

namespace TellYourFriends.Test
{
    [TestClass]
    class TestBookController
    {
        private IQueryable<Book> _listOfBooks;
        private Mock<IBookService> _mockRepository;
        private BooksController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            _listOfBooks = GetMockedBooks();
            _mockRepository = new Mock<IBookService>();
            _controller = new BooksController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetAllBooks_ReturnsAListWithTheBooks()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAllBooks()).Returns(_listOfBooks);

            // Act
            var actionResult = _controller.GetBooks();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Book>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Count() > 1);
        }

        [TestMethod]
        public void AddBook_ValidInput_ReturnsTheValidBook()
        {
            // Arrange
            var book = new Book { Id = 14, Author = "Abraham", Rating = 1, Name = "Test2", Description = "some desc", Edition = "5" };

            _mockRepository.Setup(x => x.AddBook(book)).Returns(book);

            // Act
            var actionResult = _controller.CreateBook(book);
            var contentResult = actionResult as OkNegotiatedContentResult<Book>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Update_ValidBook_ReturnsTheValidBook()
        {
            // Arrange
            var book = new Book { Id = 14, Author = "Lincoln", Rating = 1, Name = "Test2", Description = "some desc", Edition = "5" };

            _mockRepository.Setup(x => x.EditBook(book)).Returns(book);

            // Act
            var actionResult = _controller.EditBook(book);
            var contentResult = actionResult as OkNegotiatedContentResult<Book>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockRepository = null;
            _listOfBooks = null;
            _controller.Dispose();
        }

        private static IQueryable<Book> GetMockedBooks()
        {
            var books = new List<Book>
            {
               new Book {Id = 19, Author = "Author1", Rating = 1, Name = "Test3", Description="some desc", Edition="5"},
               new Book { Id = 4, Author = "Author2", Rating = 1, Name = "Test4", Description="some desc", Edition="5"},
               new Book {Id = 74, Author = "Author3", Rating = 1, Name = "Test5", Description="some desc", Edition="5" },
        };

            return books.AsQueryable();
        }
    }
}
