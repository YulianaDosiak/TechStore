using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserDAL> _mockUserDal;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockUserDal = new Mock<IUserDAL>();
            _userService = new UserService(_mockUserDal.Object);
        }

        [Test]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User> { new User { UserID = 1, Username = "TestUser" } };
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(users);

            // Act
            var result = _userService.GetAllUsers();

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Username, Is.EqualTo("TestUser"));
        }

        [Test]
        public void UpdateUser_CallsUpdateInDal()
        {
            // Arrange
            var user = new User { UserID = 1, Username = "UpdatedName" };

            // Act
            _userService.UpdateUser(user);

            // Assert
            _mockUserDal.Verify(dal => dal.Update(user), Times.Once);
        }

        [Test]
        public void DeleteUser_CallsDeleteInDal()
        {
            // Act
            _userService.DeleteUser(10);

            // Assert
            _mockUserDal.Verify(dal => dal.Delete(10), Times.Once);
        }
    }
}