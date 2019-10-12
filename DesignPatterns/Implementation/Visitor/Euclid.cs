using System;

namespace Implementation.Visitor
{
	internal class Euclid : IVisitor
	{
		public void Visit(Point2D p)
		{
			p.Metric = Math.Sqrt(p.X * p.X + p.Y * p.Y);
		}

		public void Visit(Point3D p)
		{
			p.Metric = Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
		}
	}


	internal class Lobachevsky : IVisitor
	{
		public void Visit(Point2D p)
		{
			p.Metric = Math.Pow(p.X * p.X + p.Y * p.Y, 2);
		}

		public void Visit(Point3D p)
		{
			p.Metric = Math.Pow(p.X * p.X + p.Y * p.Y + p.Z * p.Z, 2);
		}
	}
	
}