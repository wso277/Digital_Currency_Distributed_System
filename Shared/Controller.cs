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
        List<Order> sell;
        List<Order> buy;

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

                sell = JsonConvert.DeserializeObject<List<Order>>(sellString);
                buy = JsonConvert.DeserializeObject<List<Order>>(buyString);
                /*sell = new ConcurrentQueue<Order>(sellList);
                buy = new ConcurrentQueue<Order>(buyList);
                */
            }
            else
            {
                sell = new List<Order>();
                buy = new List<Order>();
            }
        }
        private void saveOrders()
        {
            string sellJson = JsonConvert.SerializeObject(sell);
            string buyJson = JsonConvert.SerializeObject(buy);

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
                sell.Add(order);
                list = concretizeOrder();
                saveOrders();
                return list;
            }
            else if (order.Type == "buy")
            {

                Log.getInstance().printLog("Added new buy order");
                buy.Add(order);
                list = concretizeOrder();
                saveOrders();
                return list;
            }

            return null;
        }

        public List<Order> concretizeOrder()
        {
            Order firstBuy = null;
            Order firstSell = null;
            int transactionAmount;


            if (buy.Count > 0)
            {
                firstBuy = buy.ElementAt(0);
            }
            
            if(sell.Count > 0)
            {
                firstSell = sell.ElementAt(0);
            }


            if (firstBuy == null || firstSell == null)
            {
                Log.getInstance().printLog("Não há ordens a concretizar");
                List<Order> list = new List<Order>();
                list.Add(new Order("dfgndfgk", 0.0f, 0, "sffgdj"));
                return list;
            }

            transactionAmount = Math.Min((int)firstBuy.NDiginotes, (int)firstSell.NDiginotes);

            List<Diginote> sellerDig = new List<Diginote>();
            List<Diginote> buyerDig = new List<Diginote>();
            diginotes.TryGetValue(firstSell.Username, out sellerDig);
            diginotes.TryGetValue(firstBuy.Username, out buyerDig);

            /*if (sellerDig.Count < transactionAmount)
            {
                Log.getInstance().printLog("O Seller não tem suficientes");
                return null;
            }*/

            if (sellerDig == null)
            {
                sellerDig = new List<Diginote>();
            }
            if (buyerDig == null)
            {
                buyerDig = new List<Diginote>();
            }

            Diginote tempDig;

            for (int i = 0; i < transactionAmount; i++)
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

            if (transactionAmount < firstBuy.NDiginotes)
            {
                firstBuy.NDiginotes = firstBuy.NDiginotes - transactionAmount;
                firstSell.NDiginotes = 0;
                sell.RemoveAt(0);
                Log.getInstance().printLog("Removeu Sell Order");
            }
            else if (transactionAmount < firstSell.NDiginotes)
            {
                firstSell.NDiginotes = firstSell.NDiginotes - transactionAmount;
                firstBuy.NDiginotes = 0;
                buy.RemoveAt(0);
                Log.getInstance().printLog("Removeu buy Order");
            }
            else
            {
                firstBuy.NDiginotes = 0;
                firstSell.NDiginotes = 0;
                buy.RemoveAt(0);
                sell.RemoveAt(0);
                Log.getInstance().printLog("Removed 2 orders");
            }

            List<Order> placeboList = new List<Order>();
            Log.getInstance().printLog(firstBuy.NDiginotes.ToString());
            Log.getInstance().printLog(firstSell.NDiginotes.ToString());
            placeboList.Add(firstBuy);
            placeboList.Add(firstSell);
            return placeboList;
        }


        public void updateOrder(Order order)
        {
            if (order.Type == "buy")
            {
                for (int i = 0; i < buy.Count; i++)
                {
                    if (buy.ElementAt(i).Username == order.Username)
                    {
                        buy[i] = order;
                        cotacao = order.Cotacao;
                    }
                    else
                    {
                        buy.ElementAt(i).Cotacao = order.Cotacao;
                    }
                }
            } 
            if (order.Type == "sell")
            {
                for (int i = 0; i < sell.Count; i++)
                {
                    if (sell.ElementAt(i).Username == order.Username)
                    {
                        sell[i] = order;
                        cotacao = order.Cotacao;
                    }
                    else
                    {
                        sell.ElementAt(i).Cotacao = order.Cotacao;
                    }
                }
            }


        }

        internal void removeOrder(Order order)
        {
            if (order.Type == "buy")
            {
                for (int i = 0; i < buy.Count; i++)
                {
                    if (buy.ElementAt(i).Username == order.Username)
                    {
                        buy.RemoveAt(i);
                        return;
                    }
                }
            }
            if (order.Type == "sell")
            {
                for (int i = 0; i < sell.Count; i++)
                {
                    if (sell.ElementAt(i).Username == order.Username)
                    {
                        sell.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
