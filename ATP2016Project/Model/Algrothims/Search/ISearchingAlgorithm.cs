using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    interface ISearchingAlgorithm
    {
        /// <summary>
        /// solve the maze 
        /// </summary>
        /// <param name="searchDomain">get the Isearchable</param>
        /// <returns>Solution of states that solve the maze</returns>
        Solution Solve(ISearchable searchDomain);

        /// <summary>
        /// get ho many nodes take to solve tha maze
        /// </summary>
        /// <returns>numbersOfNodes</returns>
        int GetNumberOfGeneratedNodes();

        /// <summary>
        /// get the time to solve the maze
        /// </summary>
        /// <returns>returns the time to solve the maze</returns>
        double GetSolvingTimeMiliseconds();
    }
}
