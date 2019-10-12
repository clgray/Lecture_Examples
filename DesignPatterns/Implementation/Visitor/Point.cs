namespace Implementation.Visitor
{
	internal abstract class Point
	{
		public double Metric { get; set; } = -1;
		public abstract void Accept(IVisitor visitor);
	}
}