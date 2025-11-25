using NUnit.Framework;
using Moq;
using TechStore.BLL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Collections.Generic;

namespace TechStore.BLL.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IUserDAL> _mockUserDal;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _mockUserDal = new Mock<IUserDAL>();
            _authService = new AuthService(_mockUserDal.Object);
        }

        [Test]
        public void Login_CorrectCredentials_ReturnsUser()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserID = 1, Username = "testuser", Password = "password123", Email = "test@mail.com" }
            };
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(users);

            // Act
            var result = _authService.Login("testuser", "password123");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserID, Is.EqualTo(1));
        }

        [Test]
        public void Login_WrongPassword_ReturnsNull()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserID = 1, Username = "testuser", Password = "password123" }
            };
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(users);

            // Act
            var result = _authService.Login("testuser", "wrongpassword");

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Register_NewUser_ReturnsTrue_AndCallsCreate()
        {
            var users = new List<User>();
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(users);
            var newUser = new User { Username = "newuser", Password = "123" };

            var result = _authService.Register(newUser);

            Assert.That(result, Is.True);
            _mockUserDal.Verify(dal => dal.Create(newUser), Times.Once);
        }

        [Test]
        public void Register_ExistingUser_ReturnsFalse_AndDoesNotCallCreate()
        {
            var users = new List<User>
            {
                new User { Username = "existingUser", Password = "123" }
            };
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(users);
            var newUser = new User { Username = "existingUser", Password = "456" };


            var result = _authService.Register(newUser);


            Assert.That(result, Is.False);
            _mockUserDal.Verify(dal => dal.Create(It.IsAny<User>()), Times.Never);
        }
    }
}