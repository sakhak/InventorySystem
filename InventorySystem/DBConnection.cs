using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    class DBConnection : DBProperties

    {
        private readonly DBProperties _dinaLove = new DBProperties();

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_dinaLove.DB_Address);
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            return connection;
        }
    }
}
