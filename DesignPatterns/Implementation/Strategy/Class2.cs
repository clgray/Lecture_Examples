using System;
using System.Collections.Generic;

namespace Implementation.Strategy
{
	class EmployeeRunner
	{
		public void Run()
		{
			list.Add(new Employee { Id = 3, Name = "Вася"});
			list.Add(new Employee { Id = 2, Name = "Петя" });
			list.Add(new Employee { Id = 1, Name = "Маша" });
			list.Sort(new EmployeeByIdComparer());
			foreach (var employee in list)
			{
				Console.WriteLine($"{employee.Name} - {employee.Id}");
			}
		}

		private List<Employee> list = new List<Employee>();
	}

	class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public override string ToString()
		{
			return string.Format("Id = {0}, Name = {1}", Id, Name);
		}
		public static void SortLists()
		{
			var list = new List<Employee>();

			// Используем "функтор"
			list.Sort(new EmployeeByIdComparer());

			// Используем делегат
			list.Sort((x, y) => x.Name.CompareTo(y.Name));
		}
	}

	class EmployeeByIdComparer : IComparer<Employee>
	{
		public int Compare(Employee x, Employee y)
		{
			return x.Id.CompareTo(y.Id);
		}
	}

	
}
