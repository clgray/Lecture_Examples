namespace Implementation.Visitor
{
	public abstract class LogEntry
	{
		public string Message { get; set; }
		public abstract void Accept(ILogEntryVisitor logEntryVisitor);
	}
}