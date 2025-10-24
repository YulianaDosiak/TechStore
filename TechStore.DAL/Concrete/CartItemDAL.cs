using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class CartItemDAL : ICartItemDAL
    {
        private readonly TechStoreDbContext _context;

        public CartItemDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CartItem> GetAll()
        {
            var list = new List<CartItem>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM CartItems", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CartItem
                        {
                            CartItemID = (int)reader["CartItemID"],
                            CartID = (int)reader["CartID"], 
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"]
                        });
                    }
                }
            }
            return list;
        }

        public CartItem GetById(int id)
        {
            CartItem item = null;
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM CartItems WHERE CartItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new CartItem
                        {
                            CartItemID = (int)reader["CartItemID"], 
                            CartID = (int)reader["CartID"], 
                            ProductID = (int)reader["ProductID"], 
                            Quantity = (int)reader["Quantity"]
                        };
                    }
                }
            }
            return item;
        }

        public void Insert(CartItem item)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO CartItems (CartID, ProductID, Quantity) VALUES (@c, @p, @q)", conn);
                cmd.Parameters.AddWithValue("@c", item.CartID);
                cmd.Parameters.AddWithValue("@p", item.ProductID);
                cmd.Parameters.AddWithValue("@q", item.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(CartItem item)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE CartItems SET CartID=@c, ProductID=@p, Quantity=@q WHERE CartItemID=@id", conn);
                cmd.Parameters.AddWithValue("@c", item.CartID);
                cmd.Parameters.AddWithValue("@p", item.ProductID);
                cmd.Parameters.AddWithValue("@q", item.Quantity);
                cmd.Parameters.AddWithValue("@id", item.CartItemID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM CartItems WHERE CartItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
