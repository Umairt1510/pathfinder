using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vpf
{
    internal class Node
    {
        int[] position;
        int[] parent;
        int distance = 0;

        public Node(int[] position, int [] parent)
        {
            this.position = position;
            this.parent = parent;
        }
        public void setDistance(int d)
        {
            distance = d;
        }
        public int getDistance()
        {
            return distance;
        }
        public int[] Position 
        { 
            get { return position; }
            set { position = value; }
        }
        public int[] Parent 
        { 
            get { return parent; }
            set { parent = value; }
        }


    }
}
