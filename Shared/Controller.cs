using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Remote
{
    public class Controller
    {
        static Controller controller = null;
        string pathToDiginoteDB = "diginotes.db";
        string pathToOrdersDB = "orders.db";
        ConcurrentDictionary<ulong, string> diginotes;
        ConcurrentQueue<Order> sell;
        ConcurrentQueue<Order> buy;

        static int MAX_DIGINOTES = 1000;
        static int DIGINOTES_TO_DISTRIBUTE = 500;
        int diginotesInSystem = 0;
        int digiValue = 1;
        public float cotacao = 1;

        public static Controller getInstance()
        {
            if (controller == null)
            {
                controller = new Controller();
            }
            return controller;
        }

        private Controller()
        {

            loadDiginotes();
            loadOrders();

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

        private void loadOrders()
        {
            if (File.Exists(pathToOrdersDB))
            {
                StreamReader rw = new StreamReader(pathToOrdersDB);
                string sellString = rw.ReadLine();
                string buyString = rw.ReadLine();

                List<Order> sellList = JsonConvert.DeserializeObject<List<Order>>(sellString);
                List<Order> buyList = JsonConvert.DeserializeObject<List<Order>>(buyString);
                sell = new ConcurrentQueue<Order>(sellList);
                buy = new ConcurrentQueue<Order>(buyList);

            }
            else
            {
                sell = new ConcurrentQueue<Order>();
                buy = new ConcurrentQueue<Order>();
            }
        }
        private void saveOrders()
        {
            List<Order> sellList = new List<Order>(sell);
            List<Order> buyList = new List<Order>(buy);
            string sellJson = JsonConvert.SerializeObject(sellList);
            string buyJson = JsonConvert.SerializeObject(buyList);

            StreamWriter sw;
            sw = File.CreateText(pathToOrdersDB);

            sw.WriteLine(sellJson);
            sw.WriteLine(buyJson);
            sw.Flush();
        }


        public void addDigiNotes(string username)
        {
            if (diginotesInSystem < MAX_DIGINOTES)
            {
                Random r = new Random();
                for (int i = 0; i < DIGINOTES_TO_DISTRIBUTE; i++)
                {
                    Diginote dig = new Diginote(r);
                    diginotes.TryAdd(dig.serialNumber, username);
                    diginotesInSystem = diginotes.Count;
                }
                saveDiginotes();
            }
        }

        public bool transaction(ulong[] diginotesToTransaction, string oldUsername, string newUsername)
        {
            foreach (ulong s in diginotesToTransaction)
            {
                string username;
                diginotes.TryGetValue(s, out username);
                if (username == oldUsername)
                {
                    diginotes.TryUpdate(s, newUsername, oldUsername);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public int getDiginotes(string username)
        {
            int nDiginotes = 0;
            foreach (string name in diginotes.Values)
            {
                if (name == username) nDiginotes++;
            }
            return nDiginotes;
        }

        public float getCotacao()
        {
            return cotacao;
        }

        public bool addOrder(Order order) {
            var iterator = sell.GetEnumerator();

            while (iterator.MoveNext()) {
                if (iterator.Current.Username == order.Username) {

                    Log.getInstance().printLog("Already exist order from same username " + order.Username);
                    return false;
                }
            }

            iterator = buy.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Username == order.Username)
                {

                    Log.getInstance().printLog("Already exist order from same username " + order.Username);
                    return false;
                }
            }

            if (order.Type == "sell")
            {
                Log.getInstance().printLog("Added new sell order");
                //sell.Enqueue(order);
                sell.Enqueue(order);
                saveOrders();
                return true;
            }
            else if (order.Type == "buy")
            {

                Log.getInstance().printLog("Added new buy order");
                //buy.Enqueue(order);
                buy.Enqueue(order);
                saveOrders();
                return true;
            }

            return false;
        }

    }
}
