using System;
using System.Collections.Generic;

namespace Implementation.Builder
{
	class Client
	{
		public static void Run()
		{
			Builder builder = new ConcreteBuilder();
			Director director = new Director(builder);
			director.Construct();
			var product = director.Result;

			Console.WriteLine(product);
			Console.ReadKey();
		}
	}
	class Director
	{
		Builder builder;
		public Director(Builder builder)
		{
			this.builder = builder;
		}
		public void Construct()
		{
			builder.BuildPartC();
			builder.BuildPartA();
			builder.BuildPartB();
			builder.BuildPartC();
			builder.BuildPartC();
			builder.BuildPartB();
			builder.BuildPartB();
			builder.BuildPartB();

		}
		public BaseProduct Result => builder.GetResult();
	}

	abstract class Builder
	{
		public abstract void BuildPartA();
		public abstract void BuildPartB();
		public abstract void BuildPartC();
		public abstract BaseProduct GetResult();
	}

	class Product:BaseProduct
	{
		List<string> parts = new List<string>();
		public override void Add(string part)
		{
			parts.Add(part);
		}
		public override string ToString()
		{

			var sb = new System.Text.StringBuilder();
			foreach (var item in parts)
			{
				sb.Append("-");
				sb.Append(item);
			}
			sb.Replace("-", "+");

			return sb.ToString();
		}

	}


	class TestProduct: BaseProduct
	{
		List<string> parts = new List<string>();
		public override void Add(string part)
		{
			parts.Add(part);
		}
		public override string ToString()
		{
			string st = "";
			foreach (var item in parts)
			{
				st += "+" + item;
			}
			return st;
		}
	}
	internal abstract class BaseProduct
	{
		public abstract void Add(string part);
	}
	class ConcreteBuilder : Builder
	{
		BaseProduct product = new TestProduct();
		public override void BuildPartA()
		{
			product.Add("Part A");
		}
		public override void BuildPartB()
		{
			product.Add("Part B");
		}
		public override void BuildPartC()
		{
			product.Add("Part C");
		}
		public override BaseProduct GetResult()
		{
			return product;
		}
	}
}