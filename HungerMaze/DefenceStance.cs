using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    //Run away from the enemy
    class DefenceStance : IStance
    {
        Random rand = new Random();
        public void React(Fighter fighter, Item[] items, Cell[] cells, Fighter[] fighters, bool[,] visitedPosition, Stack<Cell> path)
        {
            //TEST
            List<Cell> unVisitedNeighborCells = new List<Cell>();
            for (int i = 0; i < cells.Length; i++)
            {
                if (!visitedPosition[cells[i].Position.x, cells[i].Position.y])
                {
                    unVisitedNeighborCells.Add(cells[i]);
                }
            }
            if (unVisitedNeighborCells.Count > 0)
            {
                int selectedIndex = rand.Next(0, unVisitedNeighborCells.Count);
                Cell c = unVisitedNeighborCells[selectedIndex];
                fighter.GetCell.CurrentFighter = null;
                c.CurrentFighter = fighter;
                path.Push(fighter.GetCell);
                fighter.Move(c);
            }
            else
            {
                Cell step = path.Pop();
                fighter.Move(step);
            }
            //Go to the exit, otherwise item, otherwise random non fighter cell
        }
    }
}
