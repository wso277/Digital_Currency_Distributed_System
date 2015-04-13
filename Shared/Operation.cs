using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote
{
    public class Operation:MarshalByRefObject, IOperation
    {
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

        public void addOperation()
        {
            Controller.getInstance().addOrder(type, value, nDiginotes, username);
        }

        public int getDiginotes(string username)
        {
            return Controller.getInstance().getDiginotes(username);
        }

        public void addDiginotes(string username)
        {
            Controller.getInstance().addDigiNotes(username);
        }

    }
}
