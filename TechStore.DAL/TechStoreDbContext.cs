using System;
using System.Data.SqlClient;

namespace TechStore.DAL
{
    public class TechStoreDbContext : IDisposable
    {
        private readonly string _connectionString;

        public TechStoreDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.");
            }
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void Dispose()
        {
            // Ресурси SqlConnection керуються блоками using в DAL
        }
    }
}