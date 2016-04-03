using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Data;

namespace AddAdmins
{
    class ConsoleToAddAdmin
    {
        static void Main(string[] args)
        {
            AdminsManager mgr = new AdminsManager(Properties.Settings.Default.EcommerceDbConnectionString);
            mgr.AddAdmin("ygoldgrab@gmail.com", "litrules");
            Console.WriteLine("Enter a username");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password");
            string password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                password += key.KeyChar;
                Console.Write("*");
            }

            Console.WriteLine();
            //addadmin
            Admin admin = mgr.GetAdmin(username, password);
            if (admin != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error!");
            }

            Console.ReadKey(true);
        }
    }
}
