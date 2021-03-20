using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Implementation.heap
{
	class Heap
	{
		List<int> A = new List<int>();

		public void Add(int a)
		{
			A.Add(a);
			heapify(A.Count - 1);
		}

		public int GetMax()
		{
			var max = A[0];
			var last = A[A.Count - 1];
			A[0] = last;
			A.RemoveAt(A.Count - 1);
			Restore(0);
			return max;
		}

		public int Count => A.Count;

		private void Restore(int i)
		{
			var left = i * 2 + 1;
			var right = i * 2 + 2;

			var largest = i;

			if (left < A.Count - 1 && A[left] > A[largest])
			{
				largest = left;
			}

			if (right <= A.Count - 1 && A[right] > A[largest])
			{
				largest = right;

			}

			if (largest != i)
			{
				Swap(i, largest);
				Restore(largest);
			}

			
		}

		void heapify(int index)
		{
			if (index == 0) return;
			var parentIndex = (index - 1) / 2;
			if (A[index] > A[parentIndex])
			{
				Swap(index, parentIndex);
				heapify(parentIndex);
			}
		}

		private void Swap(int index1, int intex2)
		{
			var tmp = A[index1];
			A[index1] = A[intex2];
			A[intex2] = tmp;
		}

		public override string ToString()
		{
			return string.Join("-", A);
		}
	}

	public class HeapRun
	{
		public static void Run()
		{
			var heap = new Heap();
			heap.Add(1);
			heap.Add(2);
			heap.Add(3);
			heap.Add(5);
			heap.Add(6);
			heap.Add(8);
			heap.Add(10);
			heap.Add(11);
			heap.Add(16);
			heap.Add(9);
			Console.WriteLine(heap);
			heap.Add(20);
			Console.WriteLine(heap);

			while (heap.Count>0)
			{
				Console.WriteLine(heap.GetMax());
			}
		}
	}
}