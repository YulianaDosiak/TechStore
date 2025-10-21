using System.Collections.Generic;
using System.Data.SqlClient;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class ProductDAL : IProductDAL
    {
        private readonly TechStoreDbContext _context;

        public ProductDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            var list = new List<Product>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Products", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = reader["ProductName"].ToString(),
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"],
                            CategoryID = (int)reader["CategoryID"]
                        });
                    }
                }
            }
            return list;
        }

        public Product GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = reader["ProductName"].ToString(),
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"],
                            CategoryID = (int)reader["CategoryID"]
                        };
                    }
                }
            }
            return null;
        }

        public void Insert(Product p)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Products (ProductName, Price, Quantity, CategoryID) VALUES (@n, @p, @q, @c)", conn);
                cmd.Parameters.AddWithValue("@n", p.ProductName);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@q", p.Quantity);
                cmd.Parameters.AddWithValue("@c", p.CategoryID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product p)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Products SET ProductName=@n, Price=@p, Quantity=@q, CategoryID=@c WHERE ProductID=@id", conn);
                cmd.Parameters.AddWithValue("@n", p.ProductName);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@q", p.Quantity);
                cmd.Parameters.AddWithValue("@c", p.CategoryID);
                cmd.Parameters.AddWithValue("@id", p.ProductID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
