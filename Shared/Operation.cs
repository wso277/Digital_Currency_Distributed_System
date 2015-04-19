using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Remote
{
    public class Operation:MarshalByRefObject, IOperation
    {
        public event UpdateOrder updateOrderEvent;
        string type;
        float value;
        int nDiginotes;
        string username;

        public Operation(){}

        public Operation(string type, int nDiginotes, string username)
        {
            this.type = type;
            this.nDiginotes = nDiginotes;
            this.username = username;
        }

        public int getDiginotes(string username)
        {
            return Controller.getInstance().getDiginotes(username);
        }

        public void addDiginotes(string username)
        {
            Controller.getInstance().addDigiNotes(username);
        }

        public float getCotacao()
        {
            return Controller.getInstance().getCotacao();
        }

        public bool addOrder(Order order)
        {
            List<Order> list = Controller.getInstance().addOrder(order);
            if (list != null && list.Count == 2)
                NotifyOrdersDispatch(list.ElementAt(0), list.ElementAt(1));
            return true;
        }

        private void NotifyOrdersDispatch(Order buyerOrder, Order sellerOrder)
        {
            if (updateOrderEvent != null)
            {
                Delegate[] invkList = updateOrderEvent.GetInvocationList();

                foreach (UpdateOrder handler in invkList)
                {
                    Console.WriteLine("[Entities]: Event triggered: invoking handler");
                    object[] pars = { handler, buyerOrder, sellerOrder };
                    new Thread(TriggerOrdEvent).Start(pars);
                }
            }
        }

        private void TriggerOrdEvent(object pars)
        {
            UpdateOrder handler = (UpdateOrder)((object[])pars)[0];
            Order buyerOrder = (Order)((object[])pars)[1];
            Order sellerOrder = (Order)((object[])pars)[2];
            try
            {
                handler(buyerOrder, sellerOrder);
            }
            catch (Exception)
            {
                Console.WriteLine("[TriggerOrderEvent]: Exception");
                updateOrderEvent -= handler;
            }
        }
    }
}
