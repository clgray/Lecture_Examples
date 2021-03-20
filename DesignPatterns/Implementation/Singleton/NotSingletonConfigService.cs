using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Implementation.Singleton
{
	public class NotSingletonConfigService:IConfigService
	{
		private Dictionary<string, string> _config = new Dictionary<string, string>();
		private static object syncOperation = new object();

		public string GetValue(string key)
		{
			lock (syncOperation)
			{
				if (!_config.ContainsKey(key))
				{
					_config[key] = "0";
				}
			}

			return _config[key];
		}

		public void SetValue(string key, string value)
		{
			lock (syncOperation)
			{
				_config[key] = value;
			}
		}
	}
}