namespace Client
{
    partial class EditOrder
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
            this.nDiginotesLabel = new System.Windows.Forms.Label();
            this.diginoteValueLabel = new System.Windows.Forms.Label();
            this.updateOrderButton = new System.Windows.Forms.Button();
            this.removeOrderButton = new System.Windows.Forms.Button();
            this.cancelOrderButton = new System.Windows.Forms.Button();
            this.spinnerNDiginotes = new System.Windows.Forms.NumericUpDown();
            this.spinnerCotacao = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerNDiginotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerCotacao)).BeginInit();
            this.SuspendLayout();
            // 
            // nDiginotesLabel
            // 
            this.nDiginotesLabel.AutoSize = true;
            this.nDiginotesLabel.Location = new System.Drawing.Point(13, 31);
            this.nDiginotesLabel.Name = "nDiginotesLabel";
            this.nDiginotesLabel.Size = new System.Drawing.Size(106, 13);
            this.nDiginotesLabel.TabIndex = 0;
            this.nDiginotesLabel.Text = "Number of Diginotes:";
            this.nDiginotesLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // diginoteValueLabel
            // 
            this.diginoteValueLabel.AutoSize = true;
            this.diginoteValueLabel.Location = new System.Drawing.Point(13, 57);
            this.diginoteValueLabel.Name = "diginoteValueLabel";
            this.diginoteValueLabel.Size = new System.Drawing.Size(76, 13);
            this.diginoteValueLabel.TabIndex = 1;
            this.diginoteValueLabel.Text = "DiginoteValue:";
            // 
            // updateOrderButton
            // 
            this.updateOrderButton.Location = new System.Drawing.Point(12, 108);
            this.updateOrderButton.Name = "updateOrderButton";
            this.updateOrderButton.Size = new System.Drawing.Size(75, 23);
            this.updateOrderButton.TabIndex = 2;
            this.updateOrderButton.Text = "Update";
            this.updateOrderButton.UseVisualStyleBackColor = true;
            this.updateOrderButton.Click += new System.EventHandler(this.updateOrderButton_Click);
            // 
            // removeOrderButton
            // 
            this.removeOrderButton.Location = new System.Drawing.Point(100, 108);
            this.removeOrderButton.Name = "removeOrderButton";
            this.removeOrderButton.Size = new System.Drawing.Size(75, 23);
            this.removeOrderButton.TabIndex = 3;
            this.removeOrderButton.Text = "Remove";
            this.removeOrderButton.UseVisualStyleBackColor = true;
            this.removeOrderButton.Click += new System.EventHandler(this.removeOrderButton_Click);
            // 
            // cancelOrderButton
            // 
            this.cancelOrderButton.Location = new System.Drawing.Point(188, 108);
            this.cancelOrderButton.Name = "cancelOrderButton";
            this.cancelOrderButton.Size = new System.Drawing.Size(75, 23);
            this.cancelOrderButton.TabIndex = 4;
            this.cancelOrderButton.Text = "Cancel";
            this.cancelOrderButton.UseVisualStyleBackColor = true;
            this.cancelOrderButton.Click += new System.EventHandler(this.cancelOrderButton_Click);
            // 
            // spinnerNDiginotes
            // 
            this.spinnerNDiginotes.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spinnerNDiginotes.Location = new System.Drawing.Point(143, 29);
            this.spinnerNDiginotes.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.spinnerNDiginotes.Name = "spinnerNDiginotes";
            this.spinnerNDiginotes.Size = new System.Drawing.Size(120, 20);
            this.spinnerNDiginotes.TabIndex = 5;
            // 
            // spinnerCotacao
            // 
            this.spinnerCotacao.DecimalPlaces = 2;
            this.spinnerCotacao.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinnerCotacao.Location = new System.Drawing.Point(143, 55);
            this.spinnerCotacao.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            65536});
            this.spinnerCotacao.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinnerCotacao.Name = "spinnerCotacao";
            this.spinnerCotacao.Size = new System.Drawing.Size(120, 20);
            this.spinnerCotacao.TabIndex = 6;
            this.spinnerCotacao.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // EditOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 143);
            this.Controls.Add(this.spinnerCotacao);
            this.Controls.Add(this.spinnerNDiginotes);
            this.Controls.Add(this.cancelOrderButton);
            this.Controls.Add(this.removeOrderButton);
            this.Controls.Add(this.updateOrderButton);
            this.Controls.Add(this.diginoteValueLabel);
            this.Controls.Add(this.nDiginotesLabel);
            this.Name = "EditOrder";
            this.Text = "Edit Order";
            ((System.ComponentModel.ISupportInitialize)(this.spinnerNDiginotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerCotacao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nDiginotesLabel;
        private System.Windows.Forms.Label diginoteValueLabel;
        private System.Windows.Forms.Button updateOrderButton;
        private System.Windows.Forms.Button removeOrderButton;
        private System.Windows.Forms.Button cancelOrderButton;
        private System.Windows.Forms.NumericUpDown spinnerNDiginotes;
        private System.Windows.Forms.NumericUpDown spinnerCotacao;
    }
}