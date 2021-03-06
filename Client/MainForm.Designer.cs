﻿namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logoutButton = new System.Windows.Forms.Button();
            this.cotacaoTextLabel = new System.Windows.Forms.Label();
            this.cotacaoLabel = new System.Windows.Forms.Label();
            this.venderButton = new System.Windows.Forms.Button();
            this.comprarButton = new System.Windows.Forms.Button();
            this.operationHistoryTable = new System.Windows.Forms.TableLayoutPanel();
            this.nDiginotesTextLabel = new System.Windows.Forms.Label();
            this.nDiginotesLabel = new System.Windows.Forms.Label();
            this.nDiginotesSpinner = new System.Windows.Forms.NumericUpDown();
            this.EditOrderButton = new System.Windows.Forms.Button();
            this.messageTextLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nDiginotesSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(528, 12);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 0;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // cotacaoTextLabel
            // 
            this.cotacaoTextLabel.AutoSize = true;
            this.cotacaoTextLabel.Location = new System.Drawing.Point(12, 17);
            this.cotacaoTextLabel.Name = "cotacaoTextLabel";
            this.cotacaoTextLabel.Size = new System.Drawing.Size(77, 13);
            this.cotacaoTextLabel.TabIndex = 1;
            this.cotacaoTextLabel.Text = "Cotação Atual:";
            // 
            // cotacaoLabel
            // 
            this.cotacaoLabel.AutoSize = true;
            this.cotacaoLabel.Location = new System.Drawing.Point(95, 17);
            this.cotacaoLabel.Name = "cotacaoLabel";
            this.cotacaoLabel.Size = new System.Drawing.Size(13, 13);
            this.cotacaoLabel.TabIndex = 2;
            this.cotacaoLabel.Text = "0";
            // 
            // venderButton
            // 
            this.venderButton.Location = new System.Drawing.Point(12, 275);
            this.venderButton.Name = "venderButton";
            this.venderButton.Size = new System.Drawing.Size(237, 24);
            this.venderButton.TabIndex = 3;
            this.venderButton.Text = "Vender";
            this.venderButton.UseVisualStyleBackColor = true;
            this.venderButton.Click += new System.EventHandler(this.venderButton_Click);
            // 
            // comprarButton
            // 
            this.comprarButton.Location = new System.Drawing.Point(362, 275);
            this.comprarButton.Name = "comprarButton";
            this.comprarButton.Size = new System.Drawing.Size(242, 24);
            this.comprarButton.TabIndex = 4;
            this.comprarButton.Text = "Comprar";
            this.comprarButton.UseVisualStyleBackColor = true;
            this.comprarButton.Click += new System.EventHandler(this.comprarButton_Click);
            // 
            // operationHistoryTable
            // 
            this.operationHistoryTable.AutoScroll = true;
            this.operationHistoryTable.ColumnCount = 5;
            this.operationHistoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.operationHistoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.operationHistoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.operationHistoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.operationHistoryTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.operationHistoryTable.Location = new System.Drawing.Point(12, 69);
            this.operationHistoryTable.Name = "operationHistoryTable";
            this.operationHistoryTable.RowCount = 1;
            this.operationHistoryTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.operationHistoryTable.Size = new System.Drawing.Size(591, 200);
            this.operationHistoryTable.TabIndex = 5;
            this.operationHistoryTable.Tag = "";
            // 
            // nDiginotesTextLabel
            // 
            this.nDiginotesTextLabel.AutoSize = true;
            this.nDiginotesTextLabel.Location = new System.Drawing.Point(12, 40);
            this.nDiginotesTextLabel.Name = "nDiginotesTextLabel";
            this.nDiginotesTextLabel.Size = new System.Drawing.Size(109, 13);
            this.nDiginotesTextLabel.TabIndex = 6;
            this.nDiginotesTextLabel.Text = "Número de Diginotes:";
            // 
            // nDiginotesLabel
            // 
            this.nDiginotesLabel.AutoSize = true;
            this.nDiginotesLabel.Location = new System.Drawing.Point(127, 40);
            this.nDiginotesLabel.Name = "nDiginotesLabel";
            this.nDiginotesLabel.Size = new System.Drawing.Size(13, 13);
            this.nDiginotesLabel.TabIndex = 7;
            this.nDiginotesLabel.Text = "0";
            this.nDiginotesLabel.Click += new System.EventHandler(this.nDiginotesLabel_Click);
            // 
            // nDiginotesSpinner
            // 
            this.nDiginotesSpinner.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nDiginotesSpinner.Location = new System.Drawing.Point(254, 278);
            this.nDiginotesSpinner.Margin = new System.Windows.Forms.Padding(2);
            this.nDiginotesSpinner.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nDiginotesSpinner.Name = "nDiginotesSpinner";
            this.nDiginotesSpinner.Size = new System.Drawing.Size(102, 20);
            this.nDiginotesSpinner.TabIndex = 9;
            // 
            // EditOrderButton
            // 
            this.EditOrderButton.Location = new System.Drawing.Point(528, 42);
            this.EditOrderButton.Name = "EditOrderButton";
            this.EditOrderButton.Size = new System.Drawing.Size(75, 23);
            this.EditOrderButton.TabIndex = 10;
            this.EditOrderButton.Text = "Edit Order";
            this.EditOrderButton.UseVisualStyleBackColor = true;
            this.EditOrderButton.Click += new System.EventHandler(this.EditOrderButton_Click);
            // 
            // messageTextLabel
            // 
            this.messageTextLabel.AutoSize = true;
            this.messageTextLabel.Location = new System.Drawing.Point(7, 304);
            this.messageTextLabel.Name = "messageTextLabel";
            this.messageTextLabel.Size = new System.Drawing.Size(58, 13);
            this.messageTextLabel.TabIndex = 11;
            this.messageTextLabel.Text = "Messages:";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.BackColor = System.Drawing.SystemColors.Control;
            this.messageLabel.ForeColor = System.Drawing.Color.Red;
            this.messageLabel.Location = new System.Drawing.Point(71, 304);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(0, 13);
            this.messageLabel.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 324);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.messageTextLabel);
            this.Controls.Add(this.EditOrderButton);
            this.Controls.Add(this.nDiginotesSpinner);
            this.Controls.Add(this.nDiginotesLabel);
            this.Controls.Add(this.nDiginotesTextLabel);
            this.Controls.Add(this.operationHistoryTable);
            this.Controls.Add(this.comprarButton);
            this.Controls.Add(this.venderButton);
            this.Controls.Add(this.cotacaoLabel);
            this.Controls.Add(this.cotacaoTextLabel);
            this.Controls.Add(this.logoutButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nDiginotesSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label cotacaoTextLabel;
        private System.Windows.Forms.Label cotacaoLabel;
        private System.Windows.Forms.Button venderButton;
        private System.Windows.Forms.Button comprarButton;
        private System.Windows.Forms.TableLayoutPanel operationHistoryTable;
        private System.Windows.Forms.Label nDiginotesTextLabel;
        private System.Windows.Forms.Label nDiginotesLabel;
        private System.Windows.Forms.NumericUpDown nDiginotesSpinner;
        private System.Windows.Forms.Button EditOrderButton;
        private System.Windows.Forms.Label messageTextLabel;
        private System.Windows.Forms.Label messageLabel;
    }
}