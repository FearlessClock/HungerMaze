using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HungerMaze
{
    //Generate a mazing using Prims algorithm
    class PrimsAlgorithm
    {
        /// <summary>
        /// Generate a maze with prims algorithm
        /// The maze generator starts at (1, 1) 
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        static object locker = new object();
        public static Maze GeneratePrimMaze(Maze maze, int nmbrOfThreads)
        {
            List<Cell> walls = new List<Cell>();
            List<Cell> inMaze = new List<Cell>();

            //Start the maze at 1, 1
            Cell cur = maze.GetFromVec(new Vector(1, 1));
            maze.Start = new Vector(1, 1);
            DigHole(cur, ref maze, ref inMaze);
            GetWallsForCell(cur, maze, ref walls, ref inMaze);

            Random rand = new Random();
            Thread[] threads = new Thread[nmbrOfThreads];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => PrimCutUp(ref walls, ref inMaze, ref rand, ref maze, ref cur));
                threads[i].Name = "Thread: " + i;
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            maze.End = cur.Position;
            maze.SetColorAt(cur.Position, ConsoleColor.Green);
            return maze;
        }

        private static void PrimCutUp(ref List<Cell> walls, ref List<Cell> inMaze, ref Random rand, ref Maze maze, ref Cell cur)
        {
            int counter = 0;
            while (walls.Count > 0)
            {
                //Get a random wall in the list of walls
                int index = -1;
                Cell next = null;
                lock (locker)
                {
                    if (walls.Count > 0)
                    {
                        index = rand.Next(0, walls.Count);
                        next = walls.ElementAt(index);

                        //Add the wall to the maze before removing it from the list of walls
                        inMaze.Add(next);
                        walls.RemoveAt(index);

                        //Mark this wall as checked
                        //maze.layout[walls[index].Position.x, walls[index].Position.y].color = ConsoleColor.Green;
                        if (IsSurrounded(next, maze))
                        {
                            counter++;
                            DigHole(next, ref maze, ref inMaze);
                            GetWallsForCell(next, maze, ref walls, ref inMaze);
                            cur = next;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if the cell is surrounded by walls
        /// </summary>
        /// <param name="c"></param>
        /// <param name="maze"></param>
        /// <returns></returns>
        private static bool IsSurrounded(Cell c, Maze maze)
        {
            List<Cell> walls = new List<Cell>();
            GetCellsAroundCell(c, maze, ref walls);
            foreach(Cell ar in walls)
            {
                if(ar != null && !ar.IsBlocked)
                {
                    return false;
                }
            }
            return true;
        }

        private static void DigHole(Cell c, ref Maze maze, ref List<Cell> inMaze)
        {
            maze.SetCellAs(c.Position, false);
            //maze.layout[c.Position.x, c.Position.y].color = ConsoleColor.Blue;
            inMaze.Add(c);
        }

        //Get the Cardinal direction cells around the Cell c
        private static void GetCellsAroundCell(Cell c, Maze maze, ref List<Cell> walls)
        {
            int width = maze.Width;
            int height = maze.Height;
            Vector[] neighs = { new Vector(1, 0), new Vector(-1, 0), new Vector(0, 1), new Vector(0, -1)};
            foreach (Vector vec in neighs)
            {
                Vector pos = c.Position + vec;
                if(pos.Equals(c.Position - Cell.GetVecFromDirection(c.dir)))
                {
                    continue;
                }
                if ((pos.x > 0 && pos.x < width) && (pos.y > 0 && pos.y < height))
                {
                    Cell newcell = maze.GetFromVec(pos);
                    walls.Add(newcell);
                }
            }
        }

        private static void GetWallsForCell(Cell c, Maze maze, ref List<Cell> walls, ref List<Cell> inMaze)
        {
            int width = maze.Width;
            int height = maze.Height;

            Vector[] neighs = MazeHelper.GetNESWDirectionArray();
            foreach (Vector vec in neighs)
            {
                Vector pos = c.Position + vec;
                if ((pos.x > 0 && pos.x < width-1) && (pos.y > 0 && pos.y < height-1))
                {
                    bool isAlreadyInWalls = false;
                    for (int i = 0; i < walls.Count; i++)
                    {
                        if (walls[i].Position.Equals(c.Position))
                        {
                            inMaze.Add(walls[i]);
                            walls.RemoveAt(i);
                            //maze.layout[walls[i].Position.x, walls[i].Position.y].color = ConsoleColor.Green;
                            isAlreadyInWalls = true;
                            break;
                        }
                    }
                    if (!isAlreadyInWalls && !inMaze.Contains(maze.GetFromVec(pos)) && maze.IsBlockedAt(pos))
                    {
                        Cell newcell = maze.GetFromVec(pos);
                        newcell.dir = Cell.GetDirectionFromVec(vec);
                        //maze.layout[pos.x, pos.y].color = ConsoleColor.Red;
                        walls.Add(newcell);
                    }
                }
            }
        }
    }
}
