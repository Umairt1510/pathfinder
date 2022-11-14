using System;
using System.Drawing;
using System.Windows.Forms;

namespace vpf
{

    internal class Board
    {
        Pen mypen = new Pen(Brushes.Black, 1);

        int lines = 20;
        float x = 0;
        float y = 0;
        int width;
        int height;
        float spaceX;
        float spaceY;
        int[,] amn;

        bool start = false;
        int[] st;
        int[] en;
        bool end = false;

        public Board(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.spaceX = (height / lines);
            this.spaceY = (width / lines);
        }

        public void Draw(Graphics g)
        {
            x = 0;
            y = 0;
            start = false;
            end = false;
            amn = new int[20, 20];
            st = new int[1];
            en = new int[1];
            for (int i = 0; i < lines + 2 ; i++)
            {
                g.DrawLine(mypen, x, y, x, (spaceY * lines));
                x += spaceX;
            }
            x = 0;
            y = 0;
            for (int i = 0; i < lines + 1; i++)
            {
                g.DrawLine(mypen, x, y, (spaceX * lines), y);
                y += spaceY;
            }
        }

        public void addColor(int x, int y, Graphics g, MouseEventArgs e, Boolean high, Boolean mid, Boolean low)
        {
            int m = (int) (x / spaceX);
            int n = (int) (y / spaceY);


            switch (e.Button)
            {

                case MouseButtons.Left:
                    if(!start)
                    {
                        g.FillRectangle(new SolidBrush(Color.CadetBlue), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 1;
                        st = new int[] {n , m};
                        start = true;
                    }
                    else if (high)
                    {
                        g.FillRectangle(new SolidBrush(Color.LightSalmon), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 3;
                    }
                    else if (mid)
                    {
                        g.FillRectangle(new SolidBrush(Color.LightGoldenrodYellow), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 4;
                    }
                    else if (low)
                    {
                        g.FillRectangle(new SolidBrush(Color.LightGreen), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 5;
                    }
                    break;

                case MouseButtons.Right:
                    if(!end)
                    {
                        g.FillRectangle(new SolidBrush(Color.Orange), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 2;
                        en = new int[] {n, m};
                        end = true;
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), (m * spaceX) + 1, (n * spaceY) + 1, spaceX - 1, spaceY - 1);
                        amn[n, m] = 6;
                    }
                    
                    break;

            }
            
        }

        

        public void path(Graphics g)
        {
            
            aAlgorithm a = new aAlgorithm(amn, st, en);
            //Console.WriteLine(a.algor(g, spaceX, spaceY));
            a.algor(g, spaceX, spaceY);



        }

        public void pathd(Graphics g)
        {

            Dijkstra a = new Dijkstra(amn, st, en);
            //Console.WriteLine(a.algorithm(g, spaceX, spaceY));
            a.algorithm(g, spaceX, spaceY);
        }

    }
}
