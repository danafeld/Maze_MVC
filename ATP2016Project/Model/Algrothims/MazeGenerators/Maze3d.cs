using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    class Maze3d : Maze
    {
        private ArrayList maze3d;
        /// <summary>
        /// restart the maze3d with the sizes
        /// </summary>
        /// <param name="x">dim X of maze3d</param>
        /// <param name="y">dim Y of maze3d</param>
        /// <param name="z">dim z of maze3d</param>
        public Maze3d(int x, int y, int z) : base(x, y, z)
        {
            maze3d = new ArrayList();
        }

        /// <summary>
        /// return the maze3d
        /// </summary>
        /// <returns></returns>

        public ArrayList MAZE3dArray
        {
            get { return maze3d; }
        }

        /// <summary>
        /// constructor of maze with byte array
        /// </summary>
        /// <param name="uncompressed">array byte uncompressed</param>
        public Maze3d(byte[] uncompressed) : base(uncompressed)
        {

            maze3d = new ArrayList();
            m_startPosition = new Position(uncompressed[0], uncompressed[1], uncompressed[2]);
            m_goalPosition = new Position(uncompressed[3], uncompressed[4], uncompressed[5]);
            m_mx = uncompressed[6] / 2 + 1;
            m_my = uncompressed[7] / 2 + 1;
            m_mz = uncompressed[8];
            int i = 9;
            for (int layer = 0; layer < m_mz; layer++)
            {
                Maze2d maze2D = new Maze2d(m_mx, m_my, layer);
                if (layer == 0)
                {
                    maze2D.setStartPosition(m_startPosition);
                }
                if (layer == m_mz - 1)
                {
                    maze2D.setGoalPosition(m_goalPosition);
                }
                for (int row = 0; row < m_mx * 2 - 1; row++)
                {
                    for (int column = 0; column < m_my * 2 - 1; column++)
                    {
                        maze2D.MAZE2d[row, column] = uncompressed[i];
                        i++;
                    }
                }
                maze3d.Add(maze2D);
            }
        }

        /// <summary>
        /// return an arraylist of opsition that the position can move( in 3d up or Down)
        /// </summary>
        /// <param name="p">the current position</param>
        /// <returns>exception - no need to implement</returns>
        public override ArrayList getOptionsToMove(Position p)
        {
            throw new NotImplementedException();
            //asumming that we can always go up or dowm in the layers. so we didnt implement this function.
        }

        /// <summary>
        /// print the 3d maze, call the function maze2d
        /// </summary>
        public override void print()
        {
            int i = 0;
            foreach (Maze2d maze2d in maze3d) // move on the arraylist and print each maze2d
            {
                Console.WriteLine("Level " + i);
                Console.WriteLine();
                printMaze2d(maze2d);
                Console.WriteLine();
                i++;
            }
        }
        /// <summary>
        /// print the maze2d for each layer in maze3d
        /// </summary>
        /// <param name="maze2d">return maze2d</param>
        public void printMaze2d(Maze2d maze2d)
        {
            string wall = "█";
            string path = " ";
            string start = "S";
            string end = "E";
            for (int row = 0; row < MX * 2 - 1; row++)
            {
                for (int column = 0; column < MY * 2 - 1; column++)
                {
                    if (maze2d.MAZE2d[row, column] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;////
                        Console.Write("  ");////
                        //Console.Write("{0}", wall);
                    }
                    else
                    {
                        if (row == maze2d.getStartPosition().X && column == maze2d.getStartPosition().Y && maze2d.MZ == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;////
                            Console.Write(" {0}", start);
                        }
                        else
                        {
                            if (row == maze2d.getGoalPosition().X && column == maze2d.getGoalPosition().Y && maze2d.MZ == MZ - 1)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;////
                                Console.Write(" {0}", end);
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;////
                                Console.Write(" {0}", path);
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        /// <summary>
        /// return the array to byte
        /// </summary>
        /// <returns>return array in bytes</returns>
        public byte[] toByteArray()
        {
            //byte[] MazeToByte = new byte[3 + 3 + 3 + (m_mz * m_my * m_mx)];
            List<byte> MazeToByte = new List<byte>();
            MazeToByte.Add(Convert.ToByte(m_startPosition.X));
            MazeToByte.Add(Convert.ToByte(m_startPosition.Y));
            MazeToByte.Add(Convert.ToByte(m_startPosition.Z));
            MazeToByte.Add(Convert.ToByte(m_goalPosition.X));
            MazeToByte.Add(Convert.ToByte(m_goalPosition.Y));
            MazeToByte.Add(Convert.ToByte(m_goalPosition.Z));
            MazeToByte.Add(Convert.ToByte(MX * 2 - 1));
            MazeToByte.Add(Convert.ToByte(MY * 2 - 1));
            MazeToByte.Add(Convert.ToByte(MZ));
            for (int layer = 0; layer < m_mz; layer++)
            {
                for (int row = 0; row < m_mx * 2 - 1; row++)
                {
                    for (int column = 0; column < m_my * 2 - 1; column++)
                    {
                        MazeToByte.Add(Convert.ToByte(((Maze2d)maze3d[layer]).getWallsCell(row, column)));
                    }
                }
            }
            return MazeToByte.ToArray();
        }

        /// <summary>
        /// checge to int array
        /// </summary>
        /// <param name="number">the number</param>
        /// <param name="oneOrZero">byte zero or byte 1</param>
        /// <returns>array in bytes</returns>
        public byte[] ChangeTobyteint(int number, int oneOrZero)
        {
            byte[] ans = new byte[number];
            for (int i = 0; i < number; i++)
            {
                ans[i] = (byte)oneOrZero;
            }
            return ans;
        }

    }
}
