using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class MazeSizeCommand : ACommand
    {
        /// <summary>
        /// constructor of the maze size
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public MazeSizeCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params for the command</param>
        public override void DoCommand(params string[] parameters)
        {
            string name = parameters[0];
            try
            {
                if (parameters.Length != 1)
                {
                    m_view.Output("We expected for 1 parameter!"); // check if the params is proper
                    return;
                }
                if (m_model.getMaze(name) == null) // check for the name
                {
                    m_view.Output("The maze <" + name + "> doesn't exist!");
                }
                else
                {
                    int size_m = m_model.MazeSize(name); // go to model and get the maze size
                    m_view.Output("The size of the maze <" + name + "> is " + size_m + " bytes");
                }
            }
            catch (Exception e)
            {

                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>get the name - maze size</returns>
        public override string GetName()
        {
            return "mazesize";
        }
    }
}
