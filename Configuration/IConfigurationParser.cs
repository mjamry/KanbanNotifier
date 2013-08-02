using System;

namespace KanbanNotifier.Configuration
{
    /// <summary>
    /// API used to parse configuration keys.
    /// </summary>
    public interface IConfigurationParser
    {
        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as Int.
        /// </summary>
        int GetIntValue(ConfigKeys key);

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as DateTime.
        /// </summary>
        DateTime GetDateTimeValue(ConfigKeys key);

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as TimeSpan.
        /// </summary>
        TimeSpan GetTimeSpanValue(ConfigKeys key);

        /// <summary>
        /// Gets file path.
        /// </summary>
        string GetPath(ConfigKeys key);

        /// <summary>
        /// Gets key value as a string.
        /// </summary>
        string GetKeyValue(ConfigKeys key);

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as long.
        /// </summary>
        long GetLongValue(ConfigKeys key);
    }
}
