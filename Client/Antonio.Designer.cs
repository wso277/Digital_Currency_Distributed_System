namespace Client
{
    partial class Antonio
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.oldValueTextLabel = new System.Windows.Forms.Label();
            this.newValueTextLabel = new System.Windows.Forms.Label();
            this.nDiginotesTextLabel = new System.Windows.Forms.Label();
            this.oldValueLabel = new System.Windows.Forms.Label();
            this.newValueLabel = new System.Windows.Forms.Label();
            this.nDiginotesLabel = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "The system detected a change in the diginote value...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirm you want to keep your order or remove or update it.";
            // 
            // oldValueTextLabel
            // 
            this.oldValueTextLabel.AutoSize = true;
            this.oldValueTextLabel.Location = new System.Drawing.Point(12, 61);
            this.oldValueTextLabel.Name = "oldValueTextLabel";
            this.oldValueTextLabel.Size = new System.Drawing.Size(56, 13);
            this.oldValueTextLabel.TabIndex = 2;
            this.oldValueTextLabel.Text = "Old Value:";
            // 
            // newValueTextLabel
            // 
            this.newValueTextLabel.AutoSize = true;
            this.newValueTextLabel.Location = new System.Drawing.Point(12, 86);
            this.newValueTextLabel.Name = "newValueTextLabel";
            this.newValueTextLabel.Size = new System.Drawing.Size(62, 13);
            this.newValueTextLabel.TabIndex = 3;
            this.newValueTextLabel.Text = "New Value:";
            // 
            // nDiginotesTextLabel
            // 
            this.nDiginotesTextLabel.AutoSize = true;
            this.nDiginotesTextLabel.Location = new System.Drawing.Point(12, 110);
            this.nDiginotesTextLabel.Name = "nDiginotesTextLabel";
            this.nDiginotesTextLabel.Size = new System.Drawing.Size(106, 13);
            this.nDiginotesTextLabel.TabIndex = 4;
            this.nDiginotesTextLabel.Text = "Number of Diginotes:";
            // 
            // oldValueLabel
            // 
            this.oldValueLabel.AutoSize = true;
            this.oldValueLabel.Location = new System.Drawing.Point(144, 61);
            this.oldValueLabel.Name = "oldValueLabel";
            this.oldValueLabel.Size = new System.Drawing.Size(35, 13);
            this.oldValueLabel.TabIndex = 5;
            this.oldValueLabel.Text = "label6";
            this.oldValueLabel.Click += new System.EventHandler(this.label6_Click);
            // 
            // newValueLabel
            // 
            this.newValueLabel.AutoSize = true;
            this.newValueLabel.Location = new System.Drawing.Point(144, 86);
            this.newValueLabel.Name = "newValueLabel";
            this.newValueLabel.Size = new System.Drawing.Size(35, 13);
            this.newValueLabel.TabIndex = 6;
            this.newValueLabel.Text = "label7";
            // 
            // nDiginotesLabel
            // 
            this.nDiginotesLabel.AutoSize = true;
            this.nDiginotesLabel.Location = new System.Drawing.Point(144, 110);
            this.nDiginotesLabel.Name = "nDiginotesLabel";
            this.nDiginotesLabel.Size = new System.Drawing.Size(35, 13);
            this.nDiginotesLabel.TabIndex = 7;
            this.nDiginotesLabel.Text = "label8";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(174, 138);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 8;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(255, 138);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 9;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // Antonio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 173);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.nDiginotesLabel);
            this.Controls.Add(this.newValueLabel);
            this.Controls.Add(this.oldValueLabel);
            this.Controls.Add(this.nDiginotesTextLabel);
            this.Controls.Add(this.newValueTextLabel);
            this.Controls.Add(this.oldValueTextLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Antonio";
            this.Text = "Confirm Change in Value";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label oldValueTextLabel;
        private System.Windows.Forms.Label newValueTextLabel;
        private System.Windows.Forms.Label nDiginotesTextLabel;
        private System.Windows.Forms.Label oldValueLabel;
        private System.Windows.Forms.Label newValueLabel;
        private System.Windows.Forms.Label nDiginotesLabel;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button removeButton;
    }
}