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
            //Create the maze, generate the items and start the hivemind class
            Maze maze = MazeGenerator.GenerateNonPerfectPrimMaze(new Vector(15, 10), 0.5f);
            ItemFactory itemFactory = new ItemFactory();
            Random rand = new Random();
            bool gameover = false;

            for (int i = 0; i < 0.1f*maze.Width*maze.Height; i++)
            {
                Cell randomCell = maze.GetRandomUnoccupiedCell();
                randomCell.Item = itemFactory.GetItem((eItemType)rand.Next(0, 3), randomCell.Position);
            }

            int nmbrOfFighters = (int)(0.01f * maze.Height * maze.Width);
            HiveMind hiveMind = new HiveMind(maze, nmbrOfFighters);
            MazeVisualiser.ShowMaze(maze);
            MazeVisualiser.ShowFighters(hiveMind);

            
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
