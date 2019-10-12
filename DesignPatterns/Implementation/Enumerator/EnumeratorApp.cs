using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Enumerator
{
	class EnumeratorApp
	{
		public static void Run()
		{
			var collection = new MyCollection(new []{"1","2","3","4"});

			foreach (var i in collection)
			{
				Console.WriteLine(i);
			}

		}
	}
}
