using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo_lw3
{
    abstract class Figure
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Ошибка: название фигуры не может быть пустым!");
                }

                _name = value;
            }
        }

        private enum ColorEnum
        {
            Red, Green, Blue
        }
        private ColorEnum _color;
        public string Color
        {
            get => _color.ToString();
            set
            {
                if (Enum.IsDefined(typeof(ColorEnum), value))
                {
                    throw new Exception("Ошибка: некорректный цвет! Допустимые цвета: Red, Green или Blue.");
                }

                _color = (ColorEnum) Enum.Parse(typeof(ColorEnum), value);
            }
        }

        public abstract void PrintDimension();

        public virtual double GetArea()
        {
            throw new Exception("Ошибка: попытка получить площадь неопределённой фигуры!");
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine("Тип: неопределённая фигура");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цвет: {Color}");
        }
    }

    class FigureComparer : IComparer<Figure>
    {
        public enum CompareField : byte
        {
            ByName = 0,
            ByColor = 1
        }
        public CompareField Field { get; set; }
        public int Compare(Figure x, Figure y)
        {
            switch (Field)
            {
                case CompareField.ByName:
                    return string.Compare(x?.Name, y?.Name, StringComparison.Ordinal);
                case CompareField.ByColor:
                    return string.Compare(x?.Color, y?.Color, StringComparison.Ordinal);
                default:
                    return 0;
            }
        }
    }

    class Shape2D : Figure 
    {
        private double[] _centerCoords;
        public double[] CenterCoords
        {
            get => _centerCoords;
            set
            {
                if (value.Length != 2)
                {
                    throw new Exception("Ошибка: в массиве координат должно быть ровно два значения!");
                }

                _centerCoords = value;
            }
        }

        public sealed override void PrintDimension()
        {
            Console.WriteLine("Размерность: 2D фигура");
        }
    }

    class Shape3D : Figure
    {
        private double[] _centerCoords;
        public double[] CenterCoords
        {
            get => _centerCoords;
            set
            {
                if (value.Length != 3)
                {
                    throw new Exception("Ошибка: в массиве координат должно быть ровно три значения!");
                }

                _centerCoords = value;
            }
        }

        public sealed override void PrintDimension()
        {
            Console.WriteLine("Размерность: 3D фигура");
        }
    }

    sealed class Square : Shape2D 
    {
        private double _side;
        public double Side
        {
            get => _side;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Ошибка: сторона квадрата должна быть положительной!");
                }

                _side = value;
            }
        }

        public Square(string name, double side, double[] centerCoords, string color)
        {
            Name = name;
            Side = side;
            CenterCoords = centerCoords;
            Color = color;
        }

        public override double GetArea()
        {
            return _side * _side;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: квадрат");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цвет: {Color}");
            PrintDimension();
            Console.WriteLine($"Сторона: {Side}");
            Console.WriteLine($"Площадь: {GetArea()}");
        }

        static Square Create(string name, double side, double[] centerCoords, string color)
        {
            Square square = new Square(name, side, centerCoords, color);
            return square;
        }
    }

    sealed class Circle : Shape2D
    {
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Ошибка: радиус круга должен быть положительным!");
                }

                _radius = value;
            }
        }

        public Circle(string name, double radius, double[] centerCoords, string color)
        {
            Name = name;
            Radius = radius;
            CenterCoords = centerCoords;
            Color = color;
        }

        public override double GetArea()
        {
            return Math.PI * _radius * _radius;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: круг");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цвет: {Color}");
            PrintDimension();
            Console.WriteLine($"Радиус: {Radius}");
            Console.WriteLine($"Площадь: {GetArea()}");
        }

        static Circle Create(string name, double radius, double[] centerCoords, string color)
        {
            Circle circle = new Circle(name, radius, centerCoords, color);
            return circle;
        }
    }

    sealed class Cube : Shape3D
    {
        private double _side;
        public double Side
        {
            get => _side;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Ошибка: сторона куба должна быть положительной!");
                }

                _side = value;
            }
        }

        public Cube(string name, double side, double[] centerCoords, string color)
        {
            Name = name;
            Side = side;
            CenterCoords = centerCoords;
            Color = color;
        }

        public override double GetArea()
        {
            return 6 * _side * _side;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: куб");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цвет: {Color}");
            PrintDimension();
            Console.WriteLine($"Сторона: {Side}");
            Console.WriteLine($"Площадь: {GetArea()}");
        }

        static Cube Create(string name, double side, double[] centerCoords, string color)
        {
            Cube cube = new Cube(name, side, centerCoords, color);
            return cube;
        }
    }

    sealed class Ball : Shape2D
    {
        private double _radius;
        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Ошибка: радиус шара должен быть положительным!");
                }

                _radius = value;
            }
        }

        public Ball(string name, double radius, double[] centerCoords, string color)
        {
            Name = name;
            Radius = radius;
            CenterCoords = centerCoords;
            Color = color;
        }

        public override double GetArea()
        {
            return 4 * Math.PI * _radius * _radius;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: шар");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цвет: {Color}");
            PrintDimension();
            Console.WriteLine($"Радиус: {Radius}");
            Console.WriteLine($"Площадь: {GetArea()}");
        }

        static Ball Create(string name, double radius, double[] centerCoords, string color)
        {
            Ball ball = new Ball(name, radius, centerCoords, color);
            return ball;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PrintVariant();
            Console.ReadKey();
        }

        private static void PrintVariant()
        {
            const char c1 = 'O';
            const char c2 = 'N';
            const int variant = ((int)c1 + (int)c2) % 9;
            Console.WriteLine($"Вариант {variant}");
        }
    }
}
