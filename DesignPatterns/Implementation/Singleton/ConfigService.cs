using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Singleton
{
	class ConfigService
	{
		private static ConfigService _configService;
		private Dictionary<string, string> _config= new Dictionary<string, string>();
		private static object syncObj = new object();
		private static object syncOperation = new object();


		private ConfigService()
		{
			
		}
		public static ConfigService Instance()
		{
			if (_configService == null)
			lock (syncObj)
			{
				if (_configService == null)
				{
					_configService = new ConfigService();
				}
			}
			return _configService;
		}

		public string GetValue(string key)
		{

			lock (syncOperation)
			{
				if (!_config.ContainsKey(key))
				{
					_config[key] = "0";
				}
				return _config[key];
			}
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
