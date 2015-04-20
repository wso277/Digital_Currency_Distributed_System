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
using System.Threading;

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
            pathToOrdersDB = "orders_" + username + ".db";
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
                orders.Add(sell);
                if (iop.addOrder(sell))
                {
                    saveOrders();
                    updateOrderTable();
                }
                else
                {
                    orders.RemoveAt(orders.Count - 1);
                    messageLabel.Text = "An order is already placed. Remove your order before placing a new one.";
                }
            }
            else
            {
                messageLabel.Text = "Not enough diginotes for that transaction.";

            }
        }

        private void comprarButton_Click(object sender, EventArgs e)
        {
            decimal nDiginotes = nDiginotesSpinner.Value;



            if (nDiginotes > 0)
            {
                Order buy = new Order(username, iop.getCotacao(), nDiginotes, "buy");

                orders.Add(buy);
                if (iop.addOrder(buy))
                {
                    saveOrders();
                    updateOrderTable();
                }
                else
                {
                    orders.RemoveAt(orders.Count - 1);
                    messageLabel.Text = "An order is already placed. Remove your order before placing a new one.";
                }
            }
            else
            {
                messageLabel.Text = "Not enough diginotes for that transaction.";

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
                operationHistoryTable.Controls.Add(new Label() { Text = orders[(line - 1)].Type, Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders[(line - 1)].NDiginotes.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders[line - 1].Cotacao.ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = ((float)orders[line - 1].TotalDiginotes).ToString(), Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                operationHistoryTable.Controls.Add(new Label() { Text = orders[line - 1].Status, Anchor = AnchorStyles.Top, AutoSize = true }, c++, line);
                c = 0;
            }
        }


        private void loadOrders()
        {
            if (File.Exists(pathToOrdersDB))
            {
                using (StreamReader rw = new StreamReader(pathToOrdersDB))
                {

                    string orderString = rw.ReadLine();

                    orders = JsonConvert.DeserializeObject<List<Order>>(orderString);
                }

            }
            else
            {
                orders = new List<Order>();
            }
        }
        private void saveOrders()
        {
            string ordersString = JsonConvert.SerializeObject(orders);

            using (StreamWriter sw = new StreamWriter(pathToOrdersDB, false))
            {
                sw.WriteLine(ordersString);
                sw.Flush();
            }
        }


        private void blockInterfaceHandler(Order order)
        {
            this.Invoke((MethodInvoker)delegate
            {
                messageLabel.Text = "Diginote value changed. Waiting for all peers.";
            });

            if (username != order.Username)
            {

                if (orders.Count > 0 && orders[orders.Count - 1].Status == "Not Finished")
                {

                    Antonio confirmCotacao = new Antonio(orders[orders.Count - 1], iop);
                    confirmCotacao.ShowDialog();

                    if (confirmCotacao.res)
                    {
                        orders[orders.Count - 1].Cotacao = iop.getCotacao();
                        confirmCotacao.Dispose();
                    }
                    else
                    {
                        iop.removeOrder(orders[orders.Count - 1]);
                        orders.RemoveAt(orders.Count - 1);
                        confirmCotacao.Dispose();
                    }
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                updateOrderTable();
                cotacaoLabel.Text = iop.getCotacao().ToString();
                SetButtonStatus(false);
                Thread.Sleep(5000);
                SetButtonStatus(true);
            });

            saveOrders();

        }

        public void updateOrderHandler(Order buyerOrder, Order sellerOrder)
        {
            Log.getInstance().printLog("UPDATE ORDER HANDLER");

            if (orders[orders.Count - 1].Username == buyerOrder.Username)
            {
                if (buyerOrder.NDiginotes > 0)
                {
                    orders[orders.Count - 1].NDiginotes = buyerOrder.NDiginotes;
                }
                else
                {
                    orders[orders.Count - 1].NDiginotes = 0;
                    orders[orders.Count - 1].Status = "Fulfilled";
                }
            }
            if (orders[orders.Count - 1].Username == sellerOrder.Username)
            {
                if (sellerOrder.NDiginotes > 0)
                {
                    orders[orders.Count - 1].NDiginotes = sellerOrder.NDiginotes;
                }
                else
                {
                    orders[orders.Count - 1].NDiginotes = 0;
                    orders[orders.Count - 1].Status = "Fulfilled";
                }
            }

            this.Invoke((MethodInvoker)delegate
            {
                updateOrderTable();
                nDiginotesLabel.Text = iop.getDiginotes(username).ToString();
            });

        }

        private void SetButtonStatus(bool status)
        {
            venderButton.Enabled = status;
            comprarButton.Enabled = status;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            saveOrders();
            this.Hide();
            loginForm.ShowDialog();
        }

        private void EditOrderButton_Click(object sender, EventArgs e)
        {
            if (orders.Count > 0)
            {
                if (orders[orders.Count - 1].Status == "Not Finished")
                {
                    EditOrder editOrderDialog = new EditOrder(orders[orders.Count - 1], iop);
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
                            iop.updateOrder(orders[orders.Count - 1]);
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
            else
            {
                messageLabel.Text = "No current orders. Please add an order in order to edit.";
            }

            saveOrders();
        }

    }
}
