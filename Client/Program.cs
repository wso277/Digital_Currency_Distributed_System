using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            RemotingConfiguration.Configure("Client.exe.config", false);
            UserList users = UserList.getInstance();

            int pass = "password".GetHashCode();
            if (users.addUser("username", "Name", pass))
            {
                Console.WriteLine("PINTOU");
            }
            else
            {
                Console.WriteLine( "NAO PINTOU");
            }

            if (users.checkLogin("username", pass))
            {
                Console.WriteLine("C#! C#! C#!");
            }
            else
            {
                Console.WriteLine("NINGUEM QUER SABER");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
