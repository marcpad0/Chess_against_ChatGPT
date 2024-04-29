using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Menu : Form
    {
        private string SelectedPiece = default(string);

        public Menu(bool isWhiteTurn)
        {
            InitializeComponent();
            if(isWhiteTurn)
            {
                pictureBox1.ImageLocation = "Piece/King_white.png";
                pictureBox1.Tag = "Queen_white";
                pictureBox2.ImageLocation = "Piece/Bishop_white.png";
                pictureBox2.Tag = "Bishop_white";
                pictureBox3.ImageLocation = "Piece/Horse_white.png";
                pictureBox3.Tag = "Horse_white";
                pictureBox4.ImageLocation = "Piece/Tower_white.png";
                pictureBox4.Tag = "Horse_white";
                pictureBox1.Click += Picture_Click;
                pictureBox2.Click += Picture_Click;
                pictureBox3.Click += Picture_Click;
                pictureBox4.Click += Picture_Click;
            }
            else
            {
                pictureBox1.ImageLocation = "Piece/King_black.png";
                pictureBox1.Tag = "Queen_black";
                pictureBox2.ImageLocation = "Piece/Bishop_black.png";
                pictureBox2.Tag = "Bishop_black";
                pictureBox3.ImageLocation = "Piece/Horse_black.png";
                pictureBox3.Tag = "Horse_black";
                pictureBox4.ImageLocation = "Piece/Tower_black.png";
                pictureBox4.Tag = "Tower_black";
                pictureBox1.Click += Picture_Click;
                pictureBox2.Click += Picture_Click;
                pictureBox3.Click += Picture_Click;
                pictureBox4.Click += Picture_Click;
            }
        }

        public void Picture_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            string[] tagParts = pictureBox.Tag.ToString().Split('_');
            string pieceNameCheck = tagParts[0];
            SelectedPiece = pieceNameCheck;
        }

        public string Results()
        {
            return SelectedPiece;
        }
    }
}
