using ATP2016Project.Model;
using ATP2016Project.Model.Algrothims.Search;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    class SolveMazeCommand : ACommand
    {
        /// <summary>
        /// constructor of the saveCommand
        /// </summary>
        /// <param name="model">interface of model</param>
        /// <param name="view">interface of the view</param>
        public SolveMazeCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params of the command</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                if (parameters.Length != 2)
                {
                    m_view.Output("We expected for 2 parameters!"); // check the params
                    return;
                }
                string name = parameters[0];
                string alg = parameters[1];
                if (checkingIfProper(name, alg) == true)
                {
                    ASearchingAlgorithm new_alg = m_model.checkAlg(alg);
                    m_view.Output("Solve the maze <" + name + "> with the algorithm <" + alg + ">"); // open new Thread
                    m_model.SolveMaze(name, new_alg);
                    m_view.Output("Solution for maze <" + name + "> is ready!"); // succeded
                }
            }
            catch (Exception e)
            {
                m_view.Output("The maze <" + parameters[0] + "> couldn't be solved!");
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>the name of the command</returns>
        public override string GetName()
        {
            return "solvemaze";
        }

        /// <summary>
        /// checking if the input is proper
        /// </summary>
        /// <param name="name">name of the maze</param>
        /// <param name="alg">name of the algorithm</param>
        /// <returns>treu or false if the prams is proper</returns>
        private bool checkingIfProper(string name, string alg)
        {
            bool proper = true;
            if (m_model.getMaze(name) == null)//check if the maze exists
            {
                m_view.Output("The Maze <" + name + "> isnt exist!");
                proper = false;
                return proper;
            }
            if (m_model.checkAlg(alg) == null)//check if the algorithm exists
            {
                m_view.Output("The Algorithm <" + alg + "> isnt exist!");
                proper = false;
                return proper;
            }
            return proper;
        }

    }

}
