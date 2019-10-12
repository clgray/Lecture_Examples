namespace Implementation.Visitor
{
	public class SimpleLogEntry : LogEntry
	{
		public override void Accept(ILogEntryVisitor logEntryVisitor)
		{
			// ��������� ���������� ������� ���������� ����� Visit(ExceptionLogEntry)
			logEntryVisitor.Visit(this);
		}
	}
}