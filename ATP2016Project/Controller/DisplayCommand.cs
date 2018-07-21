using ATP2016Project.Model;
using ATP2016Project.Model.Algrothims.MazeGenerators;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class DisplayCommand : ACommand
    {
        /// <summary>
        /// constructor for the display command
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public DisplayCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command with the params
        /// </summary>
        /// <param name="parameters">params for the command</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                if (parameters.Length != 1)
                {
                    m_view.Output("We expected for 1 parameter!"); // we need 1 parameters for this command
                    return;
                }
                string name = parameters[0];
                m_view.Output("Display the maze <" + name + ">");
                if (m_model.getMaze(name) != null)
                {
                    Maze maze = m_model.getMaze(name); // go to model and get maze
                    m_view.Display(maze); // go to view and display the maze
                }
            }
            catch (Exception e)
            {
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// the name of the command - display
        /// </summary>
        /// <returns>nme of the command</returns>
        public override string GetName()
        {
            return "display";
        }
    }
}
