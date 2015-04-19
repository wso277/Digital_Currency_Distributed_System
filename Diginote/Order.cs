using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Order
    {
        string username;
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        float cotacao;

        public float Cotacao
        {
            get { return cotacao; }
            set { cotacao = value; }
        }
        decimal nDiginotes;

        public decimal NDiginotes
        {
            get { return nDiginotes; }
            set { nDiginotes = value; }
        }
        string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Order(string username, float cotacao, decimal nDiginotes, string type)
        {
            this.username = username;
            this.cotacao = cotacao;
            this.nDiginotes = nDiginotes;
            this.type = type;
            this.status = "Not Finished";
        }


    }
}
