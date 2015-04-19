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
        public Handlers handlersObj;

        public MainForm(string username, bool newU)
        {
            this.username = username;
            InitializeComponent();
            iop = (IOperation)RemoteNew.New(typeof(IOperation));
            if (newU)
            {
                iop.addDiginotes(username);
            }

            loadOrders();
            nDiginotesLabel.Text = iop.getDiginotes(username).ToString();
            cotacaoLabel.Text = iop.getCotacao().ToString();

            handlersObj = new Handlers(iop);
            handlersObj.updateOrderEvent += updateOrderHandler;
            handlersObj.blockInterface += blockInterfaceHandler;

            updateOrderTable();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
            operationHistoryTable.Controls.Add(new Label() { Text = "Diginotes Remaining", Anchor = AnchorStyles.Top, AutoSize = true }, 1, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Value/Diginote", Anchor = AnchorStyles.Top, AutoSize = true }, 2, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "Number of Diginotes", Anchor = AnchorStyles.Top, AutoSize = true }, 3, 0);
            operationHistoryTable.Controls.Add(new Label() { Text = "State", Anchor = AnchorStyles.Top, AutoSize = true }, 4, 0);

            for (int c = 0, line = 1; line <= orders.Count; line++)
            {
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).Type, Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).NDiginotes.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).Cotacao.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = ((float)orders.ElementAt<Order>(line - 1).TotalDiginotes).ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders.ElementAt<Order>(line - 1).Status, Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
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
            sw.Close();
        }


        private void blockInterfaceHandler(Order order)
        {
            //SetButtonStatus(false);
            if (username != order.Username)
            {

                if (orders.Count > 0 && orders.ElementAt(orders.Count - 1).Status == "Not Finished")
                {

                    Antonio confirmCotacao = new Antonio(orders.ElementAt(orders.Count - 1), iop);
                    confirmCotacao.ShowDialog();

                    if (confirmCotacao.res)
                    {
                        orders.ElementAt(orders.Count - 1).Cotacao = iop.getCotacao();
                        confirmCotacao.Dispose();
                    }
                    else
                    {
                        iop.removeOrder(orders.ElementAt(orders.Count - 1));
                        orders.RemoveAt(orders.Count - 1);
                        confirmCotacao.Dispose();
                    }
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                updateOrderTable();
                cotacaoLabel.Text = iop.getCotacao().ToString();
            });
        }

        public void updateOrderHandler(Order buyerOrder, Order sellerOrder)
        {
            Log.getInstance().printLog("UPDATE ORDER HANDLER");

            if (orders.ElementAt(orders.Count-1).Username == buyerOrder.Username)
            {
                if (buyerOrder.NDiginotes > 0)
                {
                    orders.ElementAt(orders.Count - 1).NDiginotes = buyerOrder.NDiginotes;
                }
                else
                {
                    orders.ElementAt(orders.Count - 1).NDiginotes = 0;
                    orders.ElementAt(orders.Count - 1).Status = "Fulfilled";
                }
            }
            if (orders.ElementAt(orders.Count - 1).Username == sellerOrder.Username)
            {
                if (sellerOrder.NDiginotes > 0)
                {
                    orders.ElementAt(orders.Count - 1).NDiginotes = sellerOrder.NDiginotes;
                }
                else
                {
                    orders.ElementAt(orders.Count - 1).NDiginotes = 0;
                    orders.ElementAt(orders.Count - 1).Status = "Fulfilled";
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                updateOrderTable();
                nDiginotesLabel.Text = iop.getDiginotes(username).ToString();
            });

            //SetButtonStatus(true);
        }

        private void SetButtonStatus(bool status)
        {
            venderButton.Enabled = status;
            comprarButton.Enabled = status;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //TODO voltar ao menu de login
        }

        private void EditOrderButton_Click(object sender, EventArgs e)
        {
            if (orders.ElementAt(orders.Count - 1).Status == "Not Finished")
            {
                EditOrder editOrderDialog = new EditOrder(orders.ElementAt(orders.Count - 1), iop);
                editOrderDialog.ShowDialog();
                if (editOrderDialog.update)
                {
                    if (editOrderDialog.order.Cotacao == 0)
                    {
                        orders.RemoveAt(orders.Count - 1);
                        iop.removeOrder(editOrderDialog.order);
                        editOrderDialog.Dispose();
                    }
                    else
                    {
                        orders[orders.Count - 1] = editOrderDialog.order;
                        iop.updateOrder(orders.ElementAt(orders.Count - 1));
                        editOrderDialog.Dispose();
                    }
                }
                else
                {
                    editOrderDialog.Dispose();
                }

                updateOrderTable();
                cotacaoLabel.Text = iop.getCotacao().ToString();
            }
        }

    }
}
