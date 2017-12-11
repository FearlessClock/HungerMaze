using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class HiveMind
    {
        List<IFighter> fighters = new List<IFighter>();

        string winnerName = "";
        FighterFactory fighterFactory;

        public HiveMind(Maze maze, int nmbrOfFighters)
        {
            fighterFactory = new FighterFactory(maze.GetSize);
            Random rand = new Random();
            for (int i = 0; i < nmbrOfFighters; i++)
            {
                Cell cell = maze.GetRandomUnoccupiedCell();
                IFighter curFighter = null;

                if (rand.NextDouble() < 0.5f)
                {
                    curFighter = fighterFactory.GetFighter(eFighterType.normalFighter, cell, rand.Next(1, 14));
                }
                else
                {
                    curFighter = fighterFactory.GetFighter(eFighterType.strongFighter, cell, rand.Next(1, 14));
                }
                fighters.Add(curFighter);
                cell.CurrentFighter = curFighter;
            }
        }

        public string GetWinnerName()
        {
            return winnerName;
        }

        public IEnumerable<IFighter> Fighters { get { return fighters; } }

        public void Update(Maze maze, ref bool gameover)
        {
            MazeVisualiser.ClearFighters(this);
            IFighter[] copyFighters = fighters.ToArray();
            foreach (IFighter fighter in copyFighters)
            {
                Cell[] cells = maze.GetSurroundingEmptyCells(fighter.GetPosition);
                List<IFighter> surroundingFighters = new List<IFighter>();
                List<IItem> items = new List<IItem>();

                foreach (Cell c in cells)
                {
                    if (c.CurrentFighter != null)
                    {
                        surroundingFighters.Add(c.CurrentFighter);
                    }
                    if (c.Item.Count > 0)
                    {
                        items.AddRange(c.Item);
                    }
                }

                fighter.React(items.ToArray<IItem>(), cells, surroundingFighters.ToArray<IFighter>());
                if (!gameover)
                {
                    gameover = fighter.CheckForEnd();
                    if(gameover)
                    {
                        winnerName = fighter.getName;
                    }
                }
                if (fighter.IsDead())
                {
                    fighter.GetCell.CurrentFighter = null;
                    fighters.Remove(fighter);
                }
                //Console.Write(fighter.Life + " ");
            }
            //Console.WriteLine("");
            MazeVisualiser.ShowFighters(this);
        }
    }
}
