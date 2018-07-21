using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class Generate3dMazeCommand : ACommand
    {
        /// <summary>
        /// constructor od the ganarate 3d maze
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public Generate3dMazeCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params for the command</param>
        public override void DoCommand(params string[] parameters)
        {
            int x, y, z; string name;
            try
            {
                if (parameters.Length != 4)
                {
                    m_view.Output("We expected for 4 parameters!"); // we need 4 paramters!
                    return;
                }
                name = parameters[0];
                x = Convert.ToInt32(parameters[1]);
                y = Convert.ToInt32(parameters[2]);
                z = Convert.ToInt32(parameters[3]);
                if (x < 0 || y < 0 || z < 0)
                {
                    m_view.Output("The numbers need to be positive!"); // check for the numbers if positive
                    return;
                }
                m_view.Output("starting generate the maze <" + name + ">...");
                m_model.GenerateMaze3d(name, x, y, z); // go to the model and do the generate
                m_view.Output("The maze <" + name + "> is ready!");
            }
            catch (Exception e)
            {
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command - generatemaze3d
        /// </summary>
        /// <returns>name of the command - generatemaze3d</returns>
        public override string GetName()
        {
            return "generate3dmaze";
        }
    }
}
