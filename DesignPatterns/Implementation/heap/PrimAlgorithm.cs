using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Transactions;
using Implementation.Graph;

namespace Implementation.heap
{
	public class PrimAlgorithm
	{
		public static void Run()
		{
			var graf = new List<Node>();
			for (int i = 0; i < 7; i++)
			{
				graf.Add(new Node { Id = i });
			}
			graf[0].ChildNodesWithHeight = new[] { (graf[1], 7), (graf[3], 5) };
			graf[1].ChildNodesWithHeight = new[] { (graf[0], 7), (graf[2], 8), (graf[4], 7) };
			graf[2].ChildNodesWithHeight = new[] { (graf[1], 8), (graf[4], 5) };
			graf[3].ChildNodesWithHeight = new[] { (graf[0], 5), (graf[1], 9), (graf[4], 15), (graf[5], 6) };
			graf[4].ChildNodesWithHeight = new[] { (graf[2], 5), (graf[1], 7), (graf[3], 15), (graf[4], 8), (graf[6], 9) };
			graf[5].ChildNodesWithHeight = new[] { (graf[3], 6), (graf[4], 8), (graf[6], 11) };
			graf[6].ChildNodesWithHeight = new[] { (graf[4], 9), (graf[5], 11) };

			var currentNode = graf[3];
			List<Arc> arcs= new List<Arc>();
			HashSet<Node> visitedNodes = new HashSet<Node>();
			visitedNodes.Add(graf[3]);
			var result = new List<Arc>();
			AddArcs(currentNode, arcs, result);

			while (visitedNodes.Count != graf.Count)
			{
				var minArc = arcs.Min(x  => x);
				arcs.Remove(minArc);
				if (!visitedNodes.Contains(minArc.NodeB))
				{
					visitedNodes.Add(minArc.NodeB);
					AddArcs(minArc.NodeB, arcs, result);
					result.Add(minArc);
					continue;
				}

				if (!visitedNodes.Contains(minArc.NodeA))
				{
					visitedNodes.Add(minArc.NodeA);
					AddArcs(minArc.NodeA, arcs, result);
					result.Add(minArc);
					continue;
				}
			}
			


		}

		private static void AddArcs(Node currentNode, List<Arc> arcs, List<Arc> result)
		{
			foreach (var item in currentNode.ChildNodesWithHeight)
			{
				var newArc = new Arc() {NodeA = currentNode, NodeB = item.Item1, Height = item.Item2};
				if (!arcs.Contains(newArc)&& !result.Contains(newArc))
				{ arcs.Add(newArc);}
			}
		}

		class Arc:IComparable
		{
			public Node NodeA { get; set; }
			public Node NodeB { get; set; }
			public int Height { get; set; }

			public override bool Equals(object obj)
			{
				var arcObj =  (Arc)(obj);

				return ((arcObj.NodeB.Id == NodeB.Id) && (arcObj.NodeA.Id == NodeA.Id) ||
				        (arcObj.NodeB.Id == NodeA.Id) && (arcObj.NodeA.Id == NodeB.Id)) && arcObj.Height == Height;

			}

			public override int GetHashCode()
			{
				return Height;
			}

			public int CompareTo(object obj)
			{
				var arcObj = (Arc)(obj);
				if (arcObj.Height == Height) return 0;
				if (arcObj.Height < Height)
				{ return 1;}

				return -1;

			}
		}
	}
}