using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.main
{
    public class PropertyUtil
    {
        public static string GetPropertyString()
        {
            return ConfigurationManager.AppSettings["DefaultConnection"];
        }
    }
}
