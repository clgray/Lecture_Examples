using System;

namespace Implementation.FactoryMethod
{
	class DeveloperFactoryMethod
	{
		public static void Run()
		{
			Run(new PanelDeveloper("ООО КирпичСтрой"));
			Run(new WoodDeveloper("Частный застройщик"));
			Run(new BlockDeveloper("ООО БытовкаСтрой"));
			Console.ReadLine();
		}

		static void Run(Developer developer)
		{
			var house = developer.Create();
			Console.WriteLine(house.Get());
		}
	}
	// абстрактный класс строительной компании
	abstract class Developer
	{
		public string Name { get; set; }

		public Developer(string n)
		{
			Name = n;
		}
		// фабричный метод
		abstract public House Create();
	}
	// строит панельные дома
	class PanelDeveloper : Developer
	{
		public PanelDeveloper(string n) : base(n)
		{ }

		public override House Create()
		{
			return new PanelHouse();
		}

	}
	// строит деревянные дома
	class WoodDeveloper : Developer
	{
		public WoodDeveloper(string n) : base(n)
		{ }

		public override House Create()
		{
			return new WoodHouse();
		}
	}

	class BlockDeveloper : Developer
	{
		public BlockDeveloper(string n) : base(n)
		{ }

		public override House Create()
		{
			return new BlockHouse();
		}
	}

	abstract class House
	{
		public virtual string Get()
		{
			return "House";
		}
	}

	class PanelHouse : House
	{
		public PanelHouse()
		{
			Console.WriteLine("Панельный дом построен");
		}
		public override string Get()
		{
			return "Панельный дом";
		}
	}
	class WoodHouse : House
	{
		public WoodHouse()
		{
			Console.WriteLine("Деревянный дом построен");
		}
		public override string Get()
		{
			return "Деревянный дом";
		}
	}

	class BlockHouse : House
	{
		public BlockHouse()
		{
			Console.WriteLine("Блочный дом построен");
		}
	}
}