using System;
using System.Diagnostics;

namespace Remote
{
    [Serializable()]
    public class User
    {
        string username;
        string nickname;
        long password;

        public long Pass
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public User(string user, string nick, int pass)
        {
            username = user;
            nickname = nick;
            password = pass;
        }

        public bool Login(string user, int pass)
        {
            bool flop = false;
            if (user != username)
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Wrong username!");
                flop = true;
            }

            if (pass != password)
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Wrong password!");
                return flop;
            }

            return flop;
        }

    }
}
