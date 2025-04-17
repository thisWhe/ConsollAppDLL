using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class PluginClass
    {

        public void Run(SqlConnection cnn, string cmd)
        {

            var parts = cmd.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                Console.WriteLine("Komut bos.");
                return;
            }



            switch (parts[0].ToLower())
            {
                case "insert":
                    if (parts.Length < 4)
                    {
                        Console.WriteLine("Kullanım: insert [Ad] [Soyad] [Yaş]");
                        return;
                    }
                    var insertSql = "INSERT INTO users (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)";
                    cnn.Execute(insertSql, new { FirstName = parts[1], LastName = parts[2], Age = int.Parse(parts[3]) });
                    Console.WriteLine("Kullanıcı eklendi.");
                    break;

                case "delete":
                    if (parts.Length < 2)
                    {
                        Console.WriteLine("Kullanım: delete [Id]");
                        
                    }
                    var deleteSql = "DELETE FROM users WHERE Id = @Id";
                    cnn.Execute(deleteSql, new { Id = int.Parse(parts[1]) });
                    
                    Console.WriteLine("Kullanıcı silindi.");
                    break;

                case "list":
                    var users = cnn.Query("SELECT * FROM users");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName}, {user.Age} yaşında");
                    }
                    break;

                default:
                    Console.WriteLine("Bilinmeyen komut");
                    break;
            }

            
        }

    }
}
