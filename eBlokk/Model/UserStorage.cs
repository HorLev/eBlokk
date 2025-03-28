using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBlokk.Model
{
    public static class UserStorage
    {
        private static Dictionary<string, User> users = new Dictionary<string, User>();

        public static bool AddUser(User user)
        {
            if (users.ContainsKey(user.Username))
            {
                return false;
            }

            users[user.Username] = user;
            return true;
        }

        public static User? GetUser(string username)
        {
            users.TryGetValue(username, out var user);
            return user;
        }


        public static bool ValidateUser(string username, string password)
        {
            return users.TryGetValue(username, out var user) && user.Password == password;
        }
    }
}
