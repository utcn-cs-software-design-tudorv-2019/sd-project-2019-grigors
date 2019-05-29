using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using TellYourFriends.Controllers;
using TellYourFriends.Models;
using TellYourFriends.Models.Business_Logic.Interfaces;

namespace TellYourFriends.Test
{
    [TestClass]
    class TestMovieController
    {
        private IQueryable<Movie> _listOfMovies;
        private Mock<IMovieService> _mockRepository;
        private MoviesController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            _listOfMovies = GetMockedMovies();
            _mockRepository = new Mock<IMovieService>();
            _controller = new MoviesController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetAllMovies_ReturnsAListWithTheMovies()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAllMovies()).Returns(_listOfMovies);

            // Act
            var actionResult = _controller.GetMovies();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Movie>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Count() > 1);
        }

        [TestMethod]
        public void AddMovie_ValidInput_ReturnsTheValidMovie()
        {
            // Arrange
            var movie = new Movie { Id = 14, Author = "Abraham", Rating = 1, Name = "Test2", Description = "some desc", Edition = "5" };

            _mockRepository.Setup(x => x.AddMovie(movie)).Returns(movie);

            // Act
            var actionResult = _controller.CreateMovie(movie);
            var contentResult = actionResult as OkNegotiatedContentResult<Movie>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Update_ValidMovie_ReturnsTheValidMovie()
        {
            // Arrange
            var movie = new Movie { Id = 14, Author = "Lincoln", Rating = 1, Name = "Test2", Description = "some desc", Edition = "5" };

            _mockRepository.Setup(x => x.EditMovie(movie)).Returns(movie);

            // Act
            var actionResult = _controller.EditMovie(movie);
            var contentResult = actionResult as OkNegotiatedContentResult<Movie>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockRepository = null;
            _listOfMovies = null;
            _controller.Dispose();
        }

        private static IQueryable<Movie> GetMockedMovies()
        {
            var movies = new List<Movie>
            {
               new Movie {Id = 19, Author = "Author1", Rating = 1, Name = "Test3", Description="some desc", Edition="5"},
               new Movie { Id = 4, Author = "Author2", Rating = 1, Name = "Test4", Description="some desc", Edition="5"},
               new Movie {Id = 74, Author = "Author3", Rating = 1, Name = "Test5", Description="some desc", Edition="5" },
        };

            return movies.AsQueryable();
        }
    }
}
