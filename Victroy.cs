using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Victory : Form
    {
        private Timer timer1;
        public Victory(bool isWhite)
        {
            InitializeComponent();
            if(isWhite)
            {
                pictureBox1.Image = Properties.Resources.Win_White;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.Win_Black;
            }

            timer1 = new Timer();
            timer1.Interval = 3300;
            timer1.Tick += Timer_Tick;
            timer1.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox1.Image = GetLastFrame(pictureBox1.Image as Image);
            Task.Delay(1000).ContinueWith(t => Environment.Exit(0));
        }

        private Image GetLastFrame(Image gif)
        {
            var lastFrame = new Bitmap(gif);
            return lastFrame;
        }
    }
}
