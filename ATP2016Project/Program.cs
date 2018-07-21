using ATP2016Project.Model.Algrothims.MazeGenerators;
using ATP2016Project.Model.Algrothims.Search;
using ATP2016Project.Model.Algrothims.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Controller;
using ATP2016Project.Model;
using ATP2016Project.View;
using System.IO;

namespace ATP2016Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // testMaze2dGenerator(new SimpleMaze2dGenerator());
            // testMaze3dGenerator(new MyMaze3dGenerator());
            // testSearchAlgorithms();

            //****for part 2 ****//
            //MyMaze3DCompressor comp = new MyMaze3DCompressor();
            //IMazeGenerator mg = new MyMaze3dGenerator();
            //Maze maze = mg.generate(4, 4, 5);
            //byte[] maze_byte = ((Maze3d)maze).toByteArray();
            //byte[] maze_byte_comp = comp.compress(maze_byte);
            //printByte(maze_byte_comp);
            //byte[] maze_byte_decomp = comp.decompress(maze_byte_comp);
            //  TestMyCompressorStream();
            //**end tests for part 2 ***//

            // **** for part 4 **** //
            MVC();
            // **** end part 4 **** //

            //TestCheck();
            Console.ReadKey();
        }

        private static void testMaze2dGenerator(IMazeGenerator mg)
        {
            Console.WriteLine(mg.measureAlgorithmTime(20, 14, 0));
            Maze maze = mg.generate(20, 14, 0);
            Position start = maze.getStartPosition();
            Console.WriteLine("The Start Position:");
            start.print();
            Console.WriteLine("The Goal Position:");
            maze.getGoalPosition().print();
            maze.print();
        }

        private static void testMaze3dGenerator(IMazeGenerator mg)
        {
            Console.WriteLine(mg.measureAlgorithmTime(20, 14, 3));
            Maze maze = mg.generate(20, 14, 3);
            Position start = maze.getStartPosition();
            Console.WriteLine("The Start Position:");
            start.print();
            Console.WriteLine("The Goal Position:");
            maze.getGoalPosition().print();
            maze.print();
        }

        private static void testSearchAlgorithms()
        {
            ASearchingAlgorithm BFS = new BreadthFirstSearch();
            ASearchingAlgorithm DFS = new DepthFirstSearch();
            AMazeGenerator g = new MyMaze3dGenerator();
            Maze maze = g.generate(5, 5, 4);
            ISearchable search = new SearchableMaze3d(maze);
            Console.WriteLine("****************Part 2 - Tests***************");
            Console.WriteLine("The Start State: {0}", maze.getStartPosition());
            Console.WriteLine("The Goal State: {0}", maze.getGoalPosition());
            Console.WriteLine();
            //BFS
            maze.print();
            Console.WriteLine("***************The Solve by BFS:***************");
            Solution s_bfs = BFS.Solve(search);
            s_bfs.printSolution();
            Console.WriteLine();

            //DFS
            // maze.print();
            Console.WriteLine("press any key to continue to DFS...");
            Console.ReadLine();
            Console.WriteLine("***************The Solve by DFS:***************");
            Solution s_dfs = DFS.Solve(search);
            s_dfs.printSolution();

            Console.WriteLine();
            Console.WriteLine("****************Results***************");
            Console.WriteLine("The Numbers of Nodes in BFS solution: {0} Nodes", s_bfs.numOfNodesInSolution());
            Console.WriteLine("The Numbers of Nodes in DFS solution: {0} Nodes", s_dfs.numOfNodesInSolution());
            Console.WriteLine("The Numbers of Generated Nodes in BFS: {0} Nodes", BFS.GetNumberOfGeneratedNodes());
            Console.WriteLine("The Numbers of Generated Nodes in DFS: {0} Nodes", DFS.GetNumberOfGeneratedNodes());
            Console.WriteLine("Time that take to find solution in BFS: {0} Milliseconds", BFS.GetSolvingTimeMiliseconds());
            Console.WriteLine("Time that take to find solution in DFS: {0} Milliseconds", DFS.GetSolvingTimeMiliseconds());
        }

        public static void printByte(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
            }
            Console.WriteLine();
        }

        private static void MVC()
        {
            IController controller = new MyController();
            IModel model = new MyModel(controller);
            controller.SetModel(model);
            IView view = new CLI(controller, controller.GetCommands());
            controller.SetView(view);
            view.Start();
        }

        private static void TestMyCompressorStream()
        {
            IMazeGenerator mg = new MyMaze3dGenerator();
            Maze maze = mg.generate(10, 14, 5);
            Console.WriteLine("Test - My Stream Compressor");
            string filePath = @"compressedFile.txt";
            using (FileStream fileOutStream = new FileStream(filePath, FileMode.Create))
            {
                using (MyCompressorStream c = new MyCompressorStream(fileOutStream, 1))
                {
                    byte[] byteArray = ((Maze3d)maze).toByteArray();
                    int length = byteArray.Length;
                    Console.WriteLine();
                    for (int i = 0; i < byteArray.Length; i++)
                    {
                        Console.Write(byteArray[i]);
                    }
                    Console.WriteLine();

                    Console.WriteLine();
                    c.Write(byteArray, 0, length);
                }
            }
            Console.WriteLine();
            byte[] mazeBytes;
            using (FileStream fileInStream = new FileStream(filePath, FileMode.Open))
            {
                using (MyCompressorStream inStream = new MyCompressorStream(fileInStream, 2))
                {
                    mazeBytes = new byte[(((Maze3d)maze).toByteArray()).Length];
                    int length = mazeBytes.Length;
                    inStream.Read(mazeBytes, 0, length);
                    Console.WriteLine();
                    Console.WriteLine("*** Data Bytes after reading from the file ***");
                    for (int i = 0; i < mazeBytes.Length; i++)
                    {
                        Console.Write(mazeBytes[i]);
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("*******Before writing to the file*******");
            maze.print();
            Console.WriteLine("*******After reading from the file*******");
            Maze3d mazeAfterReading = new Maze3d(mazeBytes);
            mazeAfterReading.print();
        }

        private static void TestCheck()
        {
            IMazeGenerator mg = new MyMaze3dGenerator();
            Maze maze = mg.generate(8, 8, 5);
            MyMaze3DCompressor comp = new MyMaze3DCompressor();
            byte[] byteArray = ((Maze3d)maze).toByteArray();
            Console.WriteLine("Before");
            for (int i = 0; i < byteArray.Length; i++)
            {
                Console.Write(byteArray[i]);
            }
            Console.WriteLine();
            Console.WriteLine("After");
            byte[] byteArrayAfter = comp.compress(byteArray);
            byte[] byteArrayAfterDec = comp.decompress(byteArrayAfter);
            for (int i = 0; i < byteArrayAfterDec.Length; i++)
            {
                Console.Write(byteArrayAfterDec[i]);
            }
            Console.WriteLine();
        }
    }
}
