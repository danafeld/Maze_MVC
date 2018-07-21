using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATP2016Project.Model.Algrothims.Search
{
    abstract class AState : IComparable<AState>
    {

        protected string m_state;
        protected double m_G;
        protected AState m_parentState;

        /// <summary>
        /// inisialize the state
        /// </summary>
        /// <param name="parentState">get the parentState</param>
        public AState(AState parentState)
        {
            m_parentState = parentState;
        }

        /// <summary>
        /// the weight
        /// </summary>
        public double M_G
        {
            get { return m_G; }
            set { m_G = value; }
        }

        /// <summary>
        /// get and set of state
        /// </summary>
        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        /// <summary>
        /// get and set of Parentstate
        /// </summary>
        public AState ParentState
        {
            get { return m_parentState; }
            set { m_parentState = value; }
        }

        /// <summary>
        /// compare the states
        /// </summary>
        /// <param name="state">get State</param>
        /// <returns></returns>
        public abstract int CompareTo(AState state);

        /// <summary>
        /// check if the states are equal
        /// </summary>
        /// <param name="obj">get state to compare with</param>
        /// <returns></returns>

        public abstract bool Equals(AState obj);

        /// <summary>
        /// print the state
        /// </summary>
        public abstract void PrintState();

    }
}
