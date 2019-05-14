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
            //_connectionString = "Data Source=mssql.fhict.local;User Id=dbi413117;Password=Test321!;Database=dbi413117;";
            _connectionString = "Server = mssql.fhict.local; Database = dbi413117; User Id = dbi413117; Password = Test321!;MultipleActiveResultSets=true;";
            //_connectionString = "asdasdasd";

            SqlConnection = new SqlConnection(_connectionString);
        }

        public SqlConnection GetConnString()
        {
            return SqlConnection;
        }
    }
}
