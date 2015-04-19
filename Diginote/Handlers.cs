using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public delegate void UpdateOrder(Order buyerOrder, Order sellerOrder);
    public class Handlers : MarshalByRefObject
    {

        public event UpdateOrder updateOrderEvent;

        public Handlers(IOperation iop)
        {
            iop.updateOrderEvent += FireOrderNotify;
        }

        public void FireOrderNotify(Order buyerOrder, Order sellerOrder)
        {
            updateOrderEvent(buyerOrder, sellerOrder);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
