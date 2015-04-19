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
    public partial class EditOrder : Form
    {
        public Order order {get;set;}
        public bool update { get; set; }
        IOperation iop;
        public EditOrder(Order order, IOperation iop)
        {
            InitializeComponent();
            this.order = order;
            spinnerNDiginotes.Value = this.order.NDiginotes;
            spinnerCotacao.Value = (decimal)this.order.Cotacao;
            update = false;
            this.iop = iop;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancelOrderButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeOrderButton_Click(object sender, EventArgs e)
        {
            order.Cotacao = 0;
            update = true;
            this.Close();
        }

        private void updateOrderButton_Click(object sender, EventArgs e)
        {
            if (order.Type == "buy" && spinnerCotacao.Value >= (decimal)order.Cotacao)
            {
                order.Cotacao = (float)spinnerCotacao.Value;
            }
            if (order.Type == "sell" && spinnerCotacao.Value <= (decimal)order.Cotacao)
            {
                order.Cotacao = (float)spinnerCotacao.Value;
            }
            if (spinnerNDiginotes.Value > 0 && spinnerNDiginotes.Value <= iop.getDiginotes(order.Username))
                order.NDiginotes = spinnerNDiginotes.Value;

            update = true;
            this.Close();
        }
    }
}
