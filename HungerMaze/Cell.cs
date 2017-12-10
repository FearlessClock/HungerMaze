using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    //Represent the cells of the maze in the grid
    public class Cell
    {
        Item item;
        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        NormalFighter fighter;

        public NormalFighter CurrentFighter
        {
            get { return fighter; }
            set { fighter = value; }
        }

        bool wall;
        public bool IsBlocked
        {
            get { return wall; }
            set { wall = value; }
        }

        bool end;
        public bool End
        {
            get { return end; }
            set { end = value; }
        }
        
        public enum Direction { N, S, E, W}
        public Direction dir;

        /// <summary>
        /// Get the direction (ei North, east, south, west) from a vector
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Direction GetDirectionFromVec(Vector a)
        {
            Vector[] neighs = { new Vector(1, 0), new Vector(-1, 0), new Vector(0, 1), new Vector(0, -1) };
            if (a.Equals(neighs[0]))
            {
                return Direction.E;
            }
            else if(a.Equals(neighs[1]))
            {
                return Direction.W;
            }
            else if (a.Equals(neighs[2]))
            {
                return Direction.N;
            }
            else if (a.Equals(neighs[3]))
            {
                return Direction.S;
            }
            return Direction.N;
        }

        /// <summary>
        /// Get the direction of a vector from the direction enum
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vector GetVecFromDirection(Direction d)
        {
            Vector vec = new Vector(0,0);
            switch (d)
            {
                case Direction.N:
                    vec = new Vector(0, 1);
                    break;
                case Direction.S:
                    vec = new Vector(0, -1);
                    break;
                case Direction.E:
                    vec = new Vector(1, 0);
                    break;
                case Direction.W:
                    vec = new Vector(-1, 0);
                    break;
            }
            return vec;
        }

        Vector pos;
        public Vector Position
        {
            get { return pos; }
            set { pos = value; }
        }

        //used mostly for Astar
        #region A start variables
        public Vector parentPos = null;

        bool visited;
        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public bool HasFighter()
        {
            return CurrentFighter != null ? true : false;
        }

        public ConsoleColor color = ConsoleColor.White; 

        //The movement cost to move from the starting point to a given square on the grid, following the path generated to get there from the start node.
        public float g;
        //The estimated distance to the end node using the Manhattan distance 
        public float h;

        //Parametre = to the sum of g and h
        public float f;
        #endregion

        public Cell()
        {
            dir = Direction.S;
            wall = false;
            g = 0;
            h = 0;
            f = g + h;
            pos = new Vector(0, 0);
            fighter = null;
            item = null;
        }

        public Cell(Vector p): this()
        {
            pos = p;
        }

        public Cell(bool w, int x, int y) : this(new Vector(x, y))
        {
            wall = w;
        }

        //Shallow clone a Cell
        public void Clone(Cell c)
        {
            this.pos = c.Position;
            this.IsBlocked = c.IsBlocked;
            this.g = c.g;
            this.h = c.h;
            this.f = c.f;
            this.parentPos = c.parentPos;
            this.color = c.color;
            this.visited = c.visited;
        }

        //Make a cell passable
        public void Destroy()
        {
            wall = false;
        }

        //Make a cell non passable
        public void Build()
        {
            wall = true;
        }

        /// <summary>
        /// Equal if on the same square
        /// </summary>
        /// <param name="obj">Cell to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Cell c = (Cell)obj;
            if(c.Position.x == this.pos.x && c.Position.y == this.pos.y )
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string res = "";
            if(CurrentFighter != null)
            {
                res = "{" + pos.x + " : " + pos.y + " FighterColor " + fighter.color + "}";
            }
            else
            {
                res = "{" + pos.x + " : " + pos.y + "}";
            }
            return res;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
