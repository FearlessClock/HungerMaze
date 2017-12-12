using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class MazeGenerator
    {
        /// <summary>
        /// Generate Text file maze with "maze.txt" file
        /// </summary>
        /// <returns></returns>
        static public Maze GenerateTextFileMaze()
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader("maze.txt");
            }
            catch
            {
                Console.WriteLine("Couldn't (Probably) find the file");
            }

            string[] lengthOfMaze = sr.ReadLine().Split(',');
            int w = Convert.ToInt32(lengthOfMaze[0]);
            int h = Convert.ToInt32(lengthOfMaze[1]);
            string[] line = sr.ReadLine().Split(',');
            Vector start = new Vector(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
            line = sr.ReadLine().Split(',');
            Vector end = new Vector(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));

            Maze maze = new Maze(w, h, start, end);
            string row = sr.ReadLine();
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    bool wall = true;
                    if (row[j] == '1')
                    {
                        wall = false;
                    }
                    Cell cell = new Cell(wall, j, i);
                    maze.layout[j, i] = cell;
                }
                row = sr.ReadLine();
            }
            return maze;
        }

        public static Maze GeneratePrimMaze(Vector size, int nmbrOfThreads)
        {
            Maze maze = new Maze(size, true);
            maze = PrimsAlgorithm.GeneratePrimMaze(maze, nmbrOfThreads);
            return maze;
        }

        /// <summary>
        /// Generate a prims maze then remove a percentage of the walls
        /// </summary>
        /// <param name="size"></param>
        /// <param name="percent">Between 0 and 1</param>
        /// <returns></returns>
        public static Maze GenerateNonPerfectPrimMaze(Vector size, float thresh)
        {
            Random rand = new Random();
            Maze maze = new Maze(size, true);
            maze = PrimsAlgorithm.GeneratePrimMaze(maze, 8);
            for (int i = 0; i < maze.Height/5; i++)
            {
                Cell c = maze.GetRandomWallCell();
                if(rand.NextDouble() < thresh)
                {
                    c.Destroy();
                }
            }
            return maze;
        }
    }
}
