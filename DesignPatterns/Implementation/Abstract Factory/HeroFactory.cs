using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Abstract_Factory
{
	class App
	{
		public void Run()
		{
			Run(new StrikeHeroFactory());
			//Run(new VoinFactory());

			Console.ReadLine();
		}

		static void Run(HeroFactory heroFactory)
		{
			var hero = new Hero(heroFactory);
			hero.Run();
			hero.Hit();
		}
	}
	//абстрактный класс - оружие
	abstract class Weapon
	{
		public abstract void Hit();
	}
	// абстрактный класс движение
	abstract class Movement
	{
		public abstract void Move();
	}

	// класс арбалет
	class Arbalet : Weapon
	{
		public override void Hit()
		{
			Console.WriteLine("Стреляем из арбалета");
		}
	}
	// класс меч
	class Sword : Weapon
	{
		public override void Hit()
		{
			Console.WriteLine("Бьем мечом");
		}
	}
	// движение полета
	class FlyMovement : Movement
	{
		public override void Move()
		{
			Console.WriteLine("Летим");
		}
	}
	// движение - бег
	class RunMovement : Movement
	{
		public override void Move()
		{
			Console.WriteLine("Бежим");
		}
	}
	// класс абстрактной фабрики
	abstract class HeroFactory
	{
		public abstract Movement CreateMovement();
		public abstract Weapon CreateWeapon();
	}
	// Фабрика создания летящего героя с арбалетом
	class ElfFactory : HeroFactory
	{
		public override Movement CreateMovement()
		{
			return new FlyMovement();
		}

		public override Weapon CreateWeapon()
		{
			return new Arbalet();
		}
	}
	// Фабрика создания бегущего героя с мечом
	class VoinFactory : HeroFactory
	{
		public override Movement CreateMovement()
		{
			return new RunMovement();
		}

		public override Weapon CreateWeapon()
		{
			return new Sword();
		}
	}

	// Фабрика создания бегущего героя с арбалетом
	class StrikeHeroFactory : HeroFactory
	{
		public override Movement CreateMovement()
		{
			return new RunMovement();
		}

		public override Weapon CreateWeapon()
		{
			return new Arbalet();
		}
	}
	// клиент - сам супергерой
	class Hero
	{
		private Weapon weapon;
		private Movement movement;
		public Hero(HeroFactory factory)
		{
			weapon = factory.CreateWeapon();
			movement = factory.CreateMovement();
		}
		public void Run()
		{
			movement.Move();
		}
		public void Hit()
		{
			weapon.Hit();
		}
	}
}
