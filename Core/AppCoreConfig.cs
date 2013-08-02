namespace KanbanNotifier.Core
{
    public class AppCoreConfig
    {
        public AppCoreConfig(long timeout)
        {
            UpdateTimeout = timeout;
        }

        public long UpdateTimeout { get; private set; }
    }
}
