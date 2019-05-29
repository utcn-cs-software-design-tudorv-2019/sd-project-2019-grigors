using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TellYourFriends.Controllers;
using WorkshopDotNet.DataAcces;
using WorkshopDotNet.Models;
using WorkshopDotNet.Repository.Interfaces;
namespace TellYourFriendsTest
{
    [TestClass]
    public class TestUserController
    {
        private IQueryable<User> _listOfEmployees;
        private Mock<IUserRepository> _mockRepository;
        private UserController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            _listOfEmployees = GetMockedEmployees();
            _mockRepository = new Mock<IEmployeesRepo>();
            _controller = new EmployeesController(_mockRepository.Object);
        }

        [TestMethod]
        public void GetAllEmployees_ReturnsAListWithTheEmployees()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetEmployees()).Returns(_listOfEmployees);

            // Act
            var actionResult = _controller.GetEmployees();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Employee>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content.Count() > 1);
        }

        [TestMethod]
        public void AddEmployee_ValidInput_ReturnsTheValidEmployee()
        {
            // Arrange
            var employee = new Employee { Email = "works", Password = "test", Name = "Test" };

            _mockRepository.Setup(x => x.AddEmployee(employee)).Returns(employee);

            // Act
            var actionResult = _controller.InsertEmployee(employee);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void Update_ValidEmployee_ReturnsTheValidEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 14, Email = "works", Password = "test", Name = "Test" };

            _mockRepository.Setup(x => x.UpdateEmployee(employee)).Returns(employee);

            // Act
            var actionResult = _controller.PutEmployee(employee);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockRepository = null;
            _listOfEmployees = null;
            _controller.Dispose();
        }

        private static IQueryable<Employee> GetMockedEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 8, Email = "test@tss-yonder.com", Password = "test", Name = "Test" },
                new Employee { Id = 13, Email = "ttt@tss-yonder.com", Password = "ttt", Name = "ttt" },
                new Employee { Id = 14, Email = "echipa@tss-yonder.com", Password = "echipa", Name = "TEOO" },
            };

            return employees.AsQueryable();
        }
    }
}
