using Nedeljni_III_Milos_Peric.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_III_Milos_Peric
{
    class DatabaseService
    {
        internal tblUser AddUser(tblUser user)
        {
            try
            {
                using (RecipeDatabaseEntities context = new RecipeDatabaseEntities())
                {
                    tblUser newUser = new tblUser
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Password = user.Password
                    };
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    user.UserID = newUser.UserID;
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal bool UserExists(string userName)
        {
            try
            {
                using (RecipeDatabaseEntities context = new RecipeDatabaseEntities())
                {
                    tblUser userToFind = (from u in context.tblUsers where u.UserName == userName select u).First();
                    if (userToFind != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("User not found. " + ex.Message.ToString());
                return false;
            }
        }

        internal tblUser FindUserCredentials(string userName, string password)
        {
            try
            {
                using (RecipeDatabaseEntities context = new RecipeDatabaseEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    tblUser userToFind = (from u in context.tblUsers where u.UserName == userName && u.Password == encryptedPassword select u).First();
                    return userToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("User not found. " + ex.Message.ToString());
                return null;
            }
        }

    }
}
