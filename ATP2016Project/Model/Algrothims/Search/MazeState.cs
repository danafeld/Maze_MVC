using System;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    class MazeState : AState
    {
        private Position m_position;

        /// <summary>
        /// inisialize the MazeSate
        /// </summary>
        /// <param name="Parent_state">get the ParentState</param>
        /// <param name="position">get the Position</param>
        public MazeState(AState Parent_state, Position position) : base(Parent_state)
        {
            m_position = position;
            m_state = position.ToString();
        }


        /// <summary>
        /// get and set of the position
        /// </summary>
        public Position Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        /// <summary>
        /// compare the MazeState
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public override int CompareTo(AState state)
        {
            if (M_G > state.M_G)
                return 1;
            else
            {
                if (M_G < state.M_G)
                    return -1;
                return 0;
            }
        }

        /// <summary>
        /// check if the Mase States are equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(AState obj)
        {
            //if ((obj.State == State) && (obj.M_G == M_G) && obj.ParentState.State == ParentState.State && obj.ParentState.M_G == ParentState.M_G)
            //    return true;
            //return false;

            if (obj.State == State)
                return true;
            return false;
        }

        /// <summary>
        /// print the mazeState
        /// </summary>
        public override void PrintState()
        {
            Console.WriteLine(m_state);

        }

    }
}
