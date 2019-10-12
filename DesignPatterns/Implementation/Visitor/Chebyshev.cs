using System;

namespace Implementation.Visitor
{
	internal class Chebyshev : IVisitor
	{
		public void Visit(Point2D p)
		{
			var ax = Math.Abs(p.X);
			var ay = Math.Abs(p.Y);
			p.Metric = ax > ay ? ax : ay;
		}

		public void Visit(Point3D p)
		{
			var ax = Math.Abs(p.X);
			var ay = Math.Abs(p.Y);
			var az = Math.Abs(p.Z);
			var max = ax > ay ? ax : ay;
			if (max < az) max = az;
			p.Metric = max;
		}
	}
}