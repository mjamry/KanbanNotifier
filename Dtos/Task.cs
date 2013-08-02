using System;
using System.Drawing;

namespace KanbanNotifier
{
    public class Task : IEquatable<Task>
    {
        public Task(int id, string title, string description, string assignee, Color color, string priority, string link)
        {
            ID = id;
            Title = title;
            Description = description;
            Assignee = assignee;
            Color = color;
            Priority = priority;
            Link = link;
        }

        public static Task Empty
        {
            get { return new Task(0, string.Empty, string.Empty, string.Empty, Color.White, string.Empty, string.Empty); }
        }

        public int ID { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Assignee { get; private set; }
        public Color Color { get; private set; }
        public string Priority { get; private set; }
        public string Link { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Task other)
        {
            if (other.ID == ID && other.Title == Title && other.Description == Description && other.Assignee == Assignee && other.Color == Color && other.Priority == Priority && other.Link == Link)
            {
                return true;
            }

            return false;
        }
    }
}
