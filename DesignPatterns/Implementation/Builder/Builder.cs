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
		}
		public Product Result => builder.GetResult();
	}

	abstract class Builder
	{
		public abstract void BuildPartA();
		public abstract void BuildPartB();
		public abstract void BuildPartC();
		public abstract Product GetResult();
	}

	class Product
	{
		List<string> parts = new List<string>();
		public void Add(string part)
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

	class ConcreteBuilder : Builder
	{
		Product product = new Product();
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
		public override Product GetResult()
		{
			return product;
		}
	}
}