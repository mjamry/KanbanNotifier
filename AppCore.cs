using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using KanbanNotifier.KanbanizeApi;
using Timer = System.Windows.Forms.Timer;

namespace KanbanNotifier
{
    public class AppCore : ApplicationContext
    {
        private const int UPDATE_TIMEOUT = 30000;
        private List<Activity> _activitiesCache = new List<Activity>();
        UICore _uiCore;
        Timer _statusUpdateTimer = new Timer();
        KanbanizeApiWrapper wrapper = new KanbanizeApiWrapper();
        private List<Activity> _lastActivities = new List<Activity>();

        public event EventHandler CloseRequested;

        public AppCore()
        {
            _uiCore = new UICore();

            _uiCore.DataUpdateRequested += OnDataUpdateRequested;
            _uiCore.CloseRequest += OnCloseRequested;
            _activitiesCache = wrapper.GetActivities();

            _statusUpdateTimer.Interval = UPDATE_TIMEOUT;
            _statusUpdateTimer.Tick += OnTimerTick;
            
            _statusUpdateTimer.Start();
        }

        private void OnCloseRequested(object sender, EventArgs e)
        {
            var temp = CloseRequested;
            if(temp != null)
            {
                temp(sender, e);
            }
        }

        private void OnDataUpdateRequested(object sender, EventArgs e)
        {
            _uiCore.ShowForm(_lastActivities);
            _lastActivities.Clear();
        }

        /// <summary>
        /// Fired every time timer reaches its timeout.
        /// </summary>
        private void OnTimerTick(object sender, EventArgs e)
        {
            Thread thread = new Thread(GetUpdate);
            thread.Start();
        }

        private void GetUpdate()
        {
            List<Activity> update = wrapper.GetActivities();
            CheckForChanges(update);
        }

        /// <summary>
        /// Checks for changes in items status.
        /// </summary>
        /// <param name="update"></param>
        private void CheckForChanges(List<Activity> update)
        {
            List<Activity> diffList = new List<Activity>();
            foreach (var activity in update)
            {
                if (!_activitiesCache.Contains(activity))
                {
                    diffList.Add(activity);
                }
            }

            if(diffList.Any())
            {
                _activitiesCache = update;
                _lastActivities.AddRange(diffList);
                OnChangesFound(_lastActivities.Count);
            }
        }

        /// <summary>
        /// Fires UI update when changes has been found.
        /// </summary>
        private void OnChangesFound(int numberOfNewUpdates)
        {
            _uiCore.UpdateStatus(numberOfNewUpdates);
        }
    }
}
