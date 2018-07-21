using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class LoadMazeCommand : ACommand
    {
        /// <summary>
        /// constructor of load the maze
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public LoadMazeCommand(IModel model, IView view) : base(model, view)
        {

        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params for the command</param>
        public override void DoCommand(params string[] parameters)
        {
            string path = parameters[0];
            string maze_name = parameters[1];
            try
            {
                if (parameters.Length != 2)
                {
                    m_view.Output("We expected for 2 parameters!"); // check if we get 2 params
                    return;
                }
                if (File.Exists(path + maze_name) == false)
                {
                    m_view.Output("The path <" + path + maze_name + "> doesn't exist!"); // check if the file exists
                    return;
                }
                else
                {
                    m_model.LoadMazeFromDisk(path, maze_name); // get the maze from the disk
                    m_view.Output("Dictionary named <" + maze_name + "> Loaded! from the path<" + path + ">");
                }
            }
            catch (Exception e)
            {
                m_view.Output("Dictionary <" + maze_name + "> couldn't loaded");
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>name of the command</returns>
        public override string GetName()
        {
            return "loadmaze";
        }
    }
}
