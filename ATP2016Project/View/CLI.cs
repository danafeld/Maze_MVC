using ATP2016Project.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using System.Collections;
using ATP2016Project.Model.Algrothims.Search;
using System.Threading;

namespace ATP2016Project.View
{
    class CLI : IView
    {
        private IController m_controller;
        Stream m_input = Console.OpenStandardInput();
        Stream m_output = Console.OpenStandardOutput();
        Dictionary<string, ICommand> m_commands;
        private string m_cursor = ">>";

        /// <summary>
        /// construuctior of the CLI
        /// </summary>
        /// <param name="controller">get the controller of the CLI</param>
        public CLI(IController controller)
        {
            // m_commands = new Dictionary<string, ICommand>();
            m_controller = controller;
            m_commands = controller.GetCommands();
        }

        /// <summary>
        /// constrcutor of CLI
        /// </summary>
        /// <param name="controller">get the controller of the CLI</param>
        /// <param name="commands">Dictionry of commands</param>
        public CLI(IController controller, Dictionary<string, ICommand> commands)
        {
            m_controller = controller;
            m_commands = commands;
        }

        /// <summary>
        /// start the program and print the menu
        /// </summary>
        public void Start()
        {
            PrintInstructions();
            string userCommand; string[] splitedCommand; string command;
            m_commands = m_controller.GetCommands();
            while (true)
            {
                try
                {
                    Output("");
                    userCommand = Input().Trim().ToLower();
                    splitedCommand = userCommand.Split(' ');
                    command = splitedCommand[0];
                    if (!m_commands.ContainsKey(command))
                    {
                        Output("Unrecognized command!");
                    }
                    else
                    {
                        splitedCommand = CommandWithoutName(splitedCommand);
                        m_commands[command].DoCommand(splitedCommand);
                        Thread.Sleep(1000);
                        PrintInstructions();
                    }
                }
                catch (Exception e)
                {
                    Output("Unrecognized command!");
                }
            }
        }

        /// <summary>
        /// get the string array without the command
        /// </summary>
        /// <param name="old_splited_command">string array of old splitted array</param>
        /// <returns>string array of command without the name</returns>
        private string[] CommandWithoutName(string[] old_splited_command)
        {
            string[] new_splitedCommand = new string[old_splited_command.Length - 1];
            for (int i = 0; i < new_splitedCommand.Length; i++)
            {
                new_splitedCommand[i] = old_splited_command[i + 1];
            }
            return new_splitedCommand;
        }
        /// <summary>
        /// print the instuction of the menu
        /// </summary>
        public static void PrintInstructions()
        {
            Console.WriteLine(" => Enter the name of the command from the list below");
            Console.WriteLine(" --- Available Commands: ---");
            Console.WriteLine("(1) Dir <path> : preview all the folders and files in the <path>");
            Console.WriteLine("(2) Generate3dMaze <maze name><other params> : generate the maze <name maze> with the sizes <x,y,z>");
            Console.WriteLine("(3) Display <Maze name> : print the maze <name maze>");
            Console.WriteLine("(4) SaveMaze <maze name><file path> : save the maze <name maze> to <path>");
            Console.WriteLine("(5) LoadMaze <file path><maze name> : load the maze <name maze> from <path>");
            Console.WriteLine("(6) MazeSize<maze name> : preview the size of maze <maze name> in bytes");
            Console.WriteLine("(7) FileSize<file path> : preview the size of file <file path> in bytes");
            Console.WriteLine("(8) SolveMaze<maze name><algorithm> : solve the maze <name maze> with algorithm <algorithm>");
            Console.WriteLine("(9) DisplaySolution <maze name> : preview the soltion of maze <name maze>");
            Console.WriteLine("(0) Exit : exit from the menu");
            Console.WriteLine();
        }

        /// <summary>
        /// print the output to the screen
        /// </summary>
        /// <param name="output">string of the output</param>
        public void Output(string output)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            StreamWriter streamWriter = new StreamWriter(m_output);
            streamWriter.AutoFlush = true;
            Console.SetCursorPosition(0, Console.CursorTop);
            streamWriter.WriteLine(output);
            streamWriter.WriteLine("");
            streamWriter.Write(m_cursor);
            Console.ResetColor();
        }

        /// <summary>
        /// get the input from the user
        /// </summary>
        /// <returns>get the string of the input</returns>
        public string Input()
        {
            string input;
            //using (StreamReader streamReader = new StreamReader(m_input))
            //{
            //    input = streamReader.ReadLine();
            //}
            StreamReader streamReader = new StreamReader(m_input);
            input = streamReader.ReadLine();
            return input;
        }

        /// <summary>
        /// change the dictionary of the commands
        /// </summary>
        /// <param name="commands">dictionary of commands</param>
        public void SetCommands(Dictionary<string, ICommand> commands)
        {
            m_commands = commands;
        }

        /// <summary>
        /// change the input
        /// </summary>
        /// <param name="output">stream of the input</param>
        public void SetInput(Stream input)
        {
            m_input = input;
        }

        /// <summary>
        /// change the output
        /// </summary>
        /// <param name="output">stream of the output</param>
        public void SetOutput(Stream output)
        {
            m_output = output;
        }

        /// <summary>
        /// display the maze on the screen
        /// </summary>
        /// <param name="m">the maze to print to screen</param>
        public void Display(Maze m)
        {
            Maze3d maze3d = m as Maze3d;
            maze3d.print();
        }

        /// <summary>
        /// display the solution to the screen
        /// </summary>
        /// <param name="solution">position of the solution</param>
        public void DisplaySolution(Solution solution)
        {
            string final_solution = "";
            foreach (AState state in solution.GetSolutionPath())
            {
                MazeState m_state = state as MazeState;
                final_solution += m_state.Position.ToString() + "\n"; // add the position to the list
            }
            Output(final_solution);
        }
    }
}
