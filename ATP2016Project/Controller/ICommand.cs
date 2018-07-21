using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Controller
{
    interface ICommand

    {
        /// <summary>
        /// do the command from the menu
        /// </summary>
        /// <param name="parameters">params of the command</param>
        void DoCommand(params string[] parameters);
        /// <summary>
        /// get the name of the command
        /// </summary>
        /// <returns>name of the command</returns>
        string GetName();

    }
}
