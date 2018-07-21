using ATP2016Project.Model;
using ATP2016Project.Model.Algrothims.Search;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class DisplaySolutionCommand : ACommand
    {
        /// <summary>
        /// constructor of the solution command
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public DisplaySolutionCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from th menu
        /// </summary>
        /// <param name="parameters">prams for this command</param>
        public override void DoCommand(params string[] parameters)
        {
            string name = parameters[0];

            try
            {
                if (parameters.Length != 1)
                {
                    m_view.Output("We expected for 1 parameter!"); // we need 1 parameter for this command
                    return;
                }
                if (m_model.getSolution(name) != null)
                {
                    m_view.Output("Display the maze <" + name + ">");
                    Solution s = m_model.getSolution(name); // go to model and get the solution
                    m_view.DisplaySolution(s); // go to view and print the solution
                }
                else
                {
                    m_view.Output("The maze <" + name + "> doesn't exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// get the name of the display solution
        /// </summary>
        /// <returns>get the name of the command</returns>
        public override string GetName()
        {
            return "displaysolution";
        }
    }
}
