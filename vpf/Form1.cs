using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vpf
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bitmap;
        Board board;
        private static Form1 form = null;
        private delegate void EnableDelegate(Color color, int m, int n, int spaceX, int spaceY);
        private Boolean high = false;
        private Boolean mid = false;
        private Boolean low = false;
        private Boolean click = false;

        public Form1()
        {
            InitializeComponent();
            board = new Board(pictureBox1.Height, pictureBox1.Width);
            bitmap = new Bitmap(1000, 1000);
            g = Graphics.FromImage(bitmap);
            board.Draw(g);
            pictureBox1.Image = bitmap;
            form = this;
            
        }

        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            board.addColor(e.X, e.Y, g,e, high, mid, low);
            pictureBox1.Refresh();
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            board.path(g);
            pictureBox1.Image = bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.FromArgb(240, 240, 240));
            high = mid = low = click = false;
            board.Draw(g);
            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 215)
            {
                panel1.Height = 45;
            }else
            {
                panel1.Height = 215;
            }

            if (click)
            {
                board.pathd(g);
                pictureBox1.Image = bitmap;
            }
            click = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            low = true;
            high = false;
            mid = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mid = true;
            low = false;
            high = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            high = true;
            mid = false;
            low = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Height = 45;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Brushes.Red, 8);
            p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            
        }
    }
}
