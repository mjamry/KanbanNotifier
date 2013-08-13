using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using KanbanNotifier.Configuration;
using KanbanNotifier.KanbanizeApi;
using KanbanNotifier.UI;

namespace KanbanNotifier.Core
{
    public class AppCore : ApplicationContext
    {
        private List<Activity> _activitiesCache = new List<Activity>();
        private List<Activity> _lastActivities = new List<Activity>();

        private readonly ConfigurationFactory _configFactory;
        private readonly UICore _uiCore;
        private readonly System.Windows.Forms.Timer _statusUpdateTimer;
        private readonly KanbanizeApiWrapper _kanbanizeApi;

        public event EventHandler CloseRequested;

        public AppCore()
        {
            _configFactory = new ConfigurationFactory(new ConfigurationParser());
            _statusUpdateTimer = new System.Windows.Forms.Timer();
            _kanbanizeApi = new KanbanizeApiWrapper(_configFactory.CreateApiConfig());
            _uiCore = new UICore(_configFactory.CreateUiCoreConfig());

            _uiCore.DataUpdateRequested += OnDataUpdateRequested;
            _uiCore.CloseRequest += OnCloseRequested;
            
            GetUpdate();

            //TODO - possibility to set higher interval. temporarly it is parsed as int.
            _statusUpdateTimer.Interval = (int)_configFactory.CreateAppCoreConfig().UpdateTimeout;
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
            List<Activity> update = _kanbanizeApi.GetActivities();
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
