using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    class DBProperties
    {
        private static string USER = "sa";
        private static string PASSWORD = "123";
        private static string SERVER = "MSI\\SQLHAK";
        private static string DB = "InventorySU10";
        public string DB_Address = $"SERVER={SERVER};" +
                                   $"Database={DB};" +
                                   $"user ID={USER};" +
                                   $"Password={PASSWORD}";
    }
}
    