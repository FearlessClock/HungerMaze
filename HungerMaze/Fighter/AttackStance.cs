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
            if (fighter.CanFight() && fighters.Length > 0)
            {
                fighter.Attack(fighters[0]);
            }
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
                            /* Note:
                             * 1,2,3 The 3 fighters
                             * V a visited cell
                             *      #####
                                    #V#V#
                                    #123#
                                    ##V##
                                    #####
                             * The stack pop places the fighters on other fighters and the other cells are already visited
                             * The fighters can't move! So they are all stuck!
                             * Idea: Make the surrounding cells not visited
                             * Buuuuut this is a verrry edge case and so we are leaving it for now
                             */
                            path = new Stack<Cell>();
                        }
                    }
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
            //ATTTAAAACCCCCCCKKKKK
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
