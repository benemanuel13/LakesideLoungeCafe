using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LakesideLoungeDB.Infrastructure;

namespace LakesideLoungeDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Database...");
            Database.CreateDatabase();

            Console.WriteLine("Creating Tables...");
            Database.CreateTables();

            Console.WriteLine("Creating Stored Procedures...");
            Database.CreateStoredProcedures();

            Console.WriteLine("Populating Database...");
            Database.PopulateDatabase();

            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}
