namespace Implementation.ChainOfResponsibility
{
	/// <summary>
	/// Abstract Handler in chain of responsibility pattern.
	/// </summary>
	public abstract class Logger
	{
		protected LogLevel logMask;

		// The next Handler in the chain
		protected Logger next;

		public Logger(LogLevel mask)
		{
			this.logMask = mask;
		}

		/// <summary>
		/// Sets the Next logger to make a list/chain of Handlers.
		/// </summary>
		public Logger SetNext(Logger nextlogger)
		{
			Logger lastLogger = this;

			while (lastLogger.next != null)
			{
				lastLogger = lastLogger.next;
			}

			lastLogger.next = nextlogger;
			return this;
		}

		public void Message(string msg, LogLevel severity)
		{
			if ((severity & logMask) != 0) //True only if any of the logMask bits are set in severity
			{
				WriteMessage(msg);
			}
			if (next != null)
			{
				next.Message(msg, severity);
			}
		}

		abstract protected void WriteMessage(string msg);
	}
}