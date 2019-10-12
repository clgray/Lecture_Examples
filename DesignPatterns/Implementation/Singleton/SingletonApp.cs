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
		public void Run()
		{
			var threads = new Thread[10];
			for (int i = 0; i < 10; i++)
			{
				threads[i]= new Thread(Start);
				threads[i].Start();
			}


			for (int i = 0; i < 10; i++)
			{
				threads[i].Join();
			}

			Console.WriteLine(ConfigService.Instance().GetValue("count"));

		}

		private void Start()
		{
			for (int j = 0; j < 1000000; j++)
			{
				lock (this)
				{
					var i = int.Parse(ConfigService.Instance().GetValue("count"));
					i++;
					ConfigService.Instance().SetValue("count", i.ToString());
				}
			}

		}
	}
}
