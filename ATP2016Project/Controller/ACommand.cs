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
    abstract class ACommand : ICommand
    {
        protected IModel m_model;
        protected IView m_view;
        /// <summary>
        /// consruvtor of command
        /// </summary>
        /// <param name="model">interface of model</param>
        /// <param name="view">interface of view</param>
        public ACommand(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
        }

        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">paras of the command</param>
        public abstract void DoCommand(params string[] parameters);

        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>the string of the name</returns>
        public abstract string GetName();


    }
}
