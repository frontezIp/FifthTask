using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifth_task
{
    public class DataBaseLauncher
    {
        private static string _stringConnection;

        public DataBaseLauncher(string connectionString)
        {
            StringConnection = connectionString;
        }

        public static string StringConnection { get => _stringConnection; set => _stringConnection = value; }
    }
}
