using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    public interface IStance
    {
        void React(NormalFighter fighter, Item[] items, Cell[] cells, NormalFighter[] fighters, bool[,] visitedPositions, Stack<Cell> path);
    }
}
