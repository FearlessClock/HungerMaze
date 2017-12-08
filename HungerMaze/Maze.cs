using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class Maze
    {
        //The generated maze
        public Cell[,] layout;

        //The start and end points of the maze, the entrance and the exit
        private Vector start;
        public Vector Start
        {
            get { return start; }
            set
            {
                if (value.x < 0)
                {
                    value.x = 0;
                }
                else if (value.x >= layout.GetLength(0))
                {
                    value.x = layout.GetLength(0) - 1;
                }
                if (value.y < 0)
                {
                    value.y = 0;
                }
                else if (value.y >= layout.GetLength(0))
                {
                    value.y = layout.GetLength(0) - 1;
                }
                start = value;
            }
        }
        private Vector end;
        public Vector End
        {
            get { return end; }
            set
            {
                if (value.x < 0)
                {
                    value.x = 0;
                }
                else if (value.x >= layout.GetLength(0))
                {
                    value.x = layout.GetLength(0) - 1;
                }
                if (value.y < 0)
                {
                    value.y = 0;
                }
                else if (value.y >= layout.GetLength(0))
                {
                    value.y = layout.GetLength(0) - 1;
                }
                end = value;
            }
        }

        public Cell GetStartCell()
        {
            return GetFromVec(Start);
        }

        //Get the size of the maze
        public int Width
        {
            get { return layout.GetLength(0); }
        }

        public int Height
        {
            get { return layout.GetLength(1); }
        }

        Random rand;

        public Maze(Vector size)
        {
            rand = new Random();
            layout = new Cell[size.x, size.y];
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    layout[i, j] = new Cell(false, i, j);
                }
            }
        }

        public Maze(Vector size, bool state)
        {
            rand = new Random();
            layout = new Cell[size.x, size.y];
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    layout[i, j] = new Cell(state, i, j);
                }
            }
        }

        public Cell GetRandomWallCell()
        {
            Cell c = GetFromVec(new Vector(rand.Next(0, Width), rand.Next(0, Height)));
            while (!c.IsBlocked)
            {
                c = GetFromVec(new Vector(rand.Next(0, Width), rand.Next(0, Height)));
            }
            return c;
        }

        /// <summary>
        /// Gets a random empty cell from the map, if none exists, will continue for ever
        /// </summary>
        /// <returns></returns>
        public Cell GetRandomEmptyCell()
        {
            Cell c = GetFromVec(new Vector(rand.Next(0, Width), rand.Next(0, Height)));
            while (c.IsBlocked)
            {
                c = GetFromVec(new Vector(rand.Next(0, Width), rand.Next(0, Height)));
            }
            return c;
        }

        /// <summary>
        /// Marks all the cells as not visited, usefull after Astar call
        /// </summary>
        public void SetAllNotVisited()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    MarkAsNotVisited(new Cell(true, i, j));
                }
            }
        }

        public void MarkAsVisited(Cell next)
        {
            layout[next.Position.x, next.Position.y].Visited = true;
        }

        public void MarkAsNotVisited(Cell next)
        {
            layout[next.Position.x, next.Position.y].Visited = false;
        }

        public void SetColorAt(Vector position, ConsoleColor col)
        {
            layout[position.x, position.y].color = col;
        }
        /// <summary>
        /// Create the holder of the maze info
        /// </summary>
        /// <param name="h">Height of the maze</param>
        /// <param name="w">Width of the maze</param>
        /// <param name="s">Start of the maze</param>
        /// <param name="e">End of the maze</param>
        public Maze(int w, int h, Vector s, Vector e): this(new Vector(w, h))
        {
            Start = s;
            End = e;
            rand = new Random();
        }

        /// <summary>
        /// Create the holder of the maze info
        /// </summary>
        /// <param name="size">Width and Height of the maze</param>
        /// <param name="s">Start of the maze</param>
        /// <param name="e">End of the maze</param>
        public Maze(Vector size, Vector s, Vector e): this(new Vector(size.x, size.y))
        {
            Start = s;
            End = e;
            rand = new Random();
        }

        /// <summary>
        /// Fills the maze with a certain percentage
        /// </summary>
        /// <param name="percent"></param>
        public void FillMaze(float percent)
        {
            int total = (int)(Width * Height);
            int cellsToFill = (int)Math.Ceiling((total * percent));
            int tries = 100;
            while (cellsToFill > 0 && tries > 0)
            {
                int x = rand.Next(0, Width);
                int y = rand.Next(0, Height);
                Vector pos = new Vector(x, y);
                if (!IsBlockedAt(pos))
                {
                    SetCellAs(pos, true);
                    cellsToFill--;
                    tries = 100;
                }
                tries--;
            }
        }

        public bool IsBlockedAt(Vector pos)
        {
            if (layout[pos.x, pos.y] != null)
                return layout[pos.x, pos.y].IsBlocked;
            else
                return false;
        }

        public List<Cell> GetSurroundingWalls(Cell c)
        {
            List<Cell> dirs = new List<Cell>();

            Vector[] NESW = MazeHelper.GetNESWDirectionArray();

            Vector pos = c.Position;
            for (int i = 0; i < NESW.Length; i++)
            {
                Vector curr = pos + NESW[i];
                if (curr.IsBetween(0, 0, Width, Height))
                {
                    Cell step = GetFromVec(curr);
                    if (step.IsBlocked)
                    {
                        dirs.Add(step);
                    }
                }
            }

            return dirs;
        }

        public void DestroyAt(Vector position)
        {
            layout[position.x, position.y].Destroy();
        }

        public void SetCellAs(Vector pos, bool state)
        {
            layout[pos.x, pos.y].IsBlocked = state;
        }

        /// <summary>
        /// Set the state of the cell (Mustn't be used for cloning)
        /// </summary>
        /// <param name="c"></param>
        public void SetCellAs(Cell c)
        {
            layout[c.Position.x, c.Position.y].IsBlocked = c.IsBlocked;
        }

        public Cell GetFromVec(Vector pos)
        {
            if (pos.x < Width && pos.y < Height)
            {
                Cell c = new Cell();
                Cell old = layout[pos.x, pos.y];
                c.Clone(old);
                return c;
            }
            return null;
        }

        /// <summary>
        /// Make a shallow copy of the cell then set as cell
        /// </summary>
        /// <param name="c"></param>
        public void SetCellCloneAs(Cell c)
        {
            layout[c.Position.x, c.Position.y].Clone(c);
        }

        public List<Cell> GetSurroundingPaths(Cell c)
        {
            List<Cell> dirs = new List<Cell>();

            Vector[] NESW = MazeHelper.GetNESWDirectionArray();

            Vector pos = c.Position;
            for (int i = 0; i < NESW.Length; i++)
            {
                Vector curr = pos + NESW[i];
                if (curr.IsBetween(0, 0, Width, Height))
                {
                    Cell step = GetFromVec(curr);
                    if(!step.Visited && !step.IsBlocked)
                    {
                        dirs.Add(step);
                    }
                }
            }

            return dirs;
        }

        public List<Cell> GetSurroundingPaths(Cell c, List<Cell> visited)
        {
            List<Cell> dirs = new List<Cell>();

            Vector[] NESW = MazeHelper.GetNESWDirectionArray();

            Vector pos = c.Position;
            for (int i = 0; i < NESW.Length; i++)
            {
                Vector curr = pos + NESW[i];
                if (curr.IsBetween(0, 0, Width, Height))
                {
                    Cell step = GetFromVec(curr);
                    if (!visited.Contains(step) && !step.IsBlocked)
                    {
                        dirs.Add(step);
                    }
                }
            }

            return dirs;
        }
    }
}
