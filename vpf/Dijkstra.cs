using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace vpf
{
    internal class Dijkstra
    {

        int[,] grid;
        int[] end;
        int[] start;
        List<Node> visit = new List<Node>();
        List<Node> visited = new List<Node>();

        public Dijkstra(int[,] grid, int[] start, int[] end)
        {
            this.grid = grid;
            this.start = start;
            this.end = end;
        }

        public bool algorithm(Graphics g, float spaceX, float spaceY)
        {
            Node current = new Node(start, null);
            current.setDistance(0);
            visit.Add(current);
            while (visit.Count > 0)
            {
                current = min_distance(current);
                visit.Remove(current);
                
                List<Node> temp = neighbor(current);
                
                visited.Add(current);

                if (current.Position[0] == end[0] && current.Position[1] == end[1])
                {
                    visited.Add(new Node(end, current.Position));
                    drawpath(current, g, spaceX, spaceY);
                    return true;
                }
                
                foreach (Node t in temp)
                {

                    //If 2 nodes are at the same position by have different distances, select the on with the smaller distance and remove the larger
                    if (visit.Any(x => x.Position == t.Position && x.getDistance() > t.getDistance()))
                    {
                        visit.Remove(visit.First(x => x.Position == t.Position && x.getDistance() > t.getDistance()));
                        visit.Add(t);
                    }
                    else if (!visited.Any(x => Enumerable.SequenceEqual(t.Position, x.Position)) && !visit.Any(y => Enumerable.SequenceEqual(t.Position, y.Position)))
                    {
                        visit.Add(t);
                    }
                }

            }
            return false;
        }

        public void drawpath(Node current, Graphics g, float spaceX, float spaceY)
        {

            List<Node> temp = new List<Node>();
            while (current.Parent != null)
            {
                foreach (Node t in visited)
                {
                    if (current.Parent == null)
                    {
                        continue;
                    }
                    if (t.Position == null ||  Enumerable.SequenceEqual(current.Parent, t.Position))
                    {
                        current = t;
                        temp.Add(current);
                    }

                }
            }
            //temp.Reverse();
            foreach (Node a in temp)
            {
                if (a.Parent == null)
                    continue;
                g.FillRectangle(new SolidBrush(Color.LightPink), (a.Position[1] * spaceX) + 1 , (a.Position[0] * spaceY) + 1, spaceX-1 , spaceY - 1);

            }
        }


        public Node min_distance(Node current)
        {
            List<Node> SortedList = visit.OrderBy(o => o.getDistance()).ToList();
            return SortedList[0];

        }

        public List<Node> neighbor(Node current)
        {
            List<Node> list = new List<Node>();
            Node n = null;
            int x = current.Position[0];
            int y = current.Position[1];
            
            //UP
            if (valid((x - 1), y))
            {
                n = new Node(new int[] { x - 1, y }, new int[] { x, y });
                n.setDistance(current.getDistance() + weight(x, y));
                list.Add(n);
            }

            //DOWN
            if (valid((x + 1), y))
            {
                n = new Node(new int[] { x + 1, y }, new int[] { x, y });
                n.setDistance(current.getDistance() + weight(x, y));
                list.Add(n);
            }

            //RIGHT
            if (valid(x, (y + 1)))
            {
                n = new Node(new int[] { x, y + 1 }, new int[] { x, y });
                n.setDistance(current.getDistance() + weight(x, y));
                list.Add(n);
            }

            //LEFT
            if (valid(x, (y - 1)))
            {
                n = new Node(new int[] { x, y - 1 }, new int[] { x, y });
                n.setDistance(current.getDistance() + weight(x, y));
                list.Add(n);
            }         

            return list;
        }

        public bool valid(int x, int y)
        {
            if ((x >= 0 && x <= 19) && (y >= 0 && y <= 19))
            {
                if (grid[x, y] == 0 || (grid[x, y] >= 2 && grid[x, y] <= 5))
                {
                    return true;
                }
            }

            return false;
        }

        public int weight(int x, int y)
        {
            if ((x >= 0 && x <= 19) && (y >= 0 && y <= 19))
            {
                //Heavy
                if (grid[x, y] == 3)
                {
                    return 15;
                }
                //medium
                if (grid[x, y] == 4)
                {
                    return 10;
                }
                //light
                if (grid[x, y] == 5)
                {
                    return 6;
                }
            }
            return 1;
        }

    }
}
