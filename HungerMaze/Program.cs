using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungerMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze maze = MazeGenerator.GenerateNonPerfectPrimMaze(new Vector(10, 10), 0.5f);
            HiveMind hiveMind = new HiveMind(maze, 3);
            MazeVisualiser.ShowMaze(maze);
            MazeVisualiser.ShowFighters(hiveMind);

            while (true)
            {
                Thread.Sleep(1000);
                hiveMind.Update(maze);
                //ShowFighters(hiveMind);
            }
        }
    }
}
