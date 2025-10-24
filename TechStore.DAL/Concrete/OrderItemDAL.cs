using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TechStore.DTO;
using TechStore.DAL.Interfaces;
using System;

namespace TechStore.DAL.Concrete
{
    public class OrderItemDAL : IOrderItemDAL
    {
        private readonly TechStoreDbContext _context;

        public OrderItemDAL(TechStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            var list = new List<OrderItem>();
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM OrderItems", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderItem
                        {
                            OrderItemID = (int)reader["OrderItemID"],
                            OrderID = (int)reader["OrderID"],
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
            }
            return list;
        }

        public OrderItem GetById(int id)
        {
            OrderItem item = null;
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM OrderItems WHERE OrderItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new OrderItem
                        {
                            OrderItemID = (int)reader["OrderItemID"],
                            OrderID = (int)reader["OrderID"],
                            ProductID = (int)reader["ProductID"],
                            Quantity = (int)reader["Quantity"],
                            Price = (decimal)reader["Price"]
                        };
                    }
                }
            }
            return item;
        }

        public void Insert(OrderItem item)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price) VALUES (@o, @p, @q, @r)", conn);
                cmd.Parameters.AddWithValue("@o", item.OrderID);
                cmd.Parameters.AddWithValue("@p", item.ProductID);
                cmd.Parameters.AddWithValue("@q", item.Quantity);
                cmd.Parameters.AddWithValue("@r", item.Price);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(OrderItem item)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE OrderItems SET OrderID=@o, ProductID=@p, Quantity=@q, Price=@r WHERE OrderItemID=@id", conn);
                cmd.Parameters.AddWithValue("@o", item.OrderID);
                cmd.Parameters.AddWithValue("@p", item.ProductID);
                cmd.Parameters.AddWithValue("@q", item.Quantity);
                cmd.Parameters.AddWithValue("@r", item.Price);
                cmd.Parameters.AddWithValue("@id", item.OrderItemID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = _context.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM OrderItems WHERE OrderItemID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}