using System;
using System.Collections.Generic;

namespace Implementation.Prototype
 {
    public interface IPrototype<T>
    {
        T Clone();
    }

    public class Circle : IPrototype<Circle>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }

        public Circle(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        // Поверхностное копирование
        public Circle Clone()
        {
            return (Circle)this.MemberwiseClone();
        }

        public override string ToString()
            => $"Circle: X={X}, Y={Y}, Radius={Radius}";
    }

    public class Rectangle : IPrototype<Rectangle>
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int w, int h)
        {
            Width = w;
            Height = h;
        }

        public Rectangle Clone()
        {
            return (Rectangle)this.MemberwiseClone();
        }

        public override string ToString()
            => $"Rectangle: Width={Width}, Height={Height}";
    }


    public class PrototypeRegistry
    {
        private readonly Dictionary<Type, object> _prototypes = new();

        public void Register<T>(T prototype)
        {
            _prototypes[typeof(T)] = prototype;
        }

        public T CreateClone<T>()
        {
            if (_prototypes[typeof(T)] is IPrototype<T> proto)
                return proto.Clone();
            throw new ArgumentException("Прототип не найден или несовместим.");
        }
    }

    public class App
    {
        public static void Run()
        {
            var registry = new PrototypeRegistry();
            registry.Register(new Circle(10, 20, 5));
            registry.Register(new Rectangle(30, 40));

            var circle1 = registry.CreateClone<Circle>();
            var circle2 = registry.CreateClone<Circle>();
            var rect1 = registry.CreateClone<Rectangle>();

            // Изменяем клон
            circle1.Radius = 15;

            Console.WriteLine(circle1);
            Console.WriteLine(circle2);

            Console.WriteLine(rect1);
        }
    }
}
