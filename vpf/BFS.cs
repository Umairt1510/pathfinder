using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace vpf
{
    internal class aAlgorithm
    {
        int[,] grid;
        Queue<Node> visit = new Queue<Node>();
        List<Node> visited = new List<Node>();
        List<int[]> v = new List<int[]>();
        int[] end;
        int[] start;

        
        public aAlgorithm(int [,] grid, int[] start, int[] end)
        {
            this.grid = grid;
            this.start = start;
            this.end = end;

        }

        
        public bool algor(Graphics g, float spaceX, float spaceY)
        {
            
            visit.Enqueue(new Node(start,null));
            v.Add(start);
            while (visit.Count > 0)
            {
                Node current = visit.Dequeue();
                v.RemoveAt(0);
                visited.Add(current);
                List<int[]> temp = neighbor(current);

                if (current.Position[0] == end[0] && current.Position[1] == end[1])
                {
                    visited.Add(new Node(end,current.Position));
                    drawpath(current, g, spaceX, spaceY);
                    return true;
                }
                //g.FillRectangle(new SolidBrush(Color.LightBlue), current.Position[1] * spaceX, current.Position[0] * spaceY, spaceX, spaceY);
                foreach (int[] t in temp)
                {

                    if (!visited.Any(x => Enumerable.SequenceEqual(t, x.Position)) && !v.Any(y => Enumerable.SequenceEqual(t, y)))
                    {
                        visit.Enqueue(new Node(t, current.Position));
                        v.Add(t);
                    }
                            
                    
                }
            }
            
            
            return false;
        }
        public void drawpath(Node current, Graphics g, float spaceX, float spaceY)
        {
            List<int[]> temp = new List<int[]>();
            while (current.Parent != null)
            {
                
                foreach (Node t in visited)
                {
                    if (current.Parent == t.Position)
                    {
                        current = t;
                        if(current.Position != start)
                            temp.Add(current.Position);
                    }
                   
                }
                   
            }
            
            foreach(int[] t in temp)
            {
                
                g.FillRectangle(new SolidBrush(Color.LightPink), (t[1] * spaceX) + 1, (t[0] * spaceY) + 1, spaceX - 1, spaceY - 1);
            }
        }

        //Return a list of valid neighors
        public List<int[]> neighbor(Node current)
        {
            List<int[]> list = new List<int[]>();
            int x = current.Position[0];
            int y = current.Position[1];
            if (valid( (x -1), y ))
            {
                list.Add(new int[] { x -1, y });
            }
            if (valid((x + 1), y))
            {
                list.Add(new int[] { x + 1, y });
            }
            if (valid( x, (y - 1)))
            {
                list.Add(new int[] { x , y - 1 });
            }
            if (valid( x, (y + 1)))
            {
                list.Add(new int[] { x , y + 1 });
            }
            
            return list;
        }


        public bool valid(int x, int y)
        {
            if ( (x >= 0 && x <= 19) && (y >= 0 && y <= 19) )
            {
                if (grid[x, y] == 0 || grid[x, y] == 2)
                {
                    return true;

                }
            }

            return false;
        }
    }
}
