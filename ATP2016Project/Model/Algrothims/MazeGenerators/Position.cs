using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    class Position
    {
        private int m_x;
        private int m_z;
        private int m_y;
        private Position prev;

        /// <summary>
        /// set and get the X position
        /// </summary>
        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        /// <summary>
        /// set and get the Y position
        /// </summary>
        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        /// <summary>
        /// set and get the Z position
        /// </summary>
        public int Z
        {
            get { return m_z; }
            set { m_z = value; }
        }

        /// <summary>
        ///get the prev Postion of the current position
        /// </summary>
        public Position Father
        {
            get { return prev; }

        }

        /// <summary>
        /// change the father position for the current position
        /// </summary>
        /// <param name="p"></param>
        public void setFatherPosition(Position p)
        {
            prev = new Position(0, 0, 0);
            prev.setPosition(p);
        }

        /// <summary>
        /// constructor of the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Position(int x, int y, int z)
        {
            m_x = x;
            m_y = y;
            m_z = z;

        }

        /// <summary>
        /// set and get the X position, with father field
        /// </summary>
        public Position(int x, int y, int z, Position father)
        {
            m_x = x;
            m_y = y;
            m_z = z;
            prev = new Position(0, 0, 0);
            prev.setPosition(father);
        }



        /// <summary>
        /// print the position (x,y,z)
        /// </summary>
        public void print()
        {
            Console.Write("(");
            Console.Write(m_x);
            Console.Write(",");
            Console.Write(m_y);
            Console.Write(",");
            Console.Write(m_z);
            Console.WriteLine(")");

        }

        /// <summary>
        /// check if the positins are equal. ( by x,y,z)
        /// </summary>
        /// <param name="p"> position to check with</param>
        /// <returns>true if the positions are equal otherwiae-false</returns>
        public bool equals(Position p)
        {
            if (p.X == X && p.Y == Y && p.Z == Z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// change the positions
        /// </summary>
        /// <param name="p">current position get values from this variable</param>
        public void setPosition(Position p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
            this.prev = p.prev;
        }

        /// <summary>
        /// change the Position to String
        /// </summary>
        /// <returns>POsition in String</returns>
        public override string ToString()
        {
            string s = "(" + m_x + "," + m_y + "," + m_z + ")";
            return s;
        }
    }
}
