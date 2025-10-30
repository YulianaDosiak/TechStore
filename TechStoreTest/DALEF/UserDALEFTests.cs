using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System.Linq;
using TechStore.DALEF.AutoMapper;
using TechStore.DALEF.Concrete;
using TechStore.DTO;

namespace TechStore.Test.DALEF
{
    [TestFixture]
    public class UserDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private UserDALEF _dal;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<UserMap>();
            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            _mapper = mapperConfig.CreateMapper();

            _dal = new UserDALEF(_testConnectionString, _mapper);
        }

        [Test]
        public void GetAllUsers_ReturnsUsers()
        {
            var list = _dal.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertUser_WorksCorrectly()
        {
            var user = new User
            {
                Username = "TestInsertLogin",
                Password = "123",
                Email = "test@insert.com"
            };

            var created = _dal.Create(user);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.Username, Is.EqualTo("TestInsertLogin"));
            _dal.Delete(created.UserID);
        }

        [Test]
        public void UpdateUser_WorksCorrectly()
        {
            var user = new User
            {
                Username = "TestUpdateLogin",
                Password = "123",
                Email = "test@update.com"
            };

            var created = _dal.Create(user);
            Assert.That(created, Is.Not.Null);

            created.Username = "UpdatedName";
            var updated = _dal.Update(created);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Username, Is.EqualTo("UpdatedName"));
            _dal.Delete(updated.UserID);
        }

        [Test]
        public void DeleteUser_WorksCorrectly()
        {
            var user = new User
            {
                Username = "TestDeleteLogin",
                Password = "123",
                Email = "test@delete.com"
            };

            var created = _dal.Create(user);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.UserID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.UserID);
            Assert.That(fromDb, Is.Null);
        }
    }
}