using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    interface IMazeGenerator
    {
        /// <summary>
        /// get the size of the maze and build it, and a path from start to end, and break walls in the way
        /// </summary>
        /// <param name="x">get the dim x</param>
        /// <param name="y">get the dim y</param>
        /// <param name="z">get the dim z</param>
        /// <returns>the maze after break walls</returns>
        Maze generate(int x, int y, int z);
        /// <summary>
        /// get the size of the maze and calculate the time to bulid it, and a way to solve it
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        string measureAlgorithmTime(int x, int y, int z);
    }
}
