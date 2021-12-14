using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Implementation.Singleton
{
	public class SingletonApp
	{
		private readonly IConfigService _configService;

		public SingletonApp(IConfigService configService)
		{
			_configService = configService;
		}

		public string Run()
		{
			var threadsCount = 10; 
			var threads = new Thread[threadsCount];
			for (int i = 0; i < threadsCount; i++)
			{
				threads[i]= new Thread(Start);
				threads[i].Start();
			}

			for (int i = 0; i < threadsCount; i++)
			{
				threads[i].Join();
			}

			Console.WriteLine(_configService.GetValue("count"));
			return _configService.GetValue("count");
		}

		private void Start()
		{
			for (int j = 0; j < 1000000; j++)
			{
				lock (this)
				{
					var i = int.Parse(_configService.GetValue("count"));
					i++;
					_configService.SetValue("count", i.ToString());
				}
			}

		}
	}
}
