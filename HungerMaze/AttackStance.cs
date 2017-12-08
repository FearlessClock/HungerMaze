﻿using System;
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
        public void React(Fighter fighter, Item[] items, Cell[] cells, Fighter[] fighters, bool[,] visitedPositions, Stack<Cell> path)
        {
            //TEST
            List<Cell> unVisitedNeighborCells = new List<Cell>();
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].HasFighter())
                {
                    Console.WriteLine("");
                }
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
                c.CurrentFighter = fighter;
                path.Push(fighter.GetCell);
                fighter.Move(c);
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
                        path.Push(step);
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
            //ATTTAAAACCCCCCCKKKKK
        }
    }
}