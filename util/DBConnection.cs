using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.main
{
    public class DBConnection
    {
        public static SqlConnection connection;
        public static SqlConnection GetConnection()
        {
            connection = new SqlConnection(PropertyUtil.GetPropertyString());
            connection.Open();
            return connection;
        }
    }
}
