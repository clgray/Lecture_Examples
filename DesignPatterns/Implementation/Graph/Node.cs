namespace Implementation.Graph
{
	public class Node
	{
		public int Id { get; set; }
		public Node[] ChildNodes { get; set; }
		public (Node, int)[] ChildNodesWithHeight { get; set; }
	}

}