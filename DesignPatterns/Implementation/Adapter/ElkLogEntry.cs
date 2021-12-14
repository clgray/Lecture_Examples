using System;

namespace Implementation.Adapter
{
	public class ElkLogEntry
	{
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public string Severity { get; set; }
	}
}