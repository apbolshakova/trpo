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
                if (!Enum.IsDefined(typeof(ColorEnum), value))
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

        public Shape2D(string name, double[] centerCoords, string color)
        {
            Name = name;
            CenterCoords = centerCoords;
            Color = color;
        }

        public sealed override void PrintDimension()
        {
            Console.WriteLine("Размерность: 2D фигура");
        }

        public void PrintCenterCoords()
        {
            Console.WriteLine($"Координаты центра: {_centerCoords[0]} по X; {_centerCoords[1]} по Y");
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

        public Shape3D(string name, double[] centerCoords, string color)
        {
            Name = name;
            CenterCoords = centerCoords;
            Color = color;
        }

        public sealed override void PrintDimension()
        {
            Console.WriteLine("Размерность: 3D фигура");
        }

        public void PrintCenterCoords()
        {
            Console.WriteLine($"Координаты центра: {_centerCoords[0]} по X; {_centerCoords[1]} по Y; {_centerCoords[2]} по Z");
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

        public Square(string name, double side, double[] centerCoords, string color) : base(name, centerCoords, color)
        {
            Side = side;
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
            PrintCenterCoords();
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

        public Circle(string name, double radius, double[] centerCoords, string color) : base(name, centerCoords, color)
        {
            Radius = radius;
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
            PrintCenterCoords();
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

        public Cube(string name, double side, double[] centerCoords, string color): base(name, centerCoords, color)
        {
            Side = side;
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
            PrintCenterCoords();
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

        public Ball(string name, double radius, double[] centerCoords, string color) : base(name, centerCoords, color)
        {
            Radius = radius;
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
            PrintCenterCoords();
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
            Console.WriteLine();
            TestClasses();
            Console.ReadKey();
        }

        private static void PrintVariant()
        {
            const char c1 = 'O';
            const char c2 = 'N';
            const int variant = ((int)c1 + (int)c2) % 9;
            Console.WriteLine($"Вариант {variant}");
        }

        private static void TestClasses()
        {
            //1) Создать корректный Shape2D, PrintDimension, GetArea, PrintInfo, корректно изменить имя - цвет - центр, PrintInfo, некорректно поменять центр
            //2) Попытаться создать Shape3D с некорректным цветом, создать корректный Shape3D, PrintDimension, PrintInfo, некорректно поменять имя, некорректно поменять центр
            //3) Сравнить по имени и по цвету Shape2D и Shape 3D
            //4) Попытаться создать Square c некорректной стороной, создать корректный Square через конструктор, PrintInfo
            //5) Создать корректный Square через Create, PrintInfo, попытаться присвоить некорректный side
            //6) Попытаться создать Circle c некорректным радиусом, создать корректный Circle через конструктор, PrintInfo
            //7) Создать корректный Circle через Create, PrintInfo, попытаться присвоить некорректный radius
            //8) Попытаться создать Cube c некорректной стороной, создать корректный Cube через конструктор, PrintInfo
            //9) Создать корректный Cube через Create, PrintInfo, попытаться присвоить некорректный side
            //10) Попытаться создать Ball c некорректным радиусом, создать корректный Ball через конструктор, PrintInfo
            //11) Создать корректный Ball через Create, PrintInfo, попытаться присвоить некорректный radius

            Console.WriteLine("Тестирование работы классов:");
            Console.WriteLine("\n1. Проверка работы Shape2D:");
            Console.WriteLine("\nСоздана 2D фигура.");
            var shape2D = new Shape2D("shape1", new [] {8.63, 9.98}, "Red");
            Console.WriteLine("\nВывод размерности:");
            shape2D.PrintDimension();
            Console.WriteLine("\nПопытка вывести площадь:");
            try
            {
                shape2D.GetArea();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nВывод информации и координат:");
            shape2D.PrintInfo();
            shape2D.PrintCenterCoords();
            shape2D.Name = "newShape1";
            shape2D.Color = "Green";
            shape2D.CenterCoords[0] = -1.52;
            Console.WriteLine("\nВывод информации и координат после изменения полей:");
            shape2D.PrintInfo();
            shape2D.PrintCenterCoords();
            Console.WriteLine("\nПопытка задания некорректных координат центра:");
            try
            {
                shape2D.CenterCoords = new[] {2.91, 3.09, 2.02};
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("---------------------------------------------------------------\n\n2. Проверка работы Shape3D:");

            Console.WriteLine("---------------------------------------------------------------\n\n3. Проверка работы сравнения:");

            Console.WriteLine("---------------------------------------------------------------\n\n4. Проверка работы Square, созданного через конструктор:");

            Console.WriteLine("---------------------------------------------------------------\n\n5. Проверка работы Square, созданного через Create:");

            Console.WriteLine("---------------------------------------------------------------\n\n6. Проверка работы Circle, созданного через конструктор:");

            Console.WriteLine("---------------------------------------------------------------\n\n7. Проверка работы Circle, созданного через Create:");

            Console.WriteLine("---------------------------------------------------------------\n\n8. Проверка работы Cube, созданного через конструктор:");

            Console.WriteLine("---------------------------------------------------------------\n\n9. Проверка работы Cube, созданного через Create:");

            Console.WriteLine("---------------------------------------------------------------\n\n10. Проверка работы Ball, созданного через конструктор:");

            Console.WriteLine("---------------------------------------------------------------\n\n11. Проверка работы Ball, созданного через Create:");

        }
    }
}
