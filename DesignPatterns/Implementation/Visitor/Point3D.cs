namespace Implementation.Visitor
{
	internal class Point3D : Point
	{
		public Point3D(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public double X { get; }
		public double Y { get; }
		public double Z { get; }

		public override void Accept(IVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}