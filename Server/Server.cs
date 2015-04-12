using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Common;

namespace Server
{
    class Server
    {
        static Log log;

        [STAThread]
        static void Main()
        {
            log = new Log("log.txt");

            RemotingConfiguration.Configure("Server.exe.config", false);

        }

        static public Log getLog()
        {
            return log;
        }
    }
}
