using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Common;

namespace Client
{
    static class Client
    {
        //TODO error labels
        //TODO maybe graphics
        //TODO check all values
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RemotingConfiguration.Configure("Client.exe.config", false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            Application.Run();
        }
    }
}
