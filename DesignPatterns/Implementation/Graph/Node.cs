using System;

namespace Implementation.Graph
{
	public class Node
	{
		public int Id { get; set; }
		public Node[] ChildNodes { get; set; }
		public (Node, int)[] ChildNodesWithHeight { get; set; }
		public override bool Equals(object obj)
		{
			return Equals((Node)obj);
		}

		private bool Equals(Node other)
		{
			return Id == other.Id;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}

}