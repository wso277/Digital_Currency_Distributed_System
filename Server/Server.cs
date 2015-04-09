using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace Server
{
    class Server
    {
        static Log log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log = new Log("log.txt");

            RemotingConfiguration.Configure("Server.exe.config", false);

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
