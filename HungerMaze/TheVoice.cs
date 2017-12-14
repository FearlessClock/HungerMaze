using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungerMaze
{
    class TheVoice
    {
        public bool gameoverState;

        //All the sayings that the voice can say
        List<string> sayings = new List<string>();

        public TheVoice()
        {
            GetSayingsFromTxt("sayings.txt");
        }

        public void GetSayingsFromTxt(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string line = sr.ReadLine();
            while (line != null)
            {
                sayings.Add(line);
                line = sr.ReadLine();
            }
        }

        int time = 10000;
        //Watch the game and every set amount of time, remove the items from the players
        public void spyOnGame(Maze maze, List<IFighter> fighters, object mazeLocker)
        {
            Random rand = new Random();
            Thread.Sleep(time);
            while (!gameoverState)
            {
                string emptyness = "";
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    emptyness += " ";
                }
                MazeVisualiser.PrintText(emptyness, 0, Console.CursorTop);
                MazeVisualiser.PrintText("[Game Master] " + sayings[rand.Next(0, sayings.Count)], 0, Console.CursorTop);
                List<IItem> items = new List<IItem>();

                lock (mazeLocker)
                {
                    foreach (IFighter fighter in fighters)
                    {
                        foreach (IItem item in fighter.GetItems)
                        {
                            items.Add(item);
                        }
                        fighter.RemoveAllItems();
                    }


                    foreach (IItem item in items)
                    {
                        Cell randomCell = maze.GetRandomUnoccupiedCell();
                        item.Position = randomCell.Position;
                        item.ConsoleColor = ConsoleColor.Blue;
                        randomCell.Item = item;
                    }
                }
                MazeVisualiser.ShowItems(maze);
                Thread.Sleep(rand.Next(1, 3) * time);
            }
        }
    }
}
