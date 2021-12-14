using System.Collections.Generic;

namespace Implementation.Strategy
{
	class EmployeeByIdComparer : IComparer<Employee>
	{
		public int Compare(Employee x, Employee y)
		{
			return x.Id.CompareTo(y.Id);
		}
	}
}