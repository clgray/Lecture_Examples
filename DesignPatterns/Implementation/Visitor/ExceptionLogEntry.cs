namespace Implementation.Visitor
{
	public class ExceptionLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// ��������� ���������� ������� ���������� ����� Visit(ExceptionLogEntry)
			logEntryVisitor.Visit(this);
		}
	}

	public class CriticalLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// ��������� ���������� ������� ���������� ����� Visit(ExceptionLogEntry)
			logEntryVisitor.Visit(this);
		}
	}
}