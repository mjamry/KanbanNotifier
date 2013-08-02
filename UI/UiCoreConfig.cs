namespace KanbanNotifier.UI
{
    public class UiCoreConfig
    {
        public UiCoreConfig(long timeout)
        {
            NotificationBalloonTimeout = timeout;
        }

        public long NotificationBalloonTimeout { get; private set; }
    }
}
