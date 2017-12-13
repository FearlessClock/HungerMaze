using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungerMaze
{
    class HiveMind
    {
        List<IFighter> fighters = new List<IFighter>();

        string winnerName = "";
        FighterFactory fighterFactory;
        Object mazeLocker = new Object();
        TheVoice voice;
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

            voice = new TheVoice();
            Thread gameMaster = new Thread(() => voice.spyOnGame(maze, fighters, mazeLocker));
            gameMaster.Start();
        }
        


        public string GetWinnerName()
        {
            return winnerName;
        }

        public IEnumerable<IFighter> Fighters { get { return fighters; } }
        private bool gameoverState;
        public void Update(Maze maze, ref bool gameover)
        {
            gameoverState = gameover;
            MazeVisualiser.ClearFighters(this);
            IFighter[] copyFighters = fighters.ToArray();
            Thread[] threads = new Thread[copyFighters.Length];
            int counter = 0;
            foreach (IFighter fighter in copyFighters)
            {
                threads[counter] = new Thread(() => UpdateFighter(fighter, maze));
                threads[counter].Start();
                counter++;
                //Console.Write(fighter.Life + " ");
            }
            foreach(Thread thread in threads)
            {
                thread.Join();
            }
            //Console.WriteLine("");
            gameover = gameoverState;
            MazeVisualiser.ShowFighters(this);
        }

        private void UpdateFighter(IFighter fighter, Maze maze)
        {
            Cell[] cells = new Cell[0];
            lock (mazeLocker)
            {
                cells = maze.GetSurroundingEmptyCells(fighter.GetPosition);
            }
            List<IFighter> surroundingFighters = new List<IFighter>();
            List<IItem> items = new List<IItem>();

            foreach (Cell c in cells)
            {
                if (c.CurrentFighter != null)
                {
                    surroundingFighters.Add(c.CurrentFighter);
                }
                if (c.Item != null)
                {
                    items.Add(c.Item);
                }
            }
            lock (mazeLocker)
            {
                fighter.React(items.ToArray<IItem>(), cells, surroundingFighters.ToArray<IFighter>());
                if (!gameoverState)
                {
                    gameoverState = fighter.CheckForEnd();
                    if (gameoverState)
                    {
                        voice.gameoverState = true;
                        winnerName = fighter.getName;
                    }
                }
                if (fighter.IsDead())
                {
                    fighter.GetCell.CurrentFighter = null;
                    List<IItem> deadMansItems = fighter.GetItems;
                    foreach (IItem item in deadMansItems)
                    {
                        maze.GetRandomUnoccupiedCell().Item = item;
                    }
                    MazeVisualiser.ShowItems(maze);
                    fighters.Remove(fighter);
                }
            }
        }
    }
}
