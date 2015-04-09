﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
    }
}
