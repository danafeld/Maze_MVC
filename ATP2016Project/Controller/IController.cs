using ATP2016Project.Model;
using ATP2016Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    interface IController
    {
        /// <summary>
        /// get the dictionary of commands
        /// </summary>
        /// <returns>dictionary of commands</returns>
        Dictionary<string, ICommand> GetCommands();
        /// <summary>
        /// change the interface of the model
        /// </summary>
        /// <param name="model">get  interface of the model</param>
        void SetModel(IModel model);
        /// <summary>
        /// change the interface pf the view
        /// </summary>
        /// <param name="view">get  interface of the view</param>
        void SetView(IView view);
        /// <summary>
        /// print the controller
        /// </summary>
        /// <param name="output">print the string from the controller</param>
        void Output(string output);
    }
}
