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
    class MazeVisualiser
    {
        /// <summary>
        /// Show the maze in the console with preset colors
        /// </summary>
        /// <param name="maze"></param>
        static public void ShowMaze(Maze maze)
        {
            //Console.Clear();
            for (int i = 0; i < maze.layout.GetLength(1); i++)
            {
                for (int j = 0; j < maze.layout.GetLength(0); j++)
                {
                    if(maze.layout[j, i].IsBlocked)
                    {
                        Console.ForegroundColor = maze.layout[j, i].color;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("#");
                    }
                    else
                    {
                        Console.ForegroundColor = maze.layout[j, i].color;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                }
                int left = Console.CursorLeft;
                int top = Console.CursorTop;

                Console.SetCursorPosition(maze.Start.x, maze.Start.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("S");
                Console.SetCursorPosition(maze.End.x, maze.End.y);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("E");

                Console.SetCursorPosition(left, top);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Show a path sent as a Stack odered by last to first
        /// </summary>
        /// <param name="path"></param>
        /// <param name="col"></param>
        public static void ShowPath(Stack<Cell> path, ConsoleColor col)
        {
            int top = Console.CursorTop;
            int left = Console.CursorLeft;

            if (path != null)
            {
                Random rand = new Random();
                Cell cell = path.Pop();
                Console.SetCursorPosition(cell.Position.x, cell.Position.y);
                Console.BackgroundColor = col;
                Console.ForegroundColor = ConsoleColor.White;
                //Console.Write((char)rand.Next(32, 126));
                Console.Write("E");
                //Console.BackgroundColor = col;
                while (path.Count > 1)
                {
                    cell = path.Pop();
                    Console.SetCursorPosition(cell.Position.x, cell.Position.y);
                    //Console.Write((char)rand.Next(32, 126));
                    Console.Write("*");
                    //Thread.Sleep(60);
                }
                if (path.Count > 0)
                {
                    cell = path.Pop();
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(cell.Position.x, cell.Position.y);
                    //Console.Write((char)rand.Next(32, 126));
                    Console.Write("S");
                }
                //Console.BackgroundColor = col;
            }
            else
            {
                Console.WriteLine("Path null");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            //Thread.Sleep(60);
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(left, top); 
        }
        /// <summary>
        /// Show the start position of the path
        /// </summary>
        /// <param name="pos"></param>
        public static void ShowStart(Vector pos)
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
}
