using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ATP2016Project.Model.Algrothims.Search
{
    abstract class ASearchingAlgorithm : ISearchingAlgorithm
    {
        private Queue<AState> m_openList;
        private Stopwatch m_stopWatch;
        private int m_countGeneratedNodes;
        private Queue<AState> m_closedList;
        private Stack<AState> m_stack;
        private Solution m_solution;
        private List<AState> m_successors;

        /// <summary>
        /// constructor of the ASearchingAlgorithm
        /// initialize the fields
        /// </summary>
        public ASearchingAlgorithm()
        {
            m_openList = new Queue<AState>();
            m_closedList = new Queue<AState>();
            m_stack = new Stack<AState>();
            m_stopWatch = new Stopwatch();
            m_solution = new Solution();
            m_successors = new List<AState>();
            m_countGeneratedNodes = 1;
        }

        /// <summary>
        /// set and get of the Solution
        /// </summary>
        public Solution Solution
        {
            get { return m_solution; }
            set { m_solution = value; }
        }

        /// <summary>
        /// set and get of the list of Succesors
        /// </summary>
        public List<AState> SUCCESORS
        {
            get { return m_successors; }
            set { m_successors = value; }
        }

        /// <summary>
        /// clear the openList and the closedList
        /// </summary>
        protected void ClearOpenClosedLists()
        {
            m_openList.Clear();
            m_closedList.Clear();
            m_stack.Clear();
        }

        /// <summary>
        /// add State to OpenList
        /// </summary>
        /// <param name="state">state in the maze</param>
        protected void AddToOpenList(AState state)
        {
            m_openList.Enqueue(state);
            m_countGeneratedNodes++;
        }

        /// <summary>
        /// add state to ClosedList
        /// </summary>
        /// <param name="state">state in the maze</param>
        protected void AddToClosedList(AState state)
        {
            m_closedList.Enqueue(state);
        }

        /// <summary>
        /// remove from the OpenList
        /// </summary>
        /// <returns>return the state frpm the openList</returns>
        protected AState PopOpenList()
        {
            return m_openList.Dequeue();
        }

        /// <summary>
        /// check if the state is in the openList
        /// </summary>
        /// <param name="state">state in the maze</param>
        /// <returns>return true if the state exist in the list</returns>
        protected bool IsStateInOpenList(AState state)
        {
            foreach (AState s in m_openList)
            {
                if (state.State.Equals(s.State))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// check if the state is in the closedList
        /// </summary>
        /// <param name="state">state in the maze</param>
        /// <returns>return true if the state exist in the list</returns>
        protected bool IsStateInClosedList(AState state)
        {
            foreach (AState s in m_closedList)
            {
                if (state.State.Equals(s.State))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// check if the list is Empty
        /// </summary>
        /// <returns>true if the list is empty</returns>
        protected bool IsEmptyOpenList()
        {
            return (m_openList.Count == 0);
        }

        /// <summary>
        /// get the size of the OpenList
        /// </summary>
        /// <returns>the size of the openList</returns>
        public int GetOpenListSize()
        {
            return m_openList.Count;
        }

        /// <summary>
        /// get the numbers of nodes in the way in the maze
        /// </summary>
        /// <returns>the numbers of nodes</returns>
        public int GetNumberOfGeneratedNodes()
        {
            return m_countGeneratedNodes;
        }

        /// <summary>
        /// get the time that take to solve the maze
        /// </summary>
        /// <returns>time to solve the maze</returns>
        public double GetSolvingTimeMiliseconds()
        { 
            return m_stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// start to measure the time
        /// </summary>
        public void StartMeasureTime()
        {
            m_stopWatch.Restart();
        }

        /// <summary>
        /// stop to measure the time
        /// </summary>
        public void StopMeasureTime()
        {
            m_stopWatch.Stop();
        }

        /// <summary>
        /// abstract class - solve the maze
        /// </summary>
        /// <param name="searchDomain"> Isearchable</param>
        /// <returns>Solution of steps in the maze</returns>
        public abstract Solution Solve(ISearchable searchDomain);

        /// <summary>
        /// add state to stack
        /// </summary>
        /// <param name="state">state in the maze</param>
        public void addToStack(AState state)
        {
            m_stack.Push(state);
            m_countGeneratedNodes++;
        }

        /// <summary>
        /// check if the stuck is Empty
        /// </summary>
        /// <returns>return true if the stuck is empty</returns>
        public bool IfStackEmpty()
        {
            if (m_stack.Count == 0)
                return true;
            return false;
        }

        /// <summary>
        /// remove from the stack
        /// </summary>
        /// <returns>state in the maze</returns>
        public AState popStack()
        {
            return m_stack.Pop();
        }

        /// <summary>
        /// get the way in the maze to solve it
        /// </summary>
        /// <param name="state">state in the maze</param>
        public void getBackTrace(AState state)
        {
            Solution.AddState(state);
            while (state.ParentState != null)
            {
                Solution.AddState(state.ParentState);//add to SolutionList
                state = state.ParentState; // update the fatherState
            }
        }

       
    }
}
