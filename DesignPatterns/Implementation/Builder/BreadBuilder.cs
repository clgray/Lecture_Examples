﻿using System;

namespace Implementation.Builder
{
	class App
	{
		public static void Run()
		{
			// содаем объект пекаря
			Baker baker = new Baker();
			// создаем билдер для ржаного хлеба
			BreadBuilder builder = new RyeBreadBuilder();
			// выпекаем
			Bread ryeBread = baker.Bake(builder);
			Console.WriteLine(ryeBread.ToString());
			// оздаем билдер для пшеничного хлеба
			builder = new WheatBreadBuilder();
			Bread wheatBread = baker.Bake(builder);
			Console.WriteLine(wheatBread.ToString());

			Console.Read();
		}
	}
	// абстрактный класс строителя
	abstract class BreadBuilder
	{
		public Bread Bread { get; private set; }
		public BreadBuilder()
		{
			Bread = new Bread();
		}
		public abstract void SetFlour();
		public abstract void SetSalt();
		public abstract void SetAdditives();
	}
	// пекарь
	class Baker
	{
		public Bread Bake(BreadBuilder breadBuilder)
		{
			breadBuilder.SetFlour();
			breadBuilder.SetSalt();
			breadBuilder.SetAdditives();
			return breadBuilder.Bread;
		}
	}
	// строитель для ржаного хлеба
	class RyeBreadBuilder : BreadBuilder
	{
		public override void SetFlour()
		{
			this.Bread.Flour = new Flour { Sort = "Ржаная мука 1 сорт" };
		}

		public override void SetSalt()
		{
			this.Bread.Salt = new Salt();
		}

		public override void SetAdditives()
		{
			// не используется
		}
	}
	// строитель для пшеничного хлеба
	class WheatBreadBuilder : BreadBuilder
	{
		public override void SetFlour()
		{
			this.Bread.Flour = new Flour { Sort = "Пшеничная мука высший сорт" };
		}

		public override void SetSalt()
		{
			this.Bread.Salt = new Salt();
		}

		public override void SetAdditives()
		{
			this.Bread.Additives = new Additives { Name = "улучшитель хлебопекарный" };
		}
	}
}