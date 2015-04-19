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

        int getDiginotes(string username);

        void addDiginotes(string username);

        float getCotacao();

        bool addOrder(Order order);
    }
}
