using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    /// <summary>
    /// A class to store vectors in 2D
    /// </summary>
    public class Vector
    {
        public int x;
        public int y;

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Vector a = (Vector)obj;
            if(a.x == x && a.y == y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override string ToString()
        {
            return x + " " + y;
        }

        public static int GetDistanceBetween(Vector a, Vector b)
        {
            return Math.Abs((a.x - b.x) + (a.y - b.y));
        }
        
        /// <summary>
        /// Inclusive min max check
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public bool IsBetween(float minX, float minY, float maxX, float maxY)
        {
            if(this.x >= minX && this.x < maxX && this.y >= minY && this.y < maxY)
            {
                return true;
            }
            return false;
        }
    }
}
