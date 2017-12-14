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
        public void React(IFighter fighter, IItem[] items, Cell[] cells, IFighter[] fighters, bool[,] visitedPositions, Stack<Cell> path)
        {
            List<Cell> unVisitedNeighborCells = new List<Cell>();
            //Find all the open, unvisited cells
            for (int i = 0; i < cells.Length; i++)
            {
                if (!visitedPositions[cells[i].Position.x, cells[i].Position.y] && !cells[i].HasFighter())
                {
                    unVisitedNeighborCells.Add(cells[i]);
                }
            }
            //If the fighter is next to the exit, go to the exit and leave the function
            bool stepToEnd = false;
            foreach (Cell c in unVisitedNeighborCells)
            {
                if(c.End)
                {
                    stepToEnd = true;
                    MoveFighter(fighter, c, null, path);
                }
            }
            if (!stepToEnd)
            {
                //if there are unvisited, open cells, go there
                if (unVisitedNeighborCells.Count > 0)
                {
                    int selectedIndex = rand.Next(0, unVisitedNeighborCells.Count);
                    Cell c = unVisitedNeighborCells[selectedIndex];

                    MoveFighter(fighter, c, c.Item, path);
                }
                //Else go back on your path
                else
                {
                    if (path.Count > 0)
                    {
                        Cell step = path.Pop();
                        if (!step.HasFighter())
                        {
                            fighter.GetCell.CurrentFighter = null;
                            fighter.Move(step);
                        }
                        else
                        {
                            path = new Stack<Cell>();
                        }
                    }
                    //If all else fails, backtrack
                    else
                    {
                        for (int i = 0; i < visitedPositions.GetLength(0); i++)
                        {
                            for (int j = 0; j < visitedPositions.GetLength(1); j++)
                            {
                                visitedPositions[i, j] = false;
                            }
                        }
                    }
                }
            }
        }

        private void MoveFighter(IFighter fighter, Cell c, IItem item, Stack<Cell> path)
        {
            fighter.GetCell.CurrentFighter = null;
            c.CurrentFighter = fighter;
            if (item != null)
            {
                fighter.AddItem(item);
            }
            path.Push(fighter.GetCell);
            fighter.Move(c);
            if (item != null)
            {
                c.Item = null;
            }
        }
    }
}
