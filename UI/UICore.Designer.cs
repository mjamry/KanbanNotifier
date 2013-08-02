using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KanbanNotifier.UI
{
    public partial class UICore 
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;

        private void InitialiseComponents()
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.menu = new System.Windows.Forms.ContextMenuStrip();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem("Close");

            this.menu.Items.Add(closeMenuItem);


            this.notifyIcon.Icon = ((System.Drawing.Icon)(Resource.notifyIcon));
            this.notifyIcon.Text = "KanbanNotifier";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            this.notifyIcon.BalloonTipClicked += NotifyBalloon_Click;

            this.notifyIcon.ContextMenuStrip = menu;
            this.closeMenuItem.Click += CloseMenuItem_Click;
        }
    }
}
