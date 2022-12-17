using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Visitor
{
	class VisitorApp
	{
		public static void Run()
		{
			//Point p2D = new Point2D(1, 2);
			//Point p3D = new Point3D(1, 2, 3);
			////IVisitor v = new Euclid();
			//IVisitor v = new Chebyshev();
			//p2D.Accept(v);
			//Console.WriteLine(p2D.Metric);
			//p3D.Accept(v);
			//Console.WriteLine(p3D.Metric);


			LogEntry[] logEntry = {
				new ExceptionLogEntry {Message = "Ошибка случилась"},
				new CriticalLogEntry {Message = "Критическая ошибка"},
				new SimpleLogEntry {Message = "Всё хорошо. работаем"}
			};

			var logSaver = new KibanaLogSaver();
			foreach (var entry in logEntry)
			{
				entry.Accept(logSaver);
				//logSaver.SaveLogEntry(entry);
			}

		}
	}
}
