using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KanbanNotifier
{
    public class AppCore : ApplicationContext
    {
        private const int UPDATE_TIMEOUT = 300000;

        UICore _uiCore;
        Timer _statusUpdateTimer = new Timer();
        KanbanizeApiWrapper wrapper = new KanbanizeApiWrapper();

        public AppCore()
        {
            _uiCore = new UICore();
            _statusUpdateTimer.Interval = UPDATE_TIMEOUT;
            _statusUpdateTimer.Tick += OnTimerTick;
            
            _statusUpdateTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            List<Activity> update = wrapper.GetActivities();
            if(update.Count > 0)
            {
                _uiCore.UpdateStatus(update);
            }
        }
    }
}
