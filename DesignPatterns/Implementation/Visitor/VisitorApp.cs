using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Visitor
{
	class VisitorApp
	{
		public static void Run()
		{
			Point p = new Point2D(1, 2);
			//IVisitor v = new Euclid();
			IVisitor v = new Lobachevsky();
			p.Accept(v);
			Console.WriteLine(p.Metric);
		}
	}
}
