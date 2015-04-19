using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Client
{
    public partial class Antonio : Form
    {
        public Order order { get; set; }
        public bool res { get; set; }
        public Antonio(Order order,IOperation iop)
        {
            InitializeComponent();

            this.order = order;

            nDiginotesLabel.Text = order.NDiginotes.ToString();
            newValueLabel.Text = iop.getCotacao().ToString();
            oldValueLabel.Text = order.Cotacao.ToString();
            res = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            res = true;
            this.Close();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
