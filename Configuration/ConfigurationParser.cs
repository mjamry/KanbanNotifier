using System;
using System.Configuration;

namespace KanbanNotifier.Configuration
{
    public class ConfigurationParser : IConfigurationParser
    {
        private const string CONFIG_FILE_NAME = "config.xml";
        private readonly SettingsReader _reader;

        public ConfigurationParser()
        {
            _reader = new SettingsReader(CONFIG_FILE_NAME);
        }

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as Int.
        /// </summary>
        public int GetIntValue(ConfigKeys key)
        {
            string value = _reader.GetValue(key);
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                throw new ConfigurationException(ErrorMessage(typeof(int), key));
            }
        }

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as DateTime.
        /// </summary>
        public DateTime GetDateTimeValue(ConfigKeys key)
        {
            string value = _reader.GetValue(key);
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                throw new ConfigurationException(ErrorMessage(typeof(DateTime), key));
            }
        }

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as TimeSpan.
        /// </summary>
        public TimeSpan GetTimeSpanValue(ConfigKeys key)
        {
            string value = _reader.GetValue(key);
            try
            {
                return TimeSpan.Parse(value);
            }
            catch
            {
                throw new ConfigurationException(ErrorMessage(typeof(TimeSpan), key));
            }
        }

        /// <summary>
        /// Gets file path.
        /// </summary>
        public string GetPath(ConfigKeys key)
        {
            return _reader.GetValue(key);
        }

        /// <summary>
        /// Gets key value as a string.
        /// </summary>
        public string GetKeyValue(ConfigKeys key)
        {
            return _reader.GetValue(key);
        }

        /// <summary>
        /// Gets configuration value for specified key name and try to parse it as long.
        /// </summary>
        public long GetLongValue(ConfigKeys key)
        {
            string value = _reader.GetValue(key);

            try
            {
                return long.Parse(value);
            }
            catch
            {
                throw new ConfigurationException(ErrorMessage(typeof(long), key));
            }
        }

        /// <summary>
        /// Generates error message shown (in exception) when try to read key with invalid value.
        /// </summary>
        private string ErrorMessage(object sender, ConfigKeys key)
        {
            string error = string.Format("Key \"{0}\" does not contain {1} value.", key.ToString(), sender);
            return error;
        }
    }
}
