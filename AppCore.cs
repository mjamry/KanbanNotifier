using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KanbanNotifier
{
    public class AppCore : ApplicationContext
    {
        UICore _uiCore;
        Timer _statusUpdateTimer = new Timer();
        KanbanizeApiWrapper wrapper = new KanbanizeApiWrapper();


        public AppCore()
        {
            _uiCore = new UICore();
            _statusUpdateTimer.Interval = 300000;
            _statusUpdateTimer.Tick += OnTimerTick;
            
            _statusUpdateTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            
        }
    }
}
