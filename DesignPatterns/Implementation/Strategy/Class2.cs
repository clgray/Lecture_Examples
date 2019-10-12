using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Strategy
{
	class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public override string ToString()
		{
			return string.Format("Id = {0}, Name = {1}", Id, Name);
		}
	}

	class EmployeeByIdComparer : IComparer<Employee>
	{
		public int Compare(Employee x, Employee y)
		{
			return x.Id.CompareTo(y.Id);
		}
	}

	//public static void SortLists()
	//{
	//var list = new List<Employee>();

	//// Используем "функтор"
	//list.Sort(new EmployeeByIdComparer());

	//// Используем делегат
	//list.Sort((x, y) => x.Name.CompareTo(y.Name));
	//}
}
