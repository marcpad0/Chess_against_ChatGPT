namespace Chess
{
    partial class MenuStart
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
            this.btnStartP1vsP2 = new ReaLTaiizor.Controls.Button();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.controlBox1 = new ReaLTaiizor.Controls.ControlBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.btnStartP1vsGPT = new ReaLTaiizor.Controls.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartP1vsP2
            // 
            this.btnStartP1vsP2.BackColor = System.Drawing.Color.Transparent;
            this.btnStartP1vsP2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsP2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartP1vsP2.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartP1vsP2.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnStartP1vsP2.Image = null;
            this.btnStartP1vsP2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartP1vsP2.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsP2.Location = new System.Drawing.Point(235, 296);
            this.btnStartP1vsP2.Name = "btnStartP1vsP2";
            this.btnStartP1vsP2.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartP1vsP2.PressedColor = System.Drawing.Color.Green;
            this.btnStartP1vsP2.Size = new System.Drawing.Size(120, 40);
            this.btnStartP1vsP2.TabIndex = 0;
            this.btnStartP1vsP2.Text = "P1 VS P2";
            this.btnStartP1vsP2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnStartP1vsP2.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.bigLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bigLabel1.Location = new System.Drawing.Point(239, 111);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(106, 46);
            this.bigLabel1.TabIndex = 1;
            this.bigLabel1.Text = "Chess";
            // 
            // controlBox1
            // 
            this.controlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.controlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.controlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.controlBox1.DefaultLocation = true;
            this.controlBox1.EnableHoverHighlight = true;
            this.controlBox1.EnableMaximizeButton = true;
            this.controlBox1.EnableMinimizeButton = true;
            this.controlBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.controlBox1.Location = new System.Drawing.Point(507, 18);
            this.controlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.controlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.controlBox1.Name = "controlBox1";
            this.controlBox1.Size = new System.Drawing.Size(90, 25);
            this.controlBox1.TabIndex = 3;
            this.controlBox1.Text = "controlBox1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Chess.Properties.Resources.giphy;
            this.pictureBox2.Location = new System.Drawing.Point(-4, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(596, 394);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pbSettings
            // 
            this.pbSettings.Image = global::Chess.Properties.Resources.istockphoto_1163622474_612x612;
            this.pbSettings.Location = new System.Drawing.Point(13, 18);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(36, 36);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSettings.TabIndex = 6;
            this.pbSettings.TabStop = false;
            this.pbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            // 
            // btnStartP1vsGPT
            // 
            this.btnStartP1vsGPT.BackColor = System.Drawing.Color.Transparent;
            this.btnStartP1vsGPT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsGPT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartP1vsGPT.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartP1vsGPT.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsGPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnStartP1vsGPT.Image = null;
            this.btnStartP1vsGPT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartP1vsGPT.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnStartP1vsGPT.Location = new System.Drawing.Point(235, 342);
            this.btnStartP1vsGPT.Name = "btnStartP1vsGPT";
            this.btnStartP1vsGPT.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartP1vsGPT.PressedColor = System.Drawing.Color.Green;
            this.btnStartP1vsGPT.Size = new System.Drawing.Size(120, 40);
            this.btnStartP1vsGPT.TabIndex = 7;
            this.btnStartP1vsGPT.Text = "P1 VS GPT";
            this.btnStartP1vsGPT.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnStartP1vsGPT.Click += new System.EventHandler(this.btnStartP1vsGPT_Click);
            // 
            // MenuStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(591, 393);
            this.Controls.Add(this.btnStartP1vsGPT);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.controlBox1);
            this.Controls.Add(this.bigLabel1);
            this.Controls.Add(this.btnStartP1vsP2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.TransparencyKey = System.Drawing.Color.DodgerBlue;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.Button btnStartP1vsP2;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.ControlBox controlBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pbSettings;
        private ReaLTaiizor.Controls.Button btnStartP1vsGPT;
    }
}