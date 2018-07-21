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
    class ExitCommand : ACommand
    {
        /// <summary>
        /// exit from the menu
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public ExitCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params of the command</param>
        public override void DoCommand(params string[] parameters)
        {
            m_view.Output("Exit from the program...");
            foreach (Thread t in m_model.getListOfThreads())
            {
                t.Join();
            }
            Thread.Sleep(500);
            System.Environment.Exit(0);
        }

        /// <summary>
        /// the name of the command
        /// </summary>
        /// <returns>the name of the command - exit</returns>
        public override string GetName()
        {
            return "exit";
        }
    }
}
