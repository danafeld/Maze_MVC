using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{


    abstract class Maze : IMaze
    {

        protected int m_mx;
        protected int m_my;
        protected int m_mz;
        protected Position m_startPosition;
        protected Position m_goalPosition;
        protected ArrayList m_optionsToMove;

        /// <summary>
        /// constructor of maze uncompressed
        /// </summary>
        /// <param name="uncompressed">array byte of maze uncompressed</param>
        public Maze(byte[] uncompressed)
        {

        }

        /// <summary>
        /// get and set the x-dim of the maze
        /// </summary>
        public int MX
        {
            get { return m_mx; }
            set { m_mx = value; }
        }

        /// <summary>
        /// get and set the y-dim of the maze
        /// </summary>
        public int MY
        {
            get { return m_my; }
            set { m_my = value; }
        }


        /// <summary>
        /// get and set the z-dim of the maze
        /// </summary>
        public int MZ
        {
            get { return m_mz; }
            set { m_mz = value; }
        }


        /// <summary>
        /// start the maze, build it from sizes and restart the start point and the end point
        /// </summary>
        public Maze(int x, int y, int z)
        {
            m_mx = x;
            m_my = y;
            m_mz = z;
            m_startPosition = new Position(0, 0, 0);
            if (z == 0)
                m_goalPosition = new Position(x - 1, y - 1, 0);
            else m_goalPosition = new Position(x - 1, y - 1, z - 1);
            m_optionsToMove = new ArrayList();
        }

        /// <summary>
        /// function that return the startPosition
        /// </summary>
        /// <returns> start point</returns>
        public Position getStartPosition()
        {
            return m_startPosition;
        }

        /// <summary>
        /// function that retuns the goalPosition
        /// </summary>
        /// <returns>goalPosition</returns>
        public Position getGoalPosition()
        {
            return m_goalPosition;
        }

        /// <summary>
        /// set the goal position
        /// </summary>
        /// <param name="p">position to set goal</param>
        public void setGoalPosition(Position p)
        {
            m_goalPosition.X = p.X;
            m_goalPosition.Y = p.Y;
            m_goalPosition.Z = p.Z;
        }
        /// <summary>
        /// abstarct function that get all the neighbors for the current position
        /// </summary>
        /// <param name="p"></param>
        /// <returns>arraylist of neighbors - option to move</returns>
        public abstract ArrayList getOptionsToMove(Position p);


        /// <summary>
        /// abstract function - print the maze (3d/2d)
        /// </summary>
        public abstract void print();

        /// <summary>
        /// change the goalPosition - by random dim x!
        /// </summary>
        public void setGoalPosition()
        {
            Random r = new Random();
            int num;
            num = r.Next(0, MX - 1);
            m_goalPosition.X = num * 2;
            m_goalPosition.Y = 0;
            if (MZ == 0)
                m_goalPosition.Z = 0;
            else m_goalPosition.Z = MZ - 1;
        }

        /// <summary>
        /// change the startPosition
        /// </summary>
        /// <param name="p">the position that get the values from, and change the current point acording to it</param>
        public void setStartPosition(Position p)
        {
            m_startPosition.X = p.X;
            m_startPosition.Y = p.Y;
            m_startPosition.Z = p.Z;
        }

        /// <summary>
        /// change the goal position , by equal to other position. not by random
        /// </summary>
        /// <param name="p">goal position</param>
        public void finalGoalPosition(Position p)
        {
            m_goalPosition.X = p.X;
            m_goalPosition.Y = p.Y;
            m_goalPosition.Z = p.Z;
        }
    }
}
