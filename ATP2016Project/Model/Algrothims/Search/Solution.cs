using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ATP2016Project.Model.Algrothims.Search
{
    class Solution
    {
        private ArrayList m_solutionPath;

        /// <summary>
        /// constructor of solution
        /// </summary>
        public Solution()
        {
            m_solutionPath = new ArrayList();
        }

        /// <summary>
        /// add State to Solution
        /// </summary>
        /// <param name="state">get the state to add</param>
        public void AddState(AState state)
        {
            m_solutionPath.Add(state);
        }

        /// <summary>
        /// check if the solution exist
        /// </summary>
        /// <returns>return if the solution exist</returns>
        public bool IsSolutionExists()
        {
            return m_solutionPath.Count > 0;
        }

        /// <summary>
        /// get the number of steps
        /// </summary>
        /// <returns>return the numbers of solution</returns>
        public int GetSolutionSteps()
        {
            return m_solutionPath.Count;
        }

        /// <summary>
        /// get the array list of states , that are the solution
        /// </summary>
        /// <returns>return the arraylist</returns>
        public ArrayList GetSolutionPath()
        {
            return m_solutionPath;
        }

        /// <summary>
        /// reverse the solution
        /// </summary>
        public void RevereseSolution()
        {
            m_solutionPath.Reverse();
        }

        /// <summary>
        /// print the solution, move of the arraylist and print each state
        /// </summary>
        public void printSolution()
        {
            foreach (AState state in m_solutionPath)
            {
                Console.WriteLine(state.State);
            }
        }

        /// <summary>
        /// get the num of nodes created in the soltion
        /// </summary>
        /// <returns>num of nodes</returns>
        public int numOfNodesInSolution()
        {
            return m_solutionPath.Count;
        }
    }
}
