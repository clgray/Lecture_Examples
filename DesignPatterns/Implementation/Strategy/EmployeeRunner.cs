﻿using System;
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

			list.Sort((x, y) => x.Name.CompareTo(y.Name));
			//list.Sort(new EmployeeByIdComparer());
			
			foreach (var employee in list)
			{
				Console.WriteLine($"{employee.Name} - {employee.Id}");
			}
		}

		private List<Employee> list = new List<Employee>();
	}
}