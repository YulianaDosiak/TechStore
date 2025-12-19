using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;
using TechStore.DALEF.AutoMapper;
using TechStore.DALEF.Concrete;
using TechStore.DTO;

namespace TechStore.Test.DALEF
{
    [TestFixture]
    public class ProductDALEFTests
    {
        private string _testConnectionString;
        private IMapper _mapper;
        private ProductDALEF _dal;
        private CategoryDALEF _categoryDal;
        private int _testCategoryId;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _testConnectionString = config.GetConnectionString("TestConnection");

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile<ProductMap>();
            configExpression.AddProfile<CategoryMap>();
            var mapperConfig = new MapperConfiguration(configExpression);

            _mapper = mapperConfig.CreateMapper();
            _mapper = mapperConfig.CreateMapper();

            _dal = new ProductDALEF(_testConnectionString, _mapper);
            _categoryDal = new CategoryDALEF(_testConnectionString, _mapper);

            var cat = _categoryDal.Create(new Category { CategoryName = "TestCatForProducts" });
            _testCategoryId = cat.CategoryID;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_testCategoryId > 0)
            {
                _categoryDal.Delete(_testCategoryId);
            }
        }

        [Test]
        public void GetAllProducts_ReturnsProducts()
        {
            var list = _dal.GetAll();
            Assert.That(list, Is.Not.Null);
            Assert.That(list.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void InsertProduct_WorksCorrectly()
        {
            var prod = new Product
            {
                Productname = "TestProduct_Insert",
                CategoryID = _testCategoryId,
                Price = 100,
                Quantity = 10
            };

            var created = _dal.Create(prod);
            Assert.That(created, Is.Not.Null);
            Assert.That(created.Productname, Is.EqualTo("TestProduct_Insert"));
            _dal.Delete(created.ProductID);
        }

        [Test]
        public void UpdateProduct_WorksCorrectly()
        {
            var prod = new Product
            {
                Productname = "TestProduct_Update",
                CategoryID = _testCategoryId,
                Price = 150,
                Quantity = 5
            };

            var created = _dal.Create(prod);
            Assert.That(created, Is.Not.Null);

            created.Price = 250;
            var updated = _dal.Update(created);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Price, Is.EqualTo(250));
            _dal.Delete(updated.ProductID);
        }

        [Test]
        public void DeleteProduct_WorksCorrectly()
        {
            var prod = new Product
            {
                Productname = "TestProduct_Delete",
                CategoryID = _testCategoryId,
                Price = 200,
                Quantity = 8
            };

            var created = _dal.Create(prod);
            Assert.That(created, Is.Not.Null);

            var deleted = _dal.Delete(created.ProductID);
            Assert.That(deleted, Is.True);

            var fromDb = _dal.GetById(created.ProductID);
            Assert.That(fromDb, Is.Null);
        }
    }
}