using System;

namespace KanbanNotifier
{
    public class Activity
    {
        public Activity(string author, string text, string history, DateTime time, int taskId)
        {
            Author = author;
            Event = history;
            Text = text;
            Date = time;
            TaskId = taskId;
        }

        public void AssociateTask(Task task)
        {
            Task = task;
        }

        public string Author { get; private set; }
        public string Event { get; private set; }
        public string Text { get; private set; }
        public DateTime Date { get; private set; }
        public int TaskId { get; private set; }
        public Task Task { get; private set; }
    }
}
