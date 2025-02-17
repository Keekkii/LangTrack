using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WpfApp2.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=LAPTOP-FASTSSH6\\SQLEXPRESS; Database=MVVMLoginDb; Integrated Security = true; TrustServerCertificate=True";
        }

        protected SqlConnection GetConnection() {
            return new SqlConnection(_connectionString);
        }
    }
}
