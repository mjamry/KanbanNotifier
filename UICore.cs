using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace KanbanNotifier
{
    public partial class UICore
    {
        private const int BALOON_TIMEOUT = 30000;

        private KanbanNotificationForm _mainView;
        public event EventHandler DataUpdateRequested;


        public UICore()
        {
            InitialiseComponents();
            
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            RequestDataUpdate();
        }

        private void RequestDataUpdate()
        {
            var temp = DataUpdateRequested;
            if(temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public void ShowForm(List<Activity> activities)
        {
            _mainView = new KanbanNotificationForm();
            if (_mainView.InvokeRequired)
            {
            }
            _mainView.Show(activities);
        }

        public void UpdateStatus(int itemsChanged)
        {
            notifyIcon.ShowBalloonTip(BALOON_TIMEOUT, "Status update", itemsChanged + " items has been changed.", ToolTipIcon.Info);
        }

        private void NotifyBalloon_Click(object sender, EventArgs e)
        {
            RequestDataUpdate();
        }
    }
}
