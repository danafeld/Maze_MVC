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
    class SaveMazeCommand : ACommand
    {
        /// <summary>
        /// constructor of the saveMaze
        /// </summary>
        /// <param name="model">interface of model</param>
        /// <param name="view">interface of view</param>
        public SaveMazeCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params for the command</param>
        public override void DoCommand(params string[] parameters)
        {
            string name = parameters[0];
            string filePath = parameters[1];
            try
            {
                if (parameters.Length != 2)
                {
                    m_view.Output("We expected for 2 parameters!"); // check the params
                    return;
                }
                if (m_model.getMaze(name) == null)
                {
                    m_view.Output("The name maze <" + name + "> doesn't exist!");// check for proper
                }
                else
                {
                    if (Directory.Exists(filePath) == false)
                        m_view.Output("The filePath <" + filePath + "> doesn't exist!");
                    else
                    {
                        m_model.SaveMazeToDisk(name, filePath); // go to model and save the maze
                        m_view.Output("The Maze " + name + " In filePath <" + filePath + "> saved succesfully!");
                    }
                }
            }
            catch (Exception e)
            {
                m_view.Output("The maze <" + name + "> couldnt saved!");
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>the name of the command</returns>
        public override string GetName()
        {
            return "savemaze";
        }
    }
}
