using System.Collections.Generic;
using System.Data.SqlClient;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class UserDAL : IUserDAL
    {
        private readonly TechStoreDbContext _context;

        public UserDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                    }
                }
            }
            return users;
        }

        public User GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Users WHERE UserID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void Insert(User user)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Users (Username, Email, Password) VALUES (@u, @e, @p)", conn);
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(User user)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Users SET Username=@u, Email=@e, Password=@p WHERE UserID=@id", conn);
                cmd.Parameters.AddWithValue("@u", user.Username);
                cmd.Parameters.AddWithValue("@e", user.Email);
                cmd.Parameters.AddWithValue("@p", user.Password);
                cmd.Parameters.AddWithValue("@id", user.UserID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Users WHERE UserID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
