using System;

namespace Implementation.TemplateMethod
{
	class DeveloperTemplateMethod
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
			developer.BuildHouse();
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

		// шаблонный метод
		protected abstract void Build();

		public void BuildHouse()
		{
			Build();
		}
	}

	// строит панельные дома
	class PanelDeveloper : Developer
	{
		public PanelDeveloper(string n) : base(n)
		{
		}

		protected override void Build()
		{
			Console.WriteLine("Панельный дом построен");
		}
	}

	// строит деревянные дома
	class WoodDeveloper : Developer
	{
		public WoodDeveloper(string n) : base(n)
		{
		}

		protected override void Build()
		{
			Console.WriteLine("Деревянный дом построен");
		}
	}

	class BlockDeveloper : Developer
	{
		public BlockDeveloper(string n) : base(n)
		{
		}

		protected override void Build()
		{
			Console.WriteLine("Блочный дом построен");
		}
	}
}