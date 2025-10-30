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
    public class CategoryDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private CategoryDALEF _dal;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<CategoryMap>();
            var mapperConfig = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
            _mapper = mapperConfig.CreateMapper();

            _dal = new CategoryDALEF(_testConnectionString, _mapper);
        }

        [Test]
        public void GetAllCategories_ReturnsCategories()
        {
            var categories = _dal.GetAll();
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertCategory_WorksCorrectly()
        {
            var category = new Category { CategoryName = "TestCategory_Insert" };
            var created = _dal.Create(category);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.CategoryName, Is.EqualTo("TestCategory_Insert"));
            _dal.Delete(created.CategoryID);
        }

        [Test]
        public void UpdateCategory_WorksCorrectly()
        {
            var category = new Category { CategoryName = "TestCategory_ForUpdate" };
            var created = _dal.Create(category);
            Assert.That(created, Is.Not.Null);

            created.CategoryName = "TestCategory_Updated";
            var updated = _dal.Update(created);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.CategoryName, Is.EqualTo("TestCategory_Updated"));
            _dal.Delete(updated.CategoryID);
        }

        [Test]
        public void DeleteCategory_WorksCorrectly()
        {
            var category = new Category { CategoryName = "TestCategory_ForDelete" };
            var created = _dal.Create(category);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.CategoryID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.CategoryID);
            Assert.That(fromDb, Is.Null);
        }
    }
}