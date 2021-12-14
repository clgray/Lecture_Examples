using System;

namespace Implementation.ChainOfResponsibility
{
	class FileLogger : Logger
	{
		public FileLogger(LogLevel mask)
			: base(mask)
		{ }

		protected override void WriteMessage(string msg)
		{
			// Placeholder for File writing logic
			Console.WriteLine("Writing to Log File: " + msg);
		}
	}
}