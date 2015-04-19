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
        ConcurrentDictionary<string, List<Diginote>> diginotes;
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
                    diginotes = js.Deserialize<ConcurrentDictionary<string, List<Diginote>>>(reader);
                }
            }
            else
            {
                diginotes = new ConcurrentDictionary<string, List<Diginote>>();
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
            sw.Close();
        }


        public void addDigiNotes(string username)
        {
            if (diginotesInSystem < MAX_DIGINOTES)
            {
                Random r = new Random();
                List<Diginote> clientDiginotes;
                diginotes.TryGetValue(username, out clientDiginotes);

                if (clientDiginotes == null)
                {
                    clientDiginotes = new List<Diginote>();
                }

                for (int i = 0; i < DIGINOTES_TO_DISTRIBUTE; i++)
                {
                    Diginote dig = new Diginote(r);
                    clientDiginotes.Add(dig);
                }

                diginotesInSystem += clientDiginotes.Count;
                diginotes.TryAdd(username, clientDiginotes);
                saveDiginotes();
            }
        }

        public int getDiginotes(string username)
        {
            List<Diginote> list;
            diginotes.TryGetValue(username, out list);
            if (list == null)
            {
                return 0;
            }
            else
            {
                return list.Count;
            }
        }

        public float getCotacao()
        {
            return cotacao;
        }

        public List<Order> addOrder(Order order)
        {
            var iterator = sell.GetEnumerator();

            while (iterator.MoveNext())
            {
                if (iterator.Current.Username == order.Username)
                {

                    Log.getInstance().printLog("Already exist order from same username " + order.Username);
                    return null;
                }
            }

            iterator = buy.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Username == order.Username)
                {

                    Log.getInstance().printLog("Already exist order from same username " + order.Username);
                    return null;
                }
            }

            List<Order> list;
            if (order.Type == "sell")
            {
                Log.getInstance().printLog("Added new sell order");
                sell.Enqueue(order);
                list = concretizeOrder();
                saveOrders();
                return list;
            }
            else if (order.Type == "buy")
            {

                Log.getInstance().printLog("Added new buy order");
                buy.Enqueue(order);
                list = concretizeOrder();
                saveOrders();
                return list;
            }

            return null;
        }

        private List<Order> concretizeOrder()
        {
            Order firstBuy = null;
            Order firstSell = null;
            int transactionAmmount;

            buy.TryPeek(out firstBuy);
            sell.TryPeek(out firstSell);

            if (firstBuy == null || firstSell == null)
            {
                Log.getInstance().printLog("Não há ordens a concretizar");
                return null;
            }

            transactionAmmount = Math.Min((int)firstBuy.NDiginotes, (int)firstSell.NDiginotes);

            List<Diginote> sellerDig = new List<Diginote>();
            List<Diginote> buyerDig = new List<Diginote>();
            diginotes.TryGetValue(firstSell.Username, out sellerDig);
            diginotes.TryGetValue(firstBuy.Username, out buyerDig);

            if (sellerDig.Count < transactionAmmount)
            {
                Log.getInstance().printLog("O Seller não tem suficientes");
                return null;
            }

            Diginote tempDig;

            for (int i = 0; i < transactionAmmount; i++)
            {
                tempDig = sellerDig.ElementAt(0);
                sellerDig.RemoveAt(0);
                buyerDig.Add(tempDig);
            }

            diginotes[firstBuy.Username] = buyerDig;
            diginotes[firstSell.Username] = sellerDig;

            saveDiginotes();

            Log.getInstance().printLog("Um coiso tipo fez transação");

            Order or;

            if (transactionAmmount < firstBuy.NDiginotes)
            {
                firstBuy.NDiginotes = firstBuy.NDiginotes - transactionAmmount;
                sell.TryDequeue(out or);
                Log.getInstance().printLog("Removeu Sell Order");
            }
            else if (transactionAmmount < firstSell.NDiginotes)
            {
                firstSell.NDiginotes = firstSell.NDiginotes - transactionAmmount;
                buy.TryDequeue(out or);
                Log.getInstance().printLog("Removeu buy Order");
            }
            else
            {
                buy.TryDequeue(out or);
                sell.TryDequeue(out or);
                Log.getInstance().printLog("Removed 2 orders");
            }

            List<Order> list = new List<Order>();
            list.Add(firstBuy);
            list.Add(firstSell);
            return list;
        }

    }
}
