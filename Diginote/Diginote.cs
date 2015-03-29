using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diginote
{
    public class Diginote
    {
        ulong serialNumber;
        double val;

        public ulong Serial
        {
            get
            {
                return serialNumber;
            }
        }

        public double Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }

        public Diginote(ulong value = 1)
        {
            val = value;
            serialNumber = (ulong)DateTime.Now.GetHashCode();
        }
    }
}
