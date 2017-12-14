using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    //Attack the enemy
    class AttackStance : IStance
    {
        Random rand = new Random();
        public void React(IFighter fighter, IItem[] items, Cell[] cells, IFighter[] fighters, bool[,] visitedPositions, Stack<Cell> path)
        {
            //If there is an enemy, attack it
            if (fighter.CanFight() && fighters.Length > 0)
            {
                fighter.Attack(fighters[0]);
            }
            //If there is an item, gete it
            else if(items.Length > 0)
            {
                Cell c = null;
                for (int i = 0; i < cells.Length; i++)
                {
                    if(cells[i].Position.Equals(items[0].Position))
                    {
                        c = cells[i];
                    }
                }
                if (c != null)
                {
                    MoveFighter(fighter, c, items[0], path);
                }
            }
            //Otherwise, find an open, unvisited cell and move to it.
            else
            {
                List<Cell> unVisitedNeighborCells = new List<Cell>();
                for (int i = 0; i < cells.Length; i++)
                {
                    bool var1 = !visitedPositions[cells[i].Position.x, cells[i].Position.y];
                    bool var2 = !cells[i].HasFighter();
                    if (var1 && var2)
                    {
                        unVisitedNeighborCells.Add(cells[i]);
                    }
                }
                if (unVisitedNeighborCells.Count > 0)
                {
                    int selectedIndex = rand.Next(0, unVisitedNeighborCells.Count);
                    Cell c = unVisitedNeighborCells[selectedIndex];
                    if(items.Length > 0)
                    {
                        MoveFighter(fighter, c, items[0], path);
                    }
                    else
                    {
                        MoveFighter(fighter, c, null, path);
                    }
                }
                //If no open, unvisited cells are found, go back on the path
                else
                {
                    //Double pop because the first pop is the current position
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
                    //If nothing can be done, reset everything
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
            if(item != null)
            {
                c.Item = null;
            }
        }
    }
}
