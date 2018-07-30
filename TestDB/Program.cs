using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.setConnectionSettings("den1.mssql6.gear.host", "lenanetkatestdb", "lenanetkatestdb", "Pe37B!Ual!NW");
            Database db = Database.getInstanse();
            //db.create_table("persons", "name varchar(255)");
            Console.WriteLine("Writing...");
            db.write("persons", "name", "Jon");
            Console.WriteLine("End writing");
            Console.WriteLine("Reading...");
            List<string> names = db.read("persons", "name");
            foreach(string name in names) Console.WriteLine(name);
            Console.WriteLine("End reading");
            Console.ReadKey();
            
        }
    }
}
