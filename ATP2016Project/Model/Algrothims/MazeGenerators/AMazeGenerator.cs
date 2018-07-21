using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    abstract class AMazeGenerator : IMazeGenerator
    {
        /// <summary>
        /// Arraylist of positions in the maze
        /// </summary>
        protected ArrayList positionsInMaze;
        public abstract Maze generate(int x, int y, int z);
        public Random ran= new Random();

        /// <summary>
        /// measure the time that take the algorithm to bulid the maze
        /// </summary>
        /// <param name="x">the size of dim x</param>
        /// <param name="y">the size of dim y</param>
        /// <param name="z">the size of dim z</param>
        /// <returns>the total time to build the maze</returns>
        public string measureAlgorithmTime(int x, int y, int z)
        {
            DateTime before = DateTime.Now;
            generate(x, y, z);
            DateTime after = DateTime.Now;
            TimeSpan t = after - before;
            return t.ToString();
        }

        /// <summary>
        /// get all the neighbors walls and return a wall to visit in and to move to it
        /// </summary>
        /// <param name="p">get cuurent position</param>
        /// <returns>the position to move to - wall</returns>
        protected Position getUnvisitedWallNeighborCell(Position p)
        {
            if (p.Y % 2 == 0)
            {
                if (isPositionExist(new Position(p.X - 1, p.Y, p.Z), positionsInMaze) && isPositionExist(new Position(p.X + 1, p.Y, p.Z), positionsInMaze))
                {
                    return null;
                }
                else if (isPositionExist(new Position(p.X + 1, p.Y, p.Z), positionsInMaze) && !isPositionExist(new Position(p.X - 1, p.Y, p.Z), positionsInMaze))
                {
                    return new Position(p.X - 1, p.Y, p.Z);
                }
                else
                {
                    return new Position(p.X + 1, p.Y, p.Z);
                }
            }
            else
            {
                if (isPositionExist(new Position(p.X, p.Y + 1, p.Z), positionsInMaze) && isPositionExist(new Position(p.X, p.Y - 1, p.Z), positionsInMaze))
                {
                    return null;
                }
                else if (isPositionExist(new Position(p.X, p.Y + 1, p.Z), positionsInMaze) && !isPositionExist(new Position(p.X, p.Y - 1, p.Z), positionsInMaze))
                {
                    return new Position(p.X, p.Y - 1, p.Z);
                }
                else
                {
                    return new Position(p.X, p.Y + 1, p.Z);
                }
            }
        }

        /// <summary>
        /// check if the position exist in the array and return true/false
        /// </summary>
        /// <param name="position">get current position</param>
        /// <param name="array"> array of positions</param>
        /// <returns>true if the position exist or false</returns>
        public bool isPositionExist(Position position, ArrayList array)
        {
            foreach (Position p in array)
            {
                if (p.equals(position))
                    return true;
            }
            return false;
        }

    }


}
