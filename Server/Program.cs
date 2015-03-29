using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;

namespace Server
{
    class Program
    {
        static Log log;
        static UserList users;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            users = UserList.getInstance();
            log = new Log("log.txt");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        static public Log getLog()
        {
            return log;
        }
    }
}
