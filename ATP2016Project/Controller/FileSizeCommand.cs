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
    class FileSizeCommand : ACommand
    {
        /// <summary>
        /// constructor of the file size command
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public FileSizeCommand(IModel model, IView view) : base(model, view)
        {
        }

        /// <summary>
        /// do command from the menu
        /// </summary>
        /// <param name="parameters">params of the command</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                string filePath = parameters[0];
                if (parameters.Length != 1)
                {
                    m_view.Output("We expected for 1 parameter!"); // we need one param for this command!
                    return;
                }
                if (File.Exists(filePath) == false)
                {
                    m_view.Output("The path <" + filePath + "> doesn't exist!"); // the path didnt exist
                }
                else
                {
                    long size_f = m_model.FileSize(filePath); // go to model and get the size of file
                    m_view.Output("The size of the file <" + filePath + "> is " + size_f + " bytes");
                }
            }
            catch (Exception e)
            {
                m_view.Output(e.Message);
            }
        }
        /// <summary>
        /// get the name of the command - filesize
        /// </summary>
        /// <returns>the name of the command - filesize</returns>
        public override string GetName()
        {
            return "filesize";
        }
    }
}
