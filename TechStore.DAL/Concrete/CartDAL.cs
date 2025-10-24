using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class CartDAL : ICartDAL
    {
        private readonly TechStoreDbContext _context;

        public CartDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cart> GetAll()
        {
            var list = new List<Cart>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Cart", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cart
                        {
                            CartID = (int)reader["CartID"],
                            UserID = (int)reader["UserID"] 
                        });
                    }
                }
            }
            return list;
        }

        public Cart GetById(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Cart WHERE CartID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cart
                        {
                            CartID = (int)reader["CartID"], 
                            UserID = (int)reader["UserID"] 
                        };
                    }
                }
            }
            return null;
        }

        public void Insert(Cart cart)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Cart (UserID) VALUES (@u)", conn);
                cmd.Parameters.AddWithValue("@u", cart.UserID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Cart cart)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("UPDATE Cart SET UserID=@u WHERE CartID=@id", conn);
                cmd.Parameters.AddWithValue("@u", cart.UserID);
                cmd.Parameters.AddWithValue("@id", cart.CartID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Cart WHERE CartID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
