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
            Maze maze = MazeGenerator.GenerateNonPerfectPrimMaze(new Vector(20, 20), 0.5f);
            IFactoryItem itemFactory = new IFactoryItem();

            // Get a random value in the item type enum
            Array values = Enum.GetValues(typeof(eItemType));
            Random random = new Random();
            eItemType randomType = (eItemType)values.GetValue(random.Next(values.Length));

            for (int i = 0; i < 5; i++)
            {
                Cell randomCell = maze.GetRandomUnoccupiedCell();
                randomCell.Item = itemFactory.GetItem(randomType, randomCell.Position, 2);
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
            Console.WriteLine("And the winner isssss!... I don't know... Yet!");
            Console.ReadKey();
        }
    }
}
