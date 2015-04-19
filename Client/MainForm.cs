using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.IO;
using Newtonsoft.Json;

namespace Client
{
    public partial class MainForm : Form
    {
        string pathToOrdersDB = "orders.db";
        IOperation iop;
        string username;
        List<Order> orders;
        public MainForm(string username, bool newU)
        {
            this.username = username;
            InitializeComponent();
            iop = (IOperation) RemoteNew.New(typeof(IOperation));
            if (newU)
            {
                iop.addDiginotes(username);
            }

            loadOrders();
            nDiginotesLabel.Text = iop.getDiginotes(username).ToString();
            cotacaoLabel.Text = iop.getCotacao().ToString();

            updateOrderTable();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            operationHistoryTable.Controls.Add(new Label() { Text = "Type", Anchor = AnchorStyles.Top, AutoSize = true }, 0, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Ammount of Diginotes", Anchor = AnchorStyles.Top, AutoSize = true }, 1, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Value/Diginote", Anchor = AnchorStyles.Top, AutoSize = true }, 2, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "TotalValue", Anchor = AnchorStyles.Top, AutoSize = true }, 3, 0);
            
        }

        private void nDiginotesLabel_Click(object sender, EventArgs e)
        {
            nDiginotesLabel.Text = iop.getDiginotes(username).ToString();
        }

        private void venderButton_Click(object sender, EventArgs e)
        {
            decimal nDiginotes = nDiginotesSpinner.Value;

            if (nDiginotes > 0 && nDiginotes <= iop.getDiginotes(username))
            {
                Order sell = new Order(username, iop.getCotacao(), nDiginotes, "sell");
                if (iop.addOrder(sell))
                {
                    orders.Add(sell);
                    saveOrders();
                    updateOrderTable();
                }
                else
                {
                    //TODO ADD ERROR LABEL
                }
            }
        }

        private void comprarButton_Click(object sender, EventArgs e)
        {
            decimal nDiginotes = nDiginotesSpinner.Value;

            if (nDiginotes > 0)
            {
                Order buy = new Order(username, iop.getCotacao(), nDiginotes, "buy");
                if (iop.addOrder(buy))
                {
                    orders.Add(buy);
                    saveOrders();
                    updateOrderTable();
                }
                else
                {
                    //TODO ADD error label
                }
            }
        }

        private void updateOrderTable()
        {
            operationHistoryTable.Controls.Clear();
            operationHistoryTable.Controls.Add(new Label() { Text = "Type", Anchor = AnchorStyles.Top, AutoSize = true }, 0, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Ammount of Diginotes", Anchor = AnchorStyles.Top, AutoSize = true }, 1, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Value/Diginote", Anchor = AnchorStyles.Top, AutoSize = true }, 2, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "TotalValue", Anchor = AnchorStyles.Top, AutoSize = true }, 3, 0);

            for (int c = 0, line = 1; line <= orders.Count; line++)
            {
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line-1).Type, Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).NDiginotes1.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).Cotacao.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = ((float)orders.ElementAt<Order>(line - 1).NDiginotes1 * orders.ElementAt<Order>(line - 1).Cotacao).ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                c = 0;
            }
        }

        
        private void loadOrders()
        {
            if (File.Exists(pathToOrdersDB))
            {
                StreamReader rw = new StreamReader(pathToOrdersDB);
                string orderString = rw.ReadLine();

                orders = JsonConvert.DeserializeObject<List<Order>>(orderString);

            }
            else
            {
                orders = new List<Order>();
            }
        }
        private void saveOrders()
        {
            string ordersString = JsonConvert.SerializeObject(orders);

            StreamWriter sw;
            sw = File.CreateText(pathToOrdersDB);

            sw.WriteLine(ordersString);
            sw.Flush();
        }
    }
}
