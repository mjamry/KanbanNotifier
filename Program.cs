using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KanbanNotifier.Core;

namespace KanbanNotifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var appCore = new AppCore();
            appCore.CloseRequested += OnCloseRequested;
            Application.Run(appCore);
        }

        private static void OnCloseRequested(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
