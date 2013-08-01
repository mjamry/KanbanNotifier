using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KanbanNotifier
{
    public partial class UICore
    {
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
    }
}
