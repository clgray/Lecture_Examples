namespace Implementation.Visitor
{
	internal class Point2D : Point
	{
		public Point2D(double x, double y)
		{
			X = x;
			Y = y;
		}


		public double X { get; }
		public double Y { get; }

		public override void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}