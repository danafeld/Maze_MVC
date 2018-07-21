using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    interface IMaze
    {
        /// <summary>
        /// get the start position for the maz
        /// </summary>
        /// <returns></returns>
        Position getStartPosition();
        /// <summary>
        /// get the goal position for the maze
        /// </summary>
        /// <returns></returns>
        Position getGoalPosition();
        /// <summary>
        /// get all the neighbors of the current positions
        /// </summary>
        /// <param name="p">get position</param>
        /// <returns> return arraylist of the neighbors of the posiotion</returns>
        ArrayList getOptionsToMove(Position p);
    }
}
