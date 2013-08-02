using KanbanNotifier.Core;
using KanbanNotifier.KanbanizeApi;
using KanbanNotifier.UI;

namespace KanbanNotifier.Configuration
{
    public class ConfigurationFactory
    {
        private IConfigurationParser _parser;

        public ConfigurationFactory(IConfigurationParser parser)
        {
            _parser = parser;
        }

        public KanbanApiConfig CreateApiConfig()
        {
            var url = _parser.GetKeyValue(ConfigKeys.ApiUrl);
            var key = _parser.GetKeyValue(ConfigKeys.ApiKey);
            var boardId = _parser.GetIntValue(ConfigKeys.BoardId);
            var resultsPerPage = _parser.GetIntValue(ConfigKeys.ResultsPerPage);

            return new KanbanApiConfig(url, key, boardId, resultsPerPage);
        }
        
        public AppCoreConfig CreateAppCoreConfig()
        {
            var timeout = _parser.GetLongValue(ConfigKeys.UpdateTimeoutInMs);

            return new AppCoreConfig(timeout);
        }

        public UiCoreConfig CreateUiCoreConfig()
        {
            var timeout = _parser.GetIntValue(ConfigKeys.NotificationBalloonTimeout);

            return new UiCoreConfig(timeout);
        }
    }
}
