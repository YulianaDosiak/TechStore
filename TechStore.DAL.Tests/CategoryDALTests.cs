using NUnit.Framework;
using System.Data.SqlClient;
using System.Data;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;
using System.Linq;

[TestFixture] // Атрибут NUnit, що позначає клас як тестовий
public class CategoryDALTests
{
    // Рядок підключення для тестування
    private const string TestConnectionString = "Data Source=localhost;Initial Catalog=TechStore;Integrated Security=True;TrustServerCertificate=True";

    // Об'єкти для керування транзакцією та підключенням
    private SqlConnection _connection;
    private SqlTransaction _transaction;

    // Об'єкти DAL
    private TechStoreDbContext _context;
    private ICategoryDAL _categoryDal;

    [SetUp] // Виконується перед КОЖНИМ тестовим методом (аналог [TestInitialize] в MSTest)
    public void Setup()
    {
        // 1. Створюємо та відкриваємо з'єднання
        _connection = new SqlConnection(TestConnectionString);
        _connection.Open();

        // 2. ПОЧИНАЄМО ТРАНЗАКЦІЮ (КЛЮЧОВИЙ МОМЕНТ ДЛЯ ІЗОЛЯЦІЇ ТЕСТІВ)
        _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);

        // 3. Створюємо контекст та DAL-клас, який тестуємо
        _context = new TechStoreDbContext(TestConnectionString);
        _categoryDal = new CategoryDAL(_context);
    }

    [TearDown] // Виконується після КОЖНОГО тестового методу (аналог [TestCleanup] в MSTest)
    public void TearDown()
    {
        // 4. ВІДКАТ ТРАНЗАКЦІЇ (ROLLBACK) - Скасовує всі зміни, внесені у БД під час тесту
        _transaction?.Rollback();

        // 5. Звільнення ресурсів
        _transaction?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
        _context?.Dispose();
    }

    [Test] // Атрибут NUnit, що позначає метод як тест
    public void Insert_NewCategory_ShouldBeFoundInDatabase()
    {
        // Arrange
        var newCategory = new Category { CategoryName = "Test_Insert_Category" };

        // Act
        _categoryDal.Insert(newCategory);
        var retrievedCategory = _categoryDal.GetAll().FirstOrDefault(c => c.CategoryName == "Test_Insert_Category");

        // Assert
        Assert.IsNotNull(retrievedCategory, "Категорія повинна бути знайдена після вставки.");
        Assert.AreEqual(newCategory.CategoryName, retrievedCategory.CategoryName);
    }

    [Test]
    public void Delete_ExistingCategory_ShouldBeNullAfterDeletion()
    {
        // Arrange
        var newCategory = new Category { CategoryName = "Test_Delete_Category" };
        _categoryDal.Insert(newCategory);
        var categoryToDelete = _categoryDal.GetAll().FirstOrDefault(c => c.CategoryName == "Test_Delete_Category");

        Assert.IsNotNull(categoryToDelete, "Категорія для видалення не знайдена.");

        // Act
        _categoryDal.Delete(categoryToDelete.CategoryID);

        // Assert
        var deletedCategory = _categoryDal.GetById(categoryToDelete.CategoryID);
        Assert.IsNull(deletedCategory, "Категорія повинна бути відсутня після видалення.");
    }
}