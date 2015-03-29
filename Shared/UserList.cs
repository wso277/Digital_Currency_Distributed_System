using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    class UserList
    {
        static UserList sessions = null;
        ConcurrentDictionary<string, User> userList;

        public static UserList getInstance() {
            if (sessions == null) {
                sessions = new UserList();
            }
            return sessions;
        }

        public UserList()
        {
            userList = new ConcurrentDictionary<string, User>();
        }

        public bool addUser(string username, string name, int password)
        {
            if (userList.ContainsKey(username))
            {
                Console.WriteLine("User already exists...");
                return false;
            }

            userList.GetOrAdd(username, new User(username, name, password));

            return true;
        }

        public bool checkLogin(string username, int password)
        {
            User testUser;
            userList.TryGetValue(username, out testUser);

            if (password == testUser.Pass)
            {
                return true;
            }

            return false;
        }

        public bool changePassword(string username, int oldPassword, int newPassword)
        {
            User testUser;
            if (userList.TryGetValue(username, out testUser))
            {
                if (testUser.Pass == oldPassword)
                {
                    testUser.Pass = newPassword;
                    return true;
                }
            }

            return false;
        }
    }
}
