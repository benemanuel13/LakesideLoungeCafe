using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeAdmin.Infrastructure;

namespace LakesideLoungeAdmin.Application
{
    public class MainWindowService
    {
        public void CreateUpdatesTransactionFile()
        {
            Log.CreateTransactionFile();
        }

        public void BackupDatabase(string fileName)
        {
            Database.BackupDatabase(fileName);
        }

        public bool RestoreDatabase(string fileName)
        {
            return Database.RestoreDatabase(fileName);
        }
    }
}
