using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class User
    {
        string username;
        string nickname;
        int password;

        public int Pass
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
                Console.WriteLine("Wrong username!");
                flop = true;
            }

            if (pass != password)
            {
                Console.WriteLine("Wrong password!");
                return flop;
            }

            return flop;
        }
    }
}
