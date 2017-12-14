using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungerMaze
{
    /// <summary>
    /// Class to draw the maze or path from anywhere in the script, easily
    /// </summary>
    /// 

    ///NOTE on the locks. The locks are used to make sure that the 
    ///console never writes at the same time which could cause some funny effects
    class MazeVisualiser
    {

        static ConsoleColor itemColor = ConsoleColor.Magenta;
        static ConsoleColor wallColor = ConsoleColor.White;
        static ConsoleColor EmptyColor = ConsoleColor.White;

        static object writelocker = new object();
        /// <summary>
        /// Show the maze in the console with preset colors
        /// </summary>
        /// <param name="maze"></param>
        static public void ShowMaze(Maze maze)
        {
            lock(writelocker)
            {
                //Console.Clear();
                for (int i = 0; i < maze.layout.GetLength(1); i++)
                {
                    for (int j = 0; j < maze.layout.GetLength(0); j++)
                    {
                        if (maze.layout[j, i].IsBlocked)
                        {
                            //Console.ForegroundColor = maze.layout[j, i].color;
                            Console.ForegroundColor = wallColor;
                            Console.Write("#");
                        }
                        else
                        {
                            if (maze.layout[j, i].Item != null)
                            {
                                //Console.ForegroundColor = maze.layout[j, i].color;
                                Console.ForegroundColor = itemColor;
                                Console.Write("I");
                            }
                            else
                            {
                                Console.ForegroundColor = maze.layout[j, i].color;
                                Console.ForegroundColor = EmptyColor;
                                Console.Write(" ");
                            }
                        }
                    }
                    int left = Console.CursorLeft;
                    int top = Console.CursorTop;
                    
                    Console.SetCursorPosition(maze.End.x, maze.End.y);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("E");

                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        //Clear the fighters from the screen
        public static void ClearFighters(HiveMind hiveMind)
        {
            lock (writelocker)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                foreach (IFighter fighter in hiveMind.Fighters)
                {
                    Console.SetCursorPosition(fighter.GetPosition.x, fighter.GetPosition.y);
                    Console.ForegroundColor = fighter.Color;// ConsoleColor.Red;
                    Console.Write(" ");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left, top);
            }
        }

        //Show the fighters on the screen
        public static void ShowFighters(HiveMind hiveMind)
        {
            lock (writelocker)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;

                foreach (IFighter fighter in hiveMind.Fighters)
                {
                    Console.SetCursorPosition(fighter.GetPosition.x, fighter.GetPosition.y);
                    Console.ForegroundColor = fighter.Color;// ConsoleColor.Red;
                    Console.Write("F");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(left, top);
            }
        }
        /// <summary>
        /// Show the start position of the path
        /// </summary>
        /// <param name="pos"></param>
        public static void ShowStart(Vector pos)
        {
            lock (writelocker)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pos.x, pos.y);
                //Console.Write((char)rand.Next(32, 126));
                Console.Write("S");
                Console.BackgroundColor = ConsoleColor.Black;
                //Thread.Sleep(60);
                Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(left, top);
            }
        }

        //Show the items on the screen
        public static void ShowItems(Maze maze)
        {
            lock (writelocker)
            {
                int top = Console.CursorTop;
                int left = Console.CursorLeft;
                for (int i = 0; i < maze.Width; i++)
                {
                    for (int j = 0; j < maze.Height; j++)
                    {
                        IItem itemTemp = maze.GetFromVecShallowCopy(new Vector(i, j)).Item;
                        if (itemTemp != null)
                        {
                            Console.ForegroundColor = itemTemp.ConsoleColor;
                            Console.SetCursorPosition(i, j);
                            Console.Write("I");
                        }
                    }
                }
                Console.SetCursorPosition(left, top);
            }
        }

        public static void PrintText(string text, int left, int top)
        {
            lock(writelocker)
            {
                int t = Console.CursorTop;
                int l = Console.CursorLeft;
                Console.SetCursorPosition(left, top);
                Console.Write(text);
                Console.SetCursorPosition(l, t);
            }
        }
    }
}
