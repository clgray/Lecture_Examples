using System;

namespace Implementation.ChainOfResponsibility
{
	public class ConsoleLogger : Logger
	{
		public ConsoleLogger(LogLevel mask)
			: base(mask)
		{ }

		protected override void WriteMessage(string msg)
		{
			Console.WriteLine("Writing to console: " + msg);
		}
	}
}