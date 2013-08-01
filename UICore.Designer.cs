using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KanbanNotifier
{
    public partial class UICore 
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;

        private void InitialiseComponents()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();

            this.notifyIcon.Icon = ((System.Drawing.Icon)(Resource.notifyIcon));
            this.notifyIcon.Text = "KanbanNotifier";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            this.notifyIcon.BalloonTipClicked += NotifyBalloon_Click;
        }
    }
}
