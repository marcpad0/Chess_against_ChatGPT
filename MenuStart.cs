using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class MenuStart : Form
    {
        private Timer timer1;

        public MenuStart()
        {
            Settings settings = new Settings();
            InitializeComponent();
            InitializeForm();
            timer1 = new Timer();
            timer1.Interval = 2300;
            timer1.Tick += Timer_Tick;
            timer1.Start();           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox2.Image = GetLastFrame(pictureBox2.Image as Image);
        }

        private Image GetLastFrame(Image gif)
        {
            var lastFrame = new Bitmap(gif);
            return lastFrame;
        }

        private void InitializeForm()
        {
            Resize += MenuStart_Resize;
            AdjustControlPositionsAndSizes();
            this.Controls.Add(this.pictureBox2);
            this.pictureBox2.Controls.Add(this.btnStartP1vsP2);
        }

        private void MenuStart_Resize(object sender, EventArgs e)
        {
            AdjustControlPositionsAndSizes();
        }

        private void AdjustControlPositionsAndSizes()
        {
            int formWidth = ClientSize.Width;
            int formHeight = ClientSize.Height;
            int labelX = (formWidth - bigLabel1.Width) / 2;
            bigLabel1.Location = new Point(labelX, formHeight / 4);
            pictureBox2.Width = this.Width;
            pictureBox2.Height = this.Height;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            btnStartP1vsP2.Width = 100;
            btnStartP1vsP2.Height = 50;
            btnStartP1vsP2.Location = new Point((pictureBox2.Width - btnStartP1vsP2.Width) / 2, (pictureBox2.Height - btnStartP1vsP2.Height) / 2);
            btnStartP1vsGPT.Width = 100;
            btnStartP1vsGPT.Height = 50;
            btnStartP1vsGPT.Location = new Point((pictureBox2.Width - btnStartP1vsGPT.Width) / 2, (pictureBox2.Height - btnStartP1vsGPT.Height) / 2 + 70);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 start = new Form1();
            start.FormClosed += (senderArg, eventArgs) =>
            {
                this.Close();
            };
            start.ShowDialog();
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void btnStartP1vsGPT_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 start = new Form1(true);
            start.FormClosed += (senderArg, eventArgs) =>
            {
                this.Close();
            };
            start.ShowDialog();
        }
    }
}