namespace Implementation.Visitor
{
	public abstract class LogEntry
	{
		public abstract void Accept(ILogEntryVisitor logEntryVisitor);
	}
}