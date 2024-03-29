﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KanbanNotifier.UI
{
    public partial class KanbanNotificationForm : Form
    {
        public KanbanNotificationForm()
        {
            InitializeComponent();
            this.Hide();
            this.Visible = false;
        }

        public new void Show(List<Activity> activities)
        {
            base.Show();
            this.WindowState = FormWindowState.Normal;
            FillWithData(activities);
        }

        public new void Hide()
        {
            base.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        public new void Close()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        private void FillWithData(List<Activity> activities)
        {
            foreach (var activity in activities)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridView);
                row.SetValues(activity.TaskId, activity.Task.Title, activity.Event, activity.EventDescription, activity.Date, activity.Task.Assignee, activity.Task.Link, activity.Task.Priority, activity.Task.Description);
                row.DefaultCellStyle.BackColor = activity.Task.Color;
                gridView.Rows.Add(row);
            }
        }

    }
}
