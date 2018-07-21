using ATP2016Project.Model.Algrothims.MazeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    class BreadthFirstSearch : ASearchingAlgorithm
    {
        public override Solution Solve(ISearchable searchDomain)
        {
            StartMeasureTime();
            ClearOpenClosedLists();
            AState startingState = searchDomain.GetStartState();
            AddToOpenList(startingState); // add initial state to openList
            while (!IsEmptyOpenList()) // as long openList isnt empty
            {
                AState state = PopOpenList(); // get a state from the queue
                if (state.State.Equals(searchDomain.GetGoalState().State)) // check if it is a goalState
                {
                    StopMeasureTime();
                    getBackTrace(state); // get the traceBack of the solution
                    Solution.RevereseSolution();
                    return Solution;
                }
                SUCCESORS = searchDomain.GetAllSuccessors(state); // find all the neighbors of the state
                AddToClosedList(state);
                foreach (AState astate in SUCCESORS)
                {
                    if (!IsStateInOpenList(astate) && !IsStateInClosedList(astate)) // if it doesnt exist in the OpenList and CloseList
                    {
                        astate.ParentState = state; // update his parent
                        AddToOpenList(astate); // add it to openList
                    }
                }
            }
            return null;// if there isnt solution
        }
    }
}
