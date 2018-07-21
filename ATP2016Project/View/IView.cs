using ATP2016Project.Controller;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using ATP2016Project.Model.Algrothims.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.View
{
    interface IView
    {
        /// <summary>
        /// start the view with menu and commends
        /// </summary>
        void Start();
        /// <summary>
        /// print to the screen, preview in the screen
        /// </summary>
        /// <param name="output">get the parm to print</param>
        void Output(string output);
        /// <summary>
        /// set the commands
        /// </summary>
        /// <param name="commands">dictionary of commands</param>
        void SetCommands(Dictionary<string, ICommand> commands);
        /// <summary>
        /// get the params from the user
        /// </summary>
        /// <returns>return the string pram</returns>
        string Input();
        /// <summary>
        /// function of view - display to the screen
        /// </summary>
        /// <param name="m">get the maze to display on the screen</param>
        void Display(Maze m);
        /// <summary>
        /// display the solution of the maze in the screen
        /// </summary>
        /// <param name="solution">get the position of the solution</param>
        void DisplaySolution(Solution solution);
    }
}
