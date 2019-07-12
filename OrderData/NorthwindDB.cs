using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderData
{
    public class NorthwindDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\SAITSQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
