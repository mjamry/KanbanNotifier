using System.Drawing;

namespace KanbanNotifier
{
    public class Task
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
    }
}
