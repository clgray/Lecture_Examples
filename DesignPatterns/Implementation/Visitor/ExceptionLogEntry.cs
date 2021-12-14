namespace Implementation.Visitor
{
	public class ExceptionLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// Ѕлагодар€ перегрузке методов выбираетс€ метод Visit(ExceptionLogEntry)
			logEntryVisitor.Visit(this);
		}
	}

	public class CriticalLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// Ѕлагодар€ перегрузке методов выбираетс€ метод Visit(CriticalLogEntry)
			logEntryVisitor.Visit(this);
		}
	}

}