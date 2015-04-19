using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IOperation
    {
        event UpdateOrder updateOrderEvent;

        event BlockInterface blockInterface;

        int getDiginotes(string username);

        void addDiginotes(string username);

        float getCotacao();

        bool addOrder(Order order);

        void removeOrder(Order order);

        void updateOrder(Order order);
    }
}
