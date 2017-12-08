using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class HiveMind
    {
        List<Fighter> fighters = new List<Fighter>();

        public HiveMind(Maze maze, int nmbrOfFighters)
        {
            Random rand = new Random();
            for (int i = 0; i < nmbrOfFighters; i++)
            {
                Cell cell = maze.GetRandomUnoccupiedCell();
                Fighter curFighter = null;

                if (rand.NextDouble() < 0)
                {
                    fighters.Add(new Fighter(new DefenceStance(), new Vector(maze.Width, maze.Height), cell, rand.Next(1, 10), rand.Next(0, 14), rand.Next(1, 14)));
                }
                else
                {
                    curFighter = new Fighter(new AttackStance(), new Vector(maze.Width, maze.Height), cell, rand.Next(1, 10), rand.Next(0, 14), rand.Next(1, 14));
                    fighters.Add(curFighter);
                }
                cell.CurrentFighter = curFighter;
            }
        }

        public IEnumerable<Fighter> Fighters { get { return fighters; } }

        public void Update(Maze maze, ref bool gameover)
        {
            MazeVisualiser.ClearFighters(this);
            foreach (Fighter fighter in fighters)
            {
                Cell[] cells = maze.GetSurroundingEmptyCells(fighter.GetPosition);
                List<Fighter> fighters = new List<Fighter>();
                List<Item> items = new List<Item>();

                foreach (Cell c in cells)
                {
                    fighters.Add(c.CurrentFighter);
                    items.Add(c.Item);
                }

                fighter.React(items.ToArray<Item>(), cells, fighters.ToArray<Fighter>());
                gameover = fighter.CheckForEnd();
            }
            MazeVisualiser.ShowFighters(this);
        }
    }
}
