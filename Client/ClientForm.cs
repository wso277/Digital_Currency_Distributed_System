using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        UserList ul;
        public ClientForm()
        {
            InitializeComponent();
            RemotingConfiguration.Configure("Client.exe.config", false);
            ul = new UserList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ul.checkLogin(usernameBox.Text, passwordBox.Text.GetHashCode()))
            {
                usernameBox.Text = "Pintou";
                passwordBox.Text = "Caralho!";
            }
        }
    }
}
