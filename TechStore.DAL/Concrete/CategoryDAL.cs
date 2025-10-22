using System.Collections.Generic;
using System.Data.SqlClient;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly TechStoreDbContext _context;

        public CategoryDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            var list = new List<Category>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Categories", conn)) // ВИПРАВЛЕННЯ
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Category
                            {
                                CategoryID = (int)reader["CategoryID"],
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public Category GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryID=@id", conn)) // ВИПРАВЛЕННЯ
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Category
                            {
                                CategoryID = (int)reader["CategoryID"],
                                CategoryName = reader["CategoryName"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void Insert(Category c)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand( // ВИПРАВЛЕННЯ
                    "INSERT INTO Categories (CategoryName) VALUES (@n)", conn))
                {
                    cmd.Parameters.AddWithValue("@n", c.CategoryName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Category c)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand( // ВИПРАВЛЕННЯ
                    "UPDATE Categories SET CategoryName=@n WHERE CategoryID=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@n", c.CategoryName);
                    cmd.Parameters.AddWithValue("@id", c.CategoryID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID=@id", conn)) // ВИПРАВЛЕННЯ
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}