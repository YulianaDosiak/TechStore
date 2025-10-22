using System;
using System.Data;
using System.Data.SqlClient;

namespace TechStore.DAL
{
    // Реалізує IDisposable для керування ресурсами
    public class TechStoreDbContext : IDisposable
    {
        private readonly string _connectionString;

        public TechStoreDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void Dispose()
        {
            // У цьому простому прикладі Dispose залишається порожнім, 
            // оскільки SqlConnection звільняється у блоках using в DAL класах.
        }
    }
}