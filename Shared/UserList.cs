using System;
using System.IO;
using System.Collections.Concurrent;
using System.Diagnostics;
using Newtonsoft.Json;
using Market;

namespace Shared
{
    [Serializable()]
    public class UserList : MarshalByRefObject
    {
        static string pathToDB = "database.db";
        static ConcurrentDictionary<string, User> userList;


        public UserList()
        {
            loadUsers();
        }

        public bool addUser(string username, string name, int password)
        {
            if (userList.ContainsKey(username))
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "User already exists...");
                return false;
            }

            userList.GetOrAdd(username, new User(username, name, password));
            Controller.getInstance().addDigiNotes(username);

            saveUsers();
            return true;
        }

        public bool checkLogin(string username, int password)
        {
            User testUser;
            Console.WriteLine("Username: " + username + ", password: " + password);
            userList.TryGetValue(username, out testUser);

            Console.WriteLine(testUser.Pass);

            if (password == testUser.Pass)
            {
                return true;
            }

            Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Password Errada");
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

        public static void loadUsers()
        {
            if (File.Exists(pathToDB))
            {
                
                JsonSerializer js = new JsonSerializer();
                js.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sw = new StreamReader(pathToDB))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    userList = js.Deserialize<ConcurrentDictionary<string, User>>(reader);
                }
            }
            else
            {
                userList = new ConcurrentDictionary<string, User>();
            }
        }

        public static void saveUsers()
        {
            JsonSerializer js = new JsonSerializer();
            js.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(pathToDB))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                js.Serialize(writer, userList);
            }
        }

        public ConcurrentDictionary<string, User> getUserList()
        {
            return userList;
        }

    }
}
