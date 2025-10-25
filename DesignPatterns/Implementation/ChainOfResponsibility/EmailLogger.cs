using System;

namespace Implementation.ChainOfResponsibility
{
	public class EmailLogger : Logger
	{
		public EmailLogger(LogLevel mask)
			: base(mask)
		{ }

		protected override void WriteMessage(string msg, LogLevel severity)
		{
			// Placeholder for mail send logic, usually the email configurations are saved in config file.
			Console.WriteLine($"Sending via email: {severity} - {msg}");
		}
	}
}