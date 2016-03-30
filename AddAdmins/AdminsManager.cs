using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAdmins
{
   public class AdminsManager
    {
       private readonly string _connectionString;
       public AdminsManager(string connection)
       {
           _connectionString = connection;
       }
       public void AddAdmin(string username, string password)
       {
           Admin admin = new Admin();
           string salt = PasswordHelper.GenerateRandomSalt();
           string passwordHash = PasswordHelper.HashPassword(password, salt);
           admin.PassswordHash = passwordHash;
           admin.Salt = salt;
           admin.UserName = username;
           using(var dc = new adminDataContext(_connectionString))
           {              
               dc.Admins.InsertOnSubmit(admin);
               dc.SubmitChanges();
           }
       }
       public Admin GetAdmin(string username, string password)
       {       
          
           using (var dc = new adminDataContext(_connectionString))
           {
               var admin = dc.Admins.FirstOrDefault(a => a.UserName == username);// && a.PassswordHash == )
               if(admin == null)
               {
                   return null;
               }
              bool success = PasswordHelper.PasswordMatch(password, admin.PassswordHash, admin.Salt);
              return success ? admin : null;
           }
       }
    }
}
