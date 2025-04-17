using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var dllPath = "C:\\Users\\Emir\\source\\repos\\ConsoleApp1\\ClassLibrary1\\bin\\Debug\\ClassLibrary1.dll";
            var connectionString = "Server=DESKTOP-RA4ABQ2\\SQLEXPRESS;Database=Test33;Trusted_Connection=True;";

             var connection = new SqlConnection(connectionString);
            connection.Open();

            
            InitializeDatabase(connection);

            Console.WriteLine("Komut gir (insert/delete/list). ): ");

            while (true)
            {
                Console.Write(" ");
                var input = Console.ReadLine();
                if (input == "exit") break;

                if (!File.Exists(dllPath))
                {
                    Console.WriteLine("DLL bulunamadı.");
                    continue;
                }

                try
                {
                    var a = Assembly.LoadFrom(dllPath);
                    var type = a.GetType("ClassLibrary1.PluginClass");
                    var instance = Activator.CreateInstance(type);
                    var method = type.GetMethod("Run");

                    method.Invoke(instance, new object[] { connection, input });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }
        private static void InitializeDatabase(SqlConnection connection)
        {
            
        }

    }
    
}
