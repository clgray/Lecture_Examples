using System;
using System.Collections.Generic;

namespace Implementation.Graph
{
	public class GraphApp
	{
		public static void Run()
		{
			var graf = new List<Node>();
			for (int i = 0; i < 12; i++)
			{
				graf.Add(new Node { Id = i, ChildNodes = new Node[0]});
			}
			graf[0].ChildNodes = new[] { graf[2], graf[3] };
			graf[2].ChildNodes = new[] { graf[10] };
			graf[3].ChildNodes = new[] { graf[4], graf[5] };
			graf[5].ChildNodes = new[] { graf[6], graf[7], graf[8] };
			graf[7].ChildNodes = new[] { graf[9] };
			graf[10].ChildNodes = new[] { graf[1], graf[11] };
			graf[9].ChildNodes = new[] { graf[11] };
			graf[8].ChildNodes = new[] { graf[0] };
			graf[6].ChildNodes = new[] { graf[4] };

			
			var g = new DfsGraph(graf.ToArray());
			g.DfsStackStart(graf[0]);
			//foreach (Node item in g)
			//{
			//	Console.WriteLine(item.Id);
			//}

		}
	}
}
