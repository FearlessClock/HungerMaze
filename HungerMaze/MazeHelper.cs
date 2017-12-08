using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class MazeHelper
    {
        /// <summary>
        /// Generate the 4 cardinal directions 
        /// </summary>
        /// <returns></returns>
        public static Vector[] GetNESWDirectionArray()
        {
            return new Vector[] { new Vector(1, 0), new Vector(-1, 0), new Vector(0, 1), new Vector(0, -1) };
        }
    }
}
