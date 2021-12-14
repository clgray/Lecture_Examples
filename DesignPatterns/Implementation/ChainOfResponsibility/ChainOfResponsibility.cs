namespace Implementation.ChainOfResponsibility
{
	public class ChainOfResponsibility
    {
        public static void Run()
        {
            // Build the chain of responsibility
            Logger logger;
            logger = new ConsoleLogger(LogLevel.All)
                             .SetNext(new EmailLogger(LogLevel.FunctionalMessage | LogLevel.FunctionalError))
                             .SetNext(new FileLogger(LogLevel.Warning | LogLevel.Error));

            // Handled by ConsoleLogger since the console has a loglevel of all
            logger.Message("Entering function ProcessOrder().", LogLevel.Debug);
            logger.Message("Order record retrieved.", LogLevel.Info);

            // Handled by ConsoleLogger and FileLogger since filelogger implements Warning & Error
            logger.Message("Customer Address details missing in Branch DataBase.", LogLevel.Warning);
            logger.Message("Customer Address details missing in Organization DataBase.", LogLevel.Error);

            // Handled by ConsoleLogger and EmailLogger as it implements functional error
            logger.Message("Unable to Process Order ORD1 Dated D1 For Customer C1.", LogLevel.FunctionalError);

            // Handled by ConsoleLogger and EmailLogger
            logger.Message("Order Dispatched.", LogLevel.FunctionalMessage);
        }
    }
}