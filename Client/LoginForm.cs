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
                openMainForm(false);
            }
            else
            {
                errorLabel.Text = "Login failed. Please check your username/password";
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (usernameBox.Text != "" && nameBox.Text != "" && passwordBox.Text != "")
            {
                if (ul.addUser(usernameBox.Text, nameBox.Text, passwordBox.Text.GetHashCode()))
                {

                    openMainForm(true);
                }
                else
                {
                    errorLabel.Text = "User already registered with that username";
                }
            }
            else
            {
                errorLabel.Text = "Please fill all fields to register";
            }
        }

        private void openMainForm(bool newU)
        {
            MainForm mainForm = new MainForm(usernameBox.Text, newU);
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
