using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random
{
    class Node
    {

        private int f; //Tong khoang cach f = g + h
        private int g; //Quang duong da di duoc
        private int h; //Quand duong du kien con lai-Ta su dung khoang cach mahattan
        private int prev;
        //0-up
        //1-left
        //2-down
        //3-right
        //Mang chua nhung canh ke
        private int[] adjacency = new int[4];
        public Node()
        {

        }
        public int getF()
        {
            return f;
        }
        public void setF(int f)
        {
            this.f = f;
        }
        public int getG()
        {
            return g;
        }

        public void setG(int g)
        {
            this.g = g;
        }
        public int getH()
        {
            return h;
        }

        public void setH(int h)
        {
            this.h = h;
        }

        public int getPrev()
        {
            return prev;
        }

        public void setPrev(int prev)
        {
            this.prev = prev;
        }

        public int[] getAdjacency()
        {
            return adjacency;
        }

        public void setAdjacency(int pos, int value)
        {
            adjacency[pos] = value;
        }
    }
}
