using System;
using System.Data.SqlClient;

namespace Data
{
    public class DBConnection
    {
        internal SqlConnection SqlConnection { get; }
        private string _connectionString;

        public DBConnection()
        {
            _connectionString = "Server = mssql.fhict.local; Database = dbi413117; User Id = dbi413117; Password = Test321!;MultipleActiveResultSets=true;";
            SqlConnection = new SqlConnection(_connectionString);
        }

        public SqlConnection GetConnString()
        {
            SqlConnection connString = new SqlConnection(_connectionString);
            return connString;
        }
    }
}