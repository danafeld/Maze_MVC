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
    class DirCommand : ACommand
    {
        /// <summary>
        /// coomand of the dir
        /// </summary>
        /// <param name="model">name of the model</param>
        /// <param name="view">name of the view</param>
        public DirCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do the command eith the params
        /// </summary>
        /// <param name="parameters">params of the command</param>
        public override void DoCommand(params string[] parameters)
        {
            string path = parameters[0];
            try
            {
                if (parameters.Length != 1)
                {
                    m_view.Output("We expected for 1 parameter!"); // we need 1 param for this command
                    return;
                }
                if (Directory.Exists(path) == true)
                {
                    m_view.Output("Preview the dir <" + path + ">");
                    string dirs = m_model.GetDir(path); // go for the model command
                    m_view.Output(dirs);
                }
                else
                {
                    m_view.Output("The directory <" + path + "> doesn't exist"); // print the directory doesnt exist
                }

            }
            catch (Exception e)
            {
                m_view.Output(e.Message);
            }
        }

        /// <summary>
        /// get the name of the command dir
        /// </summary>
        /// <returns>the name dir command</returns>
        public override string GetName()
        {
            return "dir";
        }
    }
}
