using ATP2016Project.Model.Algrothims.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using System.Threading;

namespace ATP2016Project.Model
{
    interface IModel
    {
        /// <summary>
        /// get all the files and folders of the path
        /// </summary>
        /// <param name="path">path of the dircetory</param>
        /// <returns>string of all the files and folders</returns>
        string GetDir(string path);
        /// <summary>
        /// generate the maze3d
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <param name="x">dim x</param>
        /// <param name="y">dim y</param>
        /// <param name="z">dim z</param>
        void GenerateMaze3d(string maze_name, int x, int y, int z);
        /// <summary>
        /// save the maze to the disk
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <param name="filePath">path of the maze</param>
        void SaveMazeToDisk(string maze_name, string filePath);
        /// <summary>
        /// load from the disk the maze
        /// </summary>
        /// <param name="filePath">name of the file path</param>
        /// <param name="maze_name">name of the file</param>
        void LoadMazeFromDisk(string filePath, string maze_name);
        /// <summary>
        /// get the maze size in bytes
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <returns>get the size of maze</returns>
        int MazeSize(string maze_name);
        /// <summary>
        /// get the size of the file in bytes
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <returns>the file size in bytes</returns>
        long FileSize(string filePath);
        /// <summary>
        /// solve the maze with the algorithems
        /// </summary>
        /// <param name="maze_name">name of the ,aze</param>
        /// <param name="alg">the algorithm to solve</param>
        void SolveMaze(string maze_name, ASearchingAlgorithm alg);
        /// <summary>
        /// get the maze from the dictionary
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <returns>get the maze</returns>
        Maze getMaze(string name);
        /// <summary>
        /// get the solution of position in the solved maze
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <returns>solution od the maze that was solved</returns>
        Solution getSolution(string name);
        /// <summary>
        /// check if the algorithm exists
        /// </summary>
        /// <param name="alg">algorithem to solve the maze</param>
        /// <returns>the algorithm to solve the maze</returns>
        ASearchingAlgorithm checkAlg(string alg);
        /// <summary>
        /// get the list of the threads
        /// </summary>
        /// <returns>list of threads</returns>
        List<Thread> getListOfThreads();
    }
}
