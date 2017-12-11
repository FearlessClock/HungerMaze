﻿using System;
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
                fighter.GetCell.CurrentFighter = null;
                if(c.Item.Count > 0)
                {
                    fighter.AddItem(c.Item.ToArray<IItem>());
                    c.Item = new List<IItem>();
                }
                c.CurrentFighter = fighter;
                path.Push(fighter.GetCell);
                fighter.Move(c);
            }
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
                        path.Push(step);
                    }
                }
            }
            //Go to the exit, otherwise item, otherwise random non fighter cell
        }
    }
}
