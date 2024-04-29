namespace Chess
{
    partial class OpenAiKeyMessageBox
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
            this.bigTextBox1 = new ReaLTaiizor.Controls.BigTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.airButton1 = new ReaLTaiizor.Controls.AirButton();
            this.airButton2 = new ReaLTaiizor.Controls.AirButton();
            this.SuspendLayout();
            // 
            // bigTextBox1
            // 
            this.bigTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.bigTextBox1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.bigTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.bigTextBox1.Image = null;
            this.bigTextBox1.Location = new System.Drawing.Point(6, 28);
            this.bigTextBox1.MaxLength = 32767;
            this.bigTextBox1.Multiline = false;
            this.bigTextBox1.Name = "bigTextBox1";
            this.bigTextBox1.ReadOnly = false;
            this.bigTextBox1.Size = new System.Drawing.Size(209, 41);
            this.bigTextBox1.TabIndex = 0;
            this.bigTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.bigTextBox1.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Insert OpenAiKey";
            // 
            // airButton1
            // 
            this.airButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.airButton1.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.airButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.airButton1.Image = null;
            this.airButton1.Location = new System.Drawing.Point(221, 28);
            this.airButton1.Name = "airButton1";
            this.airButton1.NoRounding = false;
            this.airButton1.Size = new System.Drawing.Size(61, 41);
            this.airButton1.TabIndex = 2;
            this.airButton1.Text = "Enter";
            this.airButton1.Transparent = false;
            this.airButton1.Click += new System.EventHandler(this.airButton1_Click);
            // 
            // airButton2
            // 
            this.airButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.airButton2.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.airButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.airButton2.Image = null;
            this.airButton2.Location = new System.Drawing.Point(286, 28);
            this.airButton2.Name = "airButton2";
            this.airButton2.NoRounding = false;
            this.airButton2.Size = new System.Drawing.Size(61, 41);
            this.airButton2.TabIndex = 3;
            this.airButton2.Text = "Refuse";
            this.airButton2.Transparent = false;
            this.airButton2.Click += new System.EventHandler(this.airButton2_Click);
            // 
            // OpenAiKeyMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 73);
            this.Controls.Add(this.airButton2);
            this.Controls.Add(this.airButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bigTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "OpenAiKeyMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenAiKeyMessageBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.BigTextBox bigTextBox1;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.AirButton airButton1;
        private ReaLTaiizor.Controls.AirButton airButton2;
    }
}