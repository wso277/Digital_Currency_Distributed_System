using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;
using System.Diagnostics;

namespace Client
{
    static class Client
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            RemotingConfiguration.Configure("Client.exe.config", false);
            UserList users = new UserList();

            int pass = "kekk".GetHashCode();
            if (users.addUser("username2", "Name", pass))
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Adicionei User");
            }
            else
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Não Adicionei User");
            }

            if (users.checkLogin("username2", pass))
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Guardou User, Fez Login");
            }
            else
            {
                Console.WriteLine(Process.GetCurrentProcess().ProcessName + " " + "Não Fez Login");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
