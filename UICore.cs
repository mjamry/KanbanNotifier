using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KanbanNotifier
{
    public partial class UICore
    {
        private const int BALOON_TIMEOUT = 10000;

        KanbanNotificationForm _mainView;


        public UICore()
        {
            InitialiseComponents();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            _mainView = new KanbanNotificationForm();
            _mainView.Show();
        }

        public void UpdateStatus(List<Activity> update)
        {
            notifyIcon.ShowBalloonTip(BALOON_TIMEOUT, "Status update", update.Count+" items has been changed.", ToolTipIcon.Info);
        }

        private void NotifyBalloon_Click(object sender, EventArgs e)
        {
            ShowForm();
        }
    }
}
