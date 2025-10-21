using System.Data;
using System.Data.SqlClient;

namespace TechStore.DAL
{
    public class TechStoreDbContext
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
    }
}
