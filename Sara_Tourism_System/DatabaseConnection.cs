using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Sara_Tourism_System
{
    internal class DatabaseConnection
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["sara_tourism"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

   
}
