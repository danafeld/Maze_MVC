using System;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Search
{
    class SearchableMaze3d : ISearchable
    {
        private Maze m_maze;

        /// <summary>
        /// inisialize the SearchableMaze3d
        /// </summary>
        /// <param name="maze">get the Maze</param>
        public SearchableMaze3d(Maze maze)
        {
            m_maze = maze;
        }

        /// <summary>
        /// get and set of the Maze3d
        /// </summary>
        public Maze MAZE3d
        {
            get { return m_maze; }
            set { m_maze = value; }
        }

        /// <summary>
        /// get all the Succesors for the current state
        /// </summary>
        /// <param name="state"></param>
        /// <returns>list of Succesors</returns>
        public List<AState> GetAllSuccessors(AState state)
        {
            List<AState> successors = new List<AState>();
            MazeState m = state as MazeState;
            Position down = new Position(m.Position.X + 1, m.Position.Y, m.Position.Z); // create down position
            checkDown(down, successors, m, state); // check for the down position                
            Position up = new Position(m.Position.X - 1, m.Position.Y, m.Position.Z); // create up position
            checkUp(up, successors, m, state); // check for the up position
            Position right = new Position(m.Position.X, m.Position.Y + 1, m.Position.Z); // create right position
            checkRight(right, successors, m, state);   // check for the right position              
            Position left = new Position(m.Position.X, m.Position.Y - 1, m.Position.Z); // create left position
            checkLeft(left, successors, m, state); // check for the up position
            Position UpinZ = new Position(m.Position.X, m.Position.Y, m.Position.Z + 1);
            if (checkIfInLimits(UpinZ) == true && checkIfThereIsAWall(UpinZ) == false) // up in layers in maze
            {
                AState n_Astate = new MazeState(state, UpinZ); // add to succersos if it in limits and if not wall
                successors.Add(n_Astate);
            }
            Position DowninZ = new Position(m.Position.X, m.Position.Y, m.Position.Z - 1);
            if (checkIfInLimits(DowninZ) == true && checkIfThereIsAWall(DowninZ) == false) // down in layers in maze
            {
                AState n_Astate = new MazeState(state, DowninZ); // add to succersos if it in limits and if not wall
                successors.Add(n_Astate);
            }
            return successors;
        }

        /// <summary>
        /// check if there is a way in down position
        /// </summary>
        /// <param name="down"> position in maze</param>
        /// <param name="successors">list of succesors</param>
        /// <param name="m">maze state</param>
        /// <param name="state"> state</param>
        public void checkDown(Position down, List<AState> successors, MazeState m, AState state)
        {
            if (checkIfInLimits(down) == true) // check if in limits of the maze
            {
                if (checkIfThereIsAWall(down) == false) // check if it is a wall
                {
                    Position downCell = new Position(m.Position.X + 2, m.Position.Y, m.Position.Z); // cell that it is part of the options in the maze
                    if (checkIfInLimits(downCell) == true)
                    {
                        AState n_Astate = new MazeState(state, downCell); // add to succersos if it in limits and if not wall
                        successors.Add(n_Astate);
                    }
                }
            }
        }

        /// <summary>
        /// check if there is a way in right position
        /// </summary>
        /// <param name="down"> position in maze</param>
        /// <param name="successors">list of succesors</param>
        /// <param name="m">maze state</param>
        /// <param name="state"> state</param>
        public void checkRight(Position right, List<AState> successors, MazeState m, AState state)
        {
            if (checkIfInLimits(right) == true) // check if in limits of the maze
            {
                if (checkIfThereIsAWall(right) == false) // check if it is a wall
                {
                    Position rightCell = new Position(m.Position.X, m.Position.Y + 2, m.Position.Z); // cell that it is part of the options in the maze
                    if (checkIfInLimits(rightCell) == true)
                    {
                        AState n_Astate = new MazeState(state, rightCell); // add to succersos if it in limits and if not wall
                        successors.Add(n_Astate);
                    }
                }
            }
        }

        /// <summary>
        /// check if there is a way in up position
        /// </summary>
        /// <param name="down"> position in maze</param>
        /// <param name="successors">list of succesors</param>
        /// <param name="m">maze state</param>
        /// <param name="state"> state</param>
        public void checkUp(Position up, List<AState> successors, MazeState m, AState state)
        {
            if (checkIfInLimits(up) == true) // check if in limits of the maze
            {
                if (checkIfThereIsAWall(up) == false) // check if it is a wall
                {
                    Position upCell = new Position(m.Position.X - 2, m.Position.Y, m.Position.Z); // cell that it is part of the options in the maze
                    if (checkIfInLimits(upCell) == true)
                    {
                        AState n_Astate = new MazeState(state, upCell); // add to succersos if it in limits and if not wall
                        successors.Add(n_Astate);
                    }
                }
            }
        }

        /// <summary>
        /// check if there is a way in left position
        /// </summary>
        /// <param name="down"> position in maze</param>
        /// <param name="successors">list of succesors</param>
        /// <param name="m">maze state</param>
        /// <param name="state"> state</param>
        public void checkLeft(Position left, List<AState> successors, MazeState m, AState state)
        {
            if (checkIfInLimits(left) == true) // check if in limits of the maze
            {
                if (checkIfThereIsAWall(left) == false) // check if it is a wall
                {
                    Position leftCell = new Position(m.Position.X, m.Position.Y - 2, m.Position.Z); // cell that it is part of the options in the maze
                    if (checkIfInLimits(leftCell) == true)
                    {
                        AState n_Astate = new MazeState(state, leftCell); // add to succersos if it in limits and if not wall
                        successors.Add(n_Astate);
                    }
                }
            }
        }
       
        /// <summary>
        /// check if the position in the limits of the maze
        /// </summary>
        /// <param name="p"></param>
        /// <returns>true if it in limits</returns>
        public bool checkIfInLimits(Position p)
        {
            if (p.X >= 0 && p.Y >= 0 && p.X < MAZE3d.MX * 2 - 1 && p.Y < MAZE3d.MY * 2 - 1 && p.Z >= 0 && p.Z < MAZE3d.MZ)
                return true;
            return false;
        }

        /// <summary>
        /// check if there is a wall in the cell
        /// </summary>
        /// <param name="p">get the position to check about</param>
        /// <returns>true if there is a wall</returns>
        public bool checkIfThereIsAWall(Position p)
        {
            Maze3d m3d = (Maze3d)(m_maze);
            Maze2d m2d = (Maze2d)(m3d.MAZE3dArray[p.Z]);
            if (m2d.MAZE2d[p.X, p.Y] == 1)
                return true;
            return false;
        }
        /// <summary>
        /// get the goalState of the maze
        /// </summary>
        /// <returns>goal State of the maze</returns>
        public AState GetGoalState()
        {
            AState goal = new MazeState(null, m_maze.getGoalPosition());
            return goal;
        }

        /// <summary>
        /// get the startState of the maze
        /// </summary>
        /// <returns>startStateOf the maze</returns>
        public AState GetStartState()
        {
            AState start = new MazeState(null, m_maze.getStartPosition());
            return start;
        }
    }
}
