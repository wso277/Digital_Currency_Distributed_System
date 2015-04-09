using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    public class Controller
    {
        static Controller controller = null;
        string pathToDiginoteDB = "diginotes.db";
        ConcurrentDictionary<ulong, string> diginotes;
        static int MAX_DIGINOTES = 1000;
        static int DIGINOTES_TO_DISTRIBUTE = 500;
        int diginotesInSystem = 0;
        int digiValue = 1;

        public static Controller getInstance() {
            if (controller == null)
            {
                controller = new Controller();
            }
            return controller;
        }

        public Controller()
        {

            loadDiginotes();

            diginotesInSystem = diginotes.Count;
        }

        private void loadDiginotes()
        {
            if (File.Exists(pathToDiginoteDB))
            {

                JsonSerializer js = new JsonSerializer();
                js.NullValueHandling = NullValueHandling.Ignore;
                using (StreamReader sw = new StreamReader(pathToDiginoteDB))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    diginotes = js.Deserialize<ConcurrentDictionary<ulong, string>>(reader);
                }
            }
            else
            {
                diginotes = new ConcurrentDictionary<ulong, string>();
            }
        }
        private void saveDiginotes()
        {
            JsonSerializer js = new JsonSerializer();
            js.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(pathToDiginoteDB))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                js.Serialize(writer, diginotes);
            }
        }


        public void addDigiNotes(string username)
        {
            if (diginotesInSystem < MAX_DIGINOTES)
            {
                for (int i = 0; i < DIGINOTES_TO_DISTRIBUTE; i++)
                {
                    Diginote dig = new Diginote();
                    diginotes.GetOrAdd(dig.serialNumber, username);
                }
            }
        }

        public void changeDiginoteOwner(string[] diginotes,string oldUsername,string newUsername) {

        }
    }
}
