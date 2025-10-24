using NUnit.Framework;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using TechStore.DAL;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

[TestFixture]
public class UserDALTests
{
    private const string TestConnectionString = "Data Source=localhost;Initial Catalog=TechStoreDB;Integrated Security=True;TrustServerCertificate=True";

    private SqlConnection _connection;
    private SqlTransaction _transaction;
    private TechStoreDbContext _context;
    private IUserDAL _userDal;

    [SetUp]
    public void Setup()
    {
        _connection = new SqlConnection(TestConnectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
        _context = new TechStoreDbContext(TestConnectionString);
        _userDal = new UserDAL(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
        _context?.Dispose();
    }

    [Test]
    public void Insert_And_GetById_User_ShouldBeSuccessful()
    {
        var newUser = new User
        {
            Username = "TestUser_Insert",
            Email = "test.insert@example.com",
            Password = "pass"
        };

        _userDal.Insert(newUser);

        var retrievedUser = _userDal.GetAll().FirstOrDefault(u => u.Email == "test.insert@example.com");

        Assert.IsNotNull(retrievedUser, "User should be found after insertion.");
        Assert.AreEqual(newUser.Username, retrievedUser.Username);
    }

    [Test]
    public void Update_ExistingUser_ShouldChangeUsername()
    {
        var userToInsert = new User
        {
            Username = "OldName",
            Email = "test.update@example.com",
            Password = "pass"
        };

        _userDal.Insert(userToInsert);
        var userToUpdate = _userDal.GetAll().FirstOrDefault(u => u.Email == userToInsert.Email);
        Assert.IsNotNull(userToUpdate, "User to update not found.");

        string newName = "NewUpdatedName";
        userToUpdate.Username = newName;
        _userDal.Update(userToUpdate);

        var updatedUser = _userDal.GetById(userToUpdate.UserID);
        Assert.IsNotNull(updatedUser);
        Assert.AreEqual(newName, updatedUser.Username, "Username should be updated.");
    }
}
