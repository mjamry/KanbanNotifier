namespace KanbanNotifier.UI
{
    public class UiCoreConfig
    {
        public UiCoreConfig(int timeout)
        {
            NotificationBalloonTimeout = timeout;
        }

        public int NotificationBalloonTimeout { get; private set; }
    }
}
