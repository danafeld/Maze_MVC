using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    class DepthFirstSearch : ASearchingAlgorithm
    {
        private List<AState> m_discovered;

        public DepthFirstSearch()
        {
            m_discovered = new List<AState>(); // list of discovered states
        }
        public override Solution Solve(ISearchable searchDomain)
        {
            StartMeasureTime();// start measure the time
            ClearOpenClosedLists();
            AState stratingState = searchDomain.GetStartState();
            addToStack(stratingState); // add the initial state to stack
            while (!IfStackEmpty())
            {
                AState state = popStack(); // pop from the stack
                if (state.State.Equals(searchDomain.GetGoalState().State)) // check if it is a goalState
                {
                    StopMeasureTime(); // stop measure the time
                    getBackTrace(state); // get the traceBack of the solution
                    Solution.RevereseSolution();
                    return Solution;
                }
                if (!isDiscovered(state)) // if state is not discovered
                {
                  m_discovered.Add(state); // add it to DiscoverdLIst
                  SUCCESORS = searchDomain.GetAllSuccessors(state); // foreach state in succersors, add it to stack
                   foreach (AState Astate in SUCCESORS)
                        addToStack(Astate);
                }
            }
            return null;

        }

        public bool isDiscovered(AState state) // check if the state was discovered
        {
            foreach (AState s in m_discovered)// for each state in discovered list
            {
                if (s.State.Equals(state.State))// check if equals
                    return true;
            }
            return false;
        }
    }


}
