using System;

namespace KanbanNotifier
{
    public class Activity : IEquatable<Activity>
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

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Activity other)
        {
            if(other.Date == Date && other.Author == Author && other.Event == Event && other.Text == Text)
            {
                return true;
            }

            return false;
        }
    }
}
