using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    interface ISearchable
    {
        /// <summary>
        /// get the start State 
        /// </summary>
        /// <returns>return the start state</returns>
        AState GetStartState();
        /// <summary>
        /// get the goal state
        /// </summary>
        /// <returns>return the goal state</returns>
        AState GetGoalState();

        /// <summary>
        /// get all the succesors for the the current state
        /// </summary>
        /// <param name="state"></param>
        /// <returns>list of the states</returns>
        List<AState> GetAllSuccessors(AState state);
    }
}
