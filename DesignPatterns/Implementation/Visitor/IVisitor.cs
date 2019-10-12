namespace Implementation.Visitor
{
	internal interface IVisitor
	{
		void Visit(Point2D p);
		void Visit(Point3D p);
	}
}