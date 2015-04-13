using Common;
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
    public partial class LoginForm : Form
    {
        IUserList ul;
        public LoginForm()
        {
            InitializeComponent();
            ul = (IUserList) RemoteNew.New(typeof(IUserList));
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ul.checkLogin(usernameBox.Text, passwordBox.Text.GetHashCode()))
            {
                usernameBox.Text = "Pintou";
                nameBox.Text = "";
                passwordBox.Text = "Caralho!";

                openMainForm();
            }
            else
            {
                errorLabel.Text = "Login failed. Please check your username/password";
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            ul.addUser(usernameBox.Text, nameBox.Text, passwordBox.Text.GetHashCode());
            usernameBox.Text = "Registo";
            nameBox.Text = "Deu";
            passwordBox.Text = "Caralho!";
            openMainForm();
        }

        private void openMainForm()
        {
            MainForm mainForm = new MainForm(usernameBox.Text);
            this.Hide();
            mainForm.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Application.Exit();
        }
    }
}
