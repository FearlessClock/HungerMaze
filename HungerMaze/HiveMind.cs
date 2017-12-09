using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class HiveMind
    {
        List<NormalFighter> fighters = new List<NormalFighter>();

        public HiveMind(Maze maze, int nmbrOfFighters)
        {
            Random rand = new Random();
            for (int i = 0; i < nmbrOfFighters; i++)
            {
                Cell cell = maze.GetRandomUnoccupiedCell();
                NormalFighter curFighter = null;

                if (rand.NextDouble() < 0)
                {
                    fighters.Add(new NormalFighter(new DefenceStance(), new Vector(maze.Width, maze.Height), cell, rand.Next(1, 10), rand.Next(0, 14), rand.Next(1, 14)));
                }
                else
                {
                    curFighter = new NormalFighter(new AttackStance(), new Vector(maze.Width, maze.Height), cell, rand.Next(1, 10), rand.Next(0, 14), rand.Next(1, 14));
                    fighters.Add(curFighter);
                }
                cell.CurrentFighter = curFighter;
            }
        }

        public IEnumerable<NormalFighter> Fighters { get { return fighters; } }

        public void Update(Maze maze, ref bool gameover)
        {
            MazeVisualiser.ClearFighters(this);
            foreach (NormalFighter fighter in fighters)
            {
                Cell[] cells = maze.GetSurroundingEmptyCells(fighter.GetPosition);
                List<NormalFighter> fighters = new List<NormalFighter>();
                List<Item> items = new List<Item>();

                foreach (Cell c in cells)
                {
                    if (c.CurrentFighter != null)
                    {
                        fighters.Add(c.CurrentFighter);
                    }
                    if (c.Item != null)
                    {
                        items.Add(c.Item);
                    }
                }

                fighter.React(items.ToArray<Item>(), cells, fighters.ToArray<NormalFighter>());
                gameover = fighter.CheckForEnd();
                if(fighter.IsDead())
                {
                    fighters.Remove(fighter);
                }
                Console.WriteLine(fighter.Life);
            }
            MazeVisualiser.ShowFighters(this);
        }
    }
}
