using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Web.Http.Results;
using TellYourFriends.Controllers;
using TellYourFriends.Models;
using TellYourFriends.Models.Data_Access.Repository.Interfaces;
using TellYourFriends.Models.Business_Logic.Interfaces;

namespace TellYourFriends.Test
{
    [TestClass]
    class TestUserController
    {
        private IQueryable<User> _listOfUsers;
        private Mock<IUserService> _mockRepository;
        private UsersController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            _listOfUsers = GetMockedUsers();
            _mockRepository = new Mock<IUserService>();
            _controller = new UsersController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsAListWithTheUsers()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetUsers()).Returns(_listOfUsers);

            // Act
            var actionResult = _controller.GetUsers();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<User>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Count() > 1);
        }

        [TestMethod]
        public void AddUser_ValidInput_ReturnsTheValidUser()
        {
            // Arrange
            var user = new User { Email = "works", Password = "test", Name = "Test" };

            _mockRepository.Setup(x => x.AddUser(user)).Returns(user);

            // Act
            var actionResult = _controller.CreateUser(user);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Update_ValidUser_ReturnsTheValidUser()
        {
            // Arrange
            var user = new User { Id = 14, Email = "works", Password = "test", Name = "Test" };

            _mockRepository.Setup(x => x.UpdateUser(user)).Returns(user);

            // Act
            var actionResult = _controller.EditUser(user);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockRepository = null;
            _listOfUsers = null;
            _controller.Dispose();
        }

        private static IQueryable<User> GetMockedUsers()
        {
            var users = new List<User>
            {
                new User { Id = 8, Email = "test@gmiil.com", Password = "test", Name = "Test" },
                new User { Id = 13, Email = "ttt@gmail.com", Password = "ttt", Name = "ttt" },
                new User { Id = 14, Email = "echipa@hotmail.com", Password = "echipa", Name = "TEOO" },
            };

            return users.AsQueryable();
        }
    }
}
