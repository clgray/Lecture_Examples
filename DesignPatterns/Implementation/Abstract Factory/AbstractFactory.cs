using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Abstract_Factory
{
	abstract class AbstractFactory
	{
		public abstract AbstractProductA CreateProductA();
		public abstract AbstractProductB CreateProductB();
	}
	class ConcreteFactory1 : AbstractFactory
	{
		public override AbstractProductA CreateProductA()
		{
			return new ProductA1();
		}

		public override AbstractProductB CreateProductB()
		{
			return new ProductB1();
		}
	}
	class ConcreteFactory2 : AbstractFactory
	{
		public override AbstractProductA CreateProductA()
		{
			return new ProductA2();
		}

		public override AbstractProductB CreateProductB()
		{
			return new ProductB2();
		}
	}

	abstract class AbstractProductA
	{ }

	abstract class AbstractProductB
	{ }

	class ProductA1 : AbstractProductA
	{ }

	class ProductB1 : AbstractProductB
	{ }

	class ProductA2 : AbstractProductA
	{ }

	class ProductB2 : AbstractProductB
	{ }

	class Client
	{
		private AbstractProductA abstractProductA;
		private AbstractProductB abstractProductB;

		public Client(AbstractFactory factory)
		{
			abstractProductB = factory.CreateProductB();
			abstractProductA = factory.CreateProductA();
		}
		public void Run()
		{ }
	}
}
