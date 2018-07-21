using ATP2016Project.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model.Algrothims.Search;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using System.Threading;
using ATP2016Project.Model.Algrothims.Compression;
using System.Runtime.CompilerServices;

namespace ATP2016Project.Model
{
    class MyModel : IModel
    {

        private IController m_controller;
        private Dictionary<string, Maze> m_DicOfMazes;
        private Dictionary<string, Solution> m_solutions;
        private static List<Thread> m_threads = new List<Thread>();

        /// <summary>
        /// constructor of the model
        /// </summary>
        /// <param name="controller">get the controller</param>
        public MyModel(IController controller)
        {
            m_controller = controller;
            m_solutions = new Dictionary<string, Solution>();
            m_DicOfMazes = new Dictionary<string, Maze>();

        }

        /// <summary>
        /// get the list of the threads
        /// </summary>
        /// <returns>list of the threads</returns>
        public List<Thread> getListOfThreads()
        {
            return m_threads;
        }

        /// <summary>
        /// get all the folders and files in the path
        /// </summary>
        /// <param name="path">path of the dir</param>
        /// <returns>lstring of all folders and files</returns>
        public string GetDir(string path)
        {
            string dirs = "";
            //foreach (string s in Directory.GetDirectories(path))
            //{
            //    dirs += s + " \n";
            //}
            //foreach (string s in Directory.GetFiles(path))
            //{
            //    dirs += s + " \n";
            //}
            foreach (string s in Directory.GetFileSystemEntries(path))
            {
                dirs += s + " \n";
            }
            return dirs;
        }
        /// <summary>
        /// generate the maze 3d
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <param name="x">dim x</param>
        /// <param name="y">dim y</param>
        /// <param name="z">dim z</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void GenerateMaze3d(string maze_name, int x, int y, int z)
        {
            Thread thread = new Thread(() => GenerateMaze3dInNewThread(maze_name, x, y, z));
            thread.Name = "GenerateMaze3dMazeThread";
            thread.Start();
            m_threads.Add(thread);
            //  m_controller.Output("The maze <" + name + "> is ready!"); // succeded
        }

        /// <summary>
        /// generate the maze3d in new thread
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <param name="x">dim x</param>
        /// <param name="y">dim y</param>
        /// <param name="z">dim z</param>
        private void GenerateMaze3dInNewThread(string maze_name, int x, int y, int z)
        {
            MyMaze3dGenerator maze = new MyMaze3dGenerator();
            Maze new_maze = maze.generate(x, y, z);
            IfSolutionExist(maze_name); // if the solution exist with identical name - remove it.
            IfNameMazeExist(maze_name); // if the name maze exit - remove it.
            m_DicOfMazes.Add(maze_name, new_maze); // add new maze to dictionary of mazes
        }

        /// <summary>
        /// save the maze to disk
        /// </summary>
        /// <param name="maze_name">name of the file</param>
        /// <param name="filePath">path of the file</param>
        public void SaveMazeToDisk(string maze_name, string filePath)
        {
            byte[] mazeToByte;
            string fullPath = filePath + maze_name;
            using (Stream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (Stream file_s = new MyCompressorStream(stream, 1))
                {
                    MyCompressorStream file_Str = file_s as MyCompressorStream;
                    mazeToByte = ((Maze3d)(m_DicOfMazes[maze_name])).toByteArray();
                    file_Str.Write(mazeToByte, 0, mazeToByte.Length);
                    file_Str.Flush();
                }
            }
        }

        /// <summary>
        /// load the maze from the disk
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <param name="maze_name">name of the maze</param>
        public void LoadMazeFromDisk(string filePath, string maze_name)
        {
            string fullPath = filePath + maze_name;
            byte[] byteFromFile = new byte[100];
            List<byte> byteMazeFromFile = new List<byte>();
            using (Stream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (Stream file_s = new MyCompressorStream(stream, 2))
                {
                    MyCompressorStream file_Str = file_s as MyCompressorStream;
                    int read_f = 0;
                    while ((read_f = file_Str.Read(byteFromFile, 0, 100)) != 0)
                    {
                        if (read_f < 100)
                            addToList(byteMazeFromFile, byteFromFile, read_f);
                        else
                            byteMazeFromFile.AddRange(byteFromFile);
                        byteFromFile = new byte[100];
                    }
                    byte[] b = byteMazeFromFile.ToArray();
                    m_DicOfMazes[maze_name] = new Maze3d(b);
                    file_Str.Flush();
                }
            }
        }
        /// <summary>
        /// if the buffer smaller than 100 add vlaue by value
        /// </summary>
        /// <param name="byteMazeFromFile">list of bytes</param>
        /// <param name="byteFromFile">array of bytes</param>
        /// <param name="read_f">int of read</param>
        private void addToList(List<byte> byteMazeFromFile, byte[] byteFromFile, int read_f)
        {
            int counter = 0;
            while (counter < read_f)
            {
                byteMazeFromFile.Add(byteFromFile[counter]);
                counter++;
            }
        }
        /// <summary>
        /// get the size of the maze
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <returns>get the size of the maze</returns>
        public int MazeSize(string maze_name)
        {
            int size_maze;
            Maze maze = m_DicOfMazes[maze_name];
            Maze3d maze3d = maze as Maze3d;
            size_maze = maze3d.toByteArray().Length;
            return size_maze;
        }

        /// <summary>
        /// the size of the file
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <returns>get the size of the file</returns>
        public long FileSize(string filePath)
        {
            long size_file = new FileInfo(filePath).Length;
            return size_file;
        }

        /// <summary>
        /// solve the maze
        /// </summary>
        /// <param name="maze_name">name of the maze</param>
        /// <param name="alg">algorithem to solve the maze</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SolveMaze(string maze_name, ASearchingAlgorithm alg)
        {
            Thread thread = new Thread(() => SolveMazeInNewThread(maze_name, alg));
            thread.Name = "SolveMazeThread";
            thread.Start();
            m_threads.Add(thread);
            //  m_controller.Output("Solution for maze <" + name + "> is ready!"); // succeded
        }

        /// <summary>
        /// solve the maze in new Thread
        /// </summary>
        /// <param name="name">mame of the thread</param>
        /// <param name="new_alg">alg of solving</param>
        private void SolveMazeInNewThread(string name, ASearchingAlgorithm new_alg)
        {
            Maze m_from_dic = m_DicOfMazes[name];
            Maze3d m_3d = m_from_dic as Maze3d;
            SearchableMaze3d maze3dS = new SearchableMaze3d(m_3d);
            Solution maze_solution = new_alg.Solve(maze3dS); // solve the maze
            IfSolutionExist(name); // if the solution exist with identical name - remove it.
            m_solutions.Add(name, maze_solution); // add to dictionary of solutions
        }

        /// <summary>
        /// check if the algorithm exist and which algorithm it is
        /// </summary>
        /// <param name="alg">the algorithem to solve the maze</param>
        /// <returns>get the algorithm</returns>
        public ASearchingAlgorithm checkAlg(string alg)
        {
            if (alg.ToLower() == "bfs")
            {
                BreadthFirstSearch bfs = new BreadthFirstSearch();
                return bfs;
            }

            if (alg.ToLower() == "dfs")
            {
                DepthFirstSearch dfs = new DepthFirstSearch();
                return dfs;
            }
            return null;
        }

        /// <summary>
        /// get the maze from the dictionary
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <returns>the maze from the dictionary</returns>
        public Maze getMaze(string name)
        {
            if (m_DicOfMazes.ContainsKey(name))
                return m_DicOfMazes[name];
            else return null;
        }

        /// <summary>
        /// get the solution of the maze of positions
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <returns>get the solution of the maze</returns>
        public Solution getSolution(string name)
        {
            if (m_solutions.ContainsKey(name))
                return m_solutions[name];
            else return null;

        }
        /// <summary>
        /// check if exist already solution
        /// </summary>
        /// <param name="name">name of the maze</param>
        private void IfSolutionExist(string name)
        {
            if (m_solutions.ContainsKey(name))
            {
                m_solutions.Remove(name);
                Console.ForegroundColor = ConsoleColor.Yellow;
                m_controller.Output("The Solution for this maze is already exit. Therfore, It has been Removed.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// check if name maze is exist already
        /// </summary>
        /// <param name="name">name of the maze</param>
        private void IfNameMazeExist(string name)
        {
            if (m_DicOfMazes.ContainsKey(name))
            {
                m_DicOfMazes.Remove(name);
                Console.ForegroundColor = ConsoleColor.Yellow;
                m_controller.Output("This name maze is already exit. Therfore, It has been Removed.");
                Console.ResetColor();
            }
        }
    }
}
