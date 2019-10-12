namespace Implementation.Visitor
{
	public class SimpleLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// Ѕлагодар€ перегрузке методов выбираетс€ метод Visit(ExceptionLogEntry)
			logEntryVisitor.Visit(this);
		}
	}
}