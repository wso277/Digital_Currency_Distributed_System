using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote
{
    public class Diginote
    {
        public ulong serialNumber { get; set; }
        double val;


        public Diginote() { }
        public Diginote(Random r, ulong value = 1)
        {
            val = value;
            serialNumber = (ulong)DateTime.Now.GetHashCode() + (ulong)r.Next(Int32.MaxValue);
        }
    }
}
