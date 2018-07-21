using ATP2016Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Project.View;

namespace ATP2016Project.Controller
{
    class MyController : IController
    {

        private IModel m_model;
        private IView m_view;

        /// <summary>
        /// constructor of my controller
        /// </summary>
        public MyController()
        {

        }

        /// <summary>
        /// constructor of myController
        /// </summary>
        /// <param name="model">interface of the model</param>
        /// <param name="view">interface of the view</param>
        public MyController(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
            m_view.SetCommands(GetCommands());
        }

        /// <summary>
        /// get the dictionary of commands
        /// </summary>
        /// <returns>get the dictionary of the commands</returns>
        public Dictionary<string, ICommand> GetCommands()
        {
            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
            ACommand dir = new DirCommand(m_model, m_view);
            ACommand gemeratemaze3d = new Generate3dMazeCommand(m_model, m_view);
            ACommand display = new DisplayCommand(m_model, m_view);
            ACommand savemaze = new SaveMazeCommand(m_model, m_view);
            ACommand loadmaze = new LoadMazeCommand(m_model, m_view);
            ACommand mazesize = new MazeSizeCommand(m_model, m_view);
            ACommand filesize = new FileSizeCommand(m_model, m_view);
            ACommand solvemaze = new SolveMazeCommand(m_model, m_view);
            ACommand displaysolution = new DisplaySolutionCommand(m_model, m_view);
            ACommand exit = new ExitCommand(m_model, m_view);
            commands.Add(dir.GetName(), dir);
            commands.Add(gemeratemaze3d.GetName(), gemeratemaze3d);
            commands.Add(display.GetName(), display);
            commands.Add(savemaze.GetName(), savemaze);
            commands.Add(loadmaze.GetName(), loadmaze);
            commands.Add(mazesize.GetName(), mazesize);
            commands.Add(filesize.GetName(), filesize);
            commands.Add(solvemaze.GetName(), solvemaze);
            commands.Add(displaysolution.GetName(), displaysolution);
            commands.Add(exit.GetName(), exit);
            return commands;
        }

        /// <summary>
        /// print the string of the output
        /// </summary>
        /// <param name="output">print the string</param>

        public void Output(string output)
        {
            m_view.Output(output);
        }

        /// <summary>
        /// change the model
        /// </summary>
        /// <param name="model">interface of the model</param>
        public void SetModel(IModel model)
        {

            m_model = model;
        }

        /// <summary>
        /// change the view
        /// </summary>
        /// <param name="view">interface of view</param>
        public void SetView(IView view)
        {
            m_view = view;
        }

    }
}
