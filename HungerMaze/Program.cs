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
            ItemFactory itemFactory = new ItemFactory();
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                Cell randomCell = maze.GetRandomUnoccupiedCell();
                randomCell.Item.Add(itemFactory.GetItem((eItemType)rand.Next(0, 3), randomCell.Position));
            }

            HiveMind hiveMind = new HiveMind(maze, 3);
            MazeVisualiser.ShowMaze(maze);
            MazeVisualiser.ShowFighters(hiveMind);
            bool gameover = false;
            while (!gameover)
            {
                Thread.Sleep(1000);
                hiveMind.Update(maze, ref gameover);
            }
            Console.WriteLine("And the winner isssss! " + hiveMind.GetWinnerName());
            Console.ReadKey();
        }
    }
}
