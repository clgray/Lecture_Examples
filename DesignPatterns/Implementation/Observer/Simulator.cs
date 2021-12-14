using System.Collections;

namespace Implementation.Observer
{
	class Simulator : IEnumerable
	{
		string[] moves = { "5", "3", "1", "6", "7", "2", "4" };

		public IEnumerator GetEnumerator()
		{
			foreach (string element in moves)
				yield return element;
		}
	}
}