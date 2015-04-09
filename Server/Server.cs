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
        static string pathToDiginoteDB = "diginotes.db";
        static ConcurrentDictionary<string, ulong> diginotes;
        static int maxDiginotes = 1000;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log = new Log("log.txt");

            RemotingConfiguration.Configure("Server.exe.config", false);



            loadDiginotes();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            saveDiginotes();
        }

        private static void loadDiginotes()
        {
            if (File.Exists(pathToDiginoteDB))
            {

                JsonSerializer js = new JsonSerializer();
                js.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sw = new StreamReader(pathToDiginoteDB))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    diginotes = js.Deserialize<ConcurrentDictionary<string, ulong>>(reader);
                }
            }
            else
            {
                diginotes = new ConcurrentDictionary<string, ulong>();
            }
        }
        private static void saveDiginotes()
        {
            JsonSerializer js = new JsonSerializer();
            js.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(pathToDiginoteDB))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                js.Serialize(writer, diginotes);
            }
        }

        static public Log getLog()
        {
            return log;
        }
    }
}
