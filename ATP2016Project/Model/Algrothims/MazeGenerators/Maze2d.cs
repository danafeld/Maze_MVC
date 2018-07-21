using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    class Maze2d : Maze
    {
        private int[,] m_walls;
        /// <summary>
        /// build the grid of the maze2d , restart the maze according to sizes
        /// </summary>
        /// <param name="x">the dim x</param>
        /// <param name="y">the dim y</param>
        /// <param name="z">the dim z</param>
        public Maze2d(int x, int y, int z) : base(x, y, z)
        {
            m_walls = new int[x * 2 - 1, y * 2 - 1]; // create a grid
            for (int row = 1; row < x * 2 - 1; row = row + 2) // restart the grid - rows in 1
            {
                for (int column = 0; column < y * 2 - 1; column++)
                {
                    m_walls[row, column] = 1;
                }
            }

            for (int column = 1; column < y * 2 - 1; column = column + 2) // restart the grid - columns in 1
            {
                for (int row = 0; row < x * 2 - 1; row++)
                {
                    m_walls[row, column] = 1;
                }
            }
        }

        /// <summary>
        /// return the maze 2d , 2d array with walls 
        /// </summary>
        /// <returns>array of walls</returns>
        public int[,] MAZE2d
        {
            get { return m_walls; }
        }
        /// <summary>
        /// change the cell to 0 - mean break the wall.
        /// </summary>
        /// <param name="x">position in x</param>
        /// <param name="y">position in y</param>
        public void setMazeWallsCell(int x, int y)
        {
            m_walls[x, y] = 0;
        }

        /// <summary>
        /// return the maze2d of walls
        /// </summary>
        /// <param name="x">position in x</param>
        /// <param name="y">position in y</param>
        /// <returns></returns>
        public int getWallsCell(int x, int y)
        {
            return m_walls[x, y];
        }

        /// <summary>
        /// print the maze 2d, with S for start, and E for goal
        /// </summary>
        public override void print()
        {
            string wall = "█";
            string path = " ";
            string start = "S";
            string end = "E";
            for (int row = 0; row < MX * 2 - 1; row++)
            {
                for (int column = 0; column < MY * 2 - 1; column++)
                {
                    if (m_walls[row, column] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;///
                        Console.Write(" ");///
                        //Console.Write("{0}", wall);
                    }
                    else
                    {
                        if (row == getStartPosition().X && column == getStartPosition().Y)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;////
                            Console.Write("{0}", start);
                        }
                        else
                        {
                            if (row == getGoalPosition().X && column == getGoalPosition().Y)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;////
                                Console.Write("{0}", end);
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;////
                                Console.Write("{0}", path);
                            }
                        }
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();             
                }
        }

        /// <summary>
        /// get an arraylist of all the neighbors that the current position can move to
        /// </summary>
        /// <param name="p">current position in maze2d</param>
        /// <returns>arratlist of positions of neighbors (walls)</returns>
        public override ArrayList getOptionsToMove(Position p)
        {
            ArrayList positions = new ArrayList();
            if (p.X + 1 >= 0 && p.Y >= 0 && p.X + 1 < MX * 2 - 1 && p.Y < MY * 2 - 1) // down 
            {
                if (m_walls[p.X + 1, p.Y] == 1)
                    positions.Add(new Position(p.X + 1, p.Y, p.Z));
            }
            if (p.X - 1 >= 0 && p.Y >= 0 && p.X - 1 < MX * 2 - 1 && p.Y < MY * 2 - 1) // up
            {
                if (m_walls[p.X - 1, p.Y] == 1)
                    positions.Add(new Position(p.X - 1, p.Y, p.Z));
            }
            if (p.X >= 0 && p.Y + 1 >= 0 && p.X < MX * 2 - 1 && p.Y + 1 < MY * 2 - 1) // left
            {
                if (m_walls[p.X, p.Y + 1] == 1)
                    positions.Add(new Position(p.X, p.Y + 1, p.Z));
            }
            if (p.X >= 0 && p.Y - 1 >= 0 && p.X < MX * 2 - 1 && p.Y - 1 < MY * 2 - 1) // right
            {
                if (m_walls[p.X, p.Y - 1] == 1)
                    positions.Add(new Position(p.X, p.Y - 1, p.Z));
            }
            return positions;
        }
    }
}
