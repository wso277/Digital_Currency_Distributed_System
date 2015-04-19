using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public delegate void UpdateOrder(Order buyerOrder, Order sellerOrder);
    public delegate void BlockInterface(Order order);

    public class Handlers : MarshalByRefObject
    {

        public event UpdateOrder updateOrderEvent;
        public event BlockInterface blockInterface;

        public Handlers(IOperation iop)
        {
            iop.updateOrderEvent += FireOrderNotify;
            iop.blockInterface += ShootOrderNotify;
        }

        public void FireOrderNotify(Order buyerOrder, Order sellerOrder)
        {
            updateOrderEvent(buyerOrder, sellerOrder);
        }

        public void ShootOrderNotify(Order order)
        {
            blockInterface(order);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
