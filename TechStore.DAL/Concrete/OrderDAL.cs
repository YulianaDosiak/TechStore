using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechStore.DTO;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL.Concrete
{
    public class OrderDAL : IOrderDAL
    {
        private readonly string _connectionString;

        public OrderDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> GetAll()
        {
            var list = new List<Order>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Orders", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            UserID = (int)reader["UserID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"]
                        });
                    }
                }
            }
            return list;
        }

        public Order GetById(int id)
        {
            Order order = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            UserID = (int)reader["UserID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"]
                        };
                    }
                }
            }
            return order;
        }

        public void Insert(Order o)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Orders (UserID, OrderDate, TotalAmount) VALUES (@u, @d, @t)", conn);
                cmd.Parameters.AddWithValue("@u", o.UserID);
                cmd.Parameters.AddWithValue("@d", o.OrderDate);
                cmd.Parameters.AddWithValue("@t", o.TotalAmount);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Order o)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Orders SET UserID=@u, OrderDate=@d, TotalAmount=@t WHERE OrderID=@id", conn);
                cmd.Parameters.AddWithValue("@u", o.UserID);
                cmd.Parameters.AddWithValue("@d", o.OrderDate);
                cmd.Parameters.AddWithValue("@t", o.TotalAmount);
                cmd.Parameters.AddWithValue("@id", o.OrderID);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Orders WHERE OrderID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
