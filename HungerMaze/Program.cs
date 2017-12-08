using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze maze = MazeGenerator.GenerateNonPerfectPrimMaze(new Vector(20, 20), 0.9f);
            MazeVisualiser.ShowMaze(maze);


            Console.ReadKey();
        }
    }
}
