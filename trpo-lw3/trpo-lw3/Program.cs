﻿using System;
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

        public static Square Create(string name, double side, double[] centerCoords, string color)
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

        public static Circle Create(string name, double radius, double[] centerCoords, string color)
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

        public static Cube Create(string name, double side, double[] centerCoords, string color)
        {
            Cube cube = new Cube(name, side, centerCoords, color);
            return cube;
        }
    }

    sealed class Ball : Shape3D
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

        public static Ball Create(string name, double radius, double[] centerCoords, string color)
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
            Console.WriteLine();
            Console.WriteLine("Нажмите любую кнопку чтобы перейти в консольное меню.");
            Console.ReadKey();
            StartMainLoop();
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
            //1) Создать корректный Shape2D, PrintDimension, GetArea, PrintInfo и PrintCenterCoords, корректно изменить имя - цвет - центр, PrintInfo и PrintCenterCoords, некорректно поменять центр
            //2) Попытаться создать Shape3D с некорректным цветом, создать корректный Shape3D, PrintDimension, PrintInfo и PrintCenterCoords, некорректно поменять имя, некорректно поменять центр
            //3) Попытаться создать Square c некорректной стороной, создать корректный Square через конструктор, PrintInfo
            //4) Создать корректный Square через Create, PrintInfo, попытаться присвоить некорректный side
            //5) Попытаться создать Circle c некорректным радиусом, создать корректный Circle через конструктор, PrintInfo
            //6) Создать корректный Circle через Create, PrintInfo, попытаться присвоить некорректный radius
            //7) Попытаться создать Cube c некорректной стороной, создать корректный Cube через конструктор, PrintInfo
            //8) Создать корректный Cube через Create, PrintInfo, попытаться присвоить некорректный side
            //9) Попытаться создать Ball c некорректным радиусом, создать корректный Ball через конструктор, PrintInfo
            //10) Создать корректный Ball через Create, PrintInfo, попытаться присвоить некорректный radius
            //11) Сравнить по имени и по цвету фигуры из тестов

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


            Console.WriteLine("\n---------------------------------------------------------------\n\n2. Проверка работы Shape3D:");
            Shape3D shape3D;
            Console.WriteLine("\nПопытка создать Shape3D с некорректный цветом:");
            try
            {
                shape3D = new Shape3D("shape2", new[] {6.01, 4.44, 3.03}, "Rainbow");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nСоздана 3D фигура.");
            shape3D = new Shape3D("shape2", new[] {6.01, 4.44, 3.03}, "Blue");
            Console.WriteLine("\nВывод размерности:");
            shape3D.PrintDimension();
            Console.WriteLine("\nВывод информации и координат:");
            shape3D.PrintInfo();
            shape3D.PrintCenterCoords();
            Console.WriteLine("\nПопытка некорректно поменять имя:");
            try
            {
                shape3D.Name = " ";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nПопытка задания некорректных координат центра:");
            try
            {
                shape3D.CenterCoords = new[] { 2.91, 3.09 };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n---------------------------------------------------------------\n\n3. Проверка работы Square, созданного через конструктор:");
            Square square1;
            Console.WriteLine("\nПопытка создать Square с некорректной стороной:");
            try
            {
                square1 = new Square("square1", -1, new[] { 9.2, 8.3 }, "Red");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nСоздан квадрат. Вывод информации:");
            square1 = new Square("square1", 5, new[] { 9.2, 8.3 }, "Red");
            square1.PrintInfo();

            Console.WriteLine("\n---------------------------------------------------------------\n\n4. Проверка работы Square, созданного через Create:");
            Square square2 = Square.Create("square2", 9.1, new[] { 3.9, 3.7 }, "Green");
            Console.WriteLine("\nСоздан квадрат с помощью метода Create. Вывод информации:");
            square2.PrintInfo();
            Console.WriteLine("\nПопытка задать некорректную сторону:");
            try
            {
                square2.Side = -2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n---------------------------------------------------------------\n\n5. Проверка работы Circle, созданного через конструктор:");
            Circle circle1;
            Console.WriteLine("\nПопытка создать Circle с некорректным радиусом:");
            try
            {
                circle1 = new Circle("circle1", -1, new[] { 9.2, 8.3 }, "Red");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nСоздан круг. Вывод информации:");
            circle1 = new Circle("circle1", 5, new[] { 9.2, 8.3 }, "Blue");
            circle1.PrintInfo();

            Console.WriteLine("\n---------------------------------------------------------------\n\n6. Проверка работы Circle, созданного через Create:");
            Circle circle2 = Circle.Create("circle2", 9.1, new[] { 3.9, 3.7 }, "Red");
            Console.WriteLine("\nСоздан круг с помощью метода Create. Вывод информации:");
            circle2.PrintInfo();
            Console.WriteLine("\nПопытка задать некорректный радиус:");
            try
            {
                circle2.Radius = -2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n---------------------------------------------------------------\n\n7. Проверка работы Cube, созданного через конструктор:");
            Cube cube1;
            Console.WriteLine("\nПопытка создать Cube с некорректной стороной:");
            try
            {
                cube1 = new Cube("cube1", -1, new[] { 9.2, 8.3, 1.2 }, "Red");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nСоздан куб. Вывод информации:");
            cube1 = new Cube("cube1", 5, new[] { 9.2, 8.3, 1.2 }, "Green");
            cube1.PrintInfo();

            Console.WriteLine("\n---------------------------------------------------------------\n\n8. Проверка работы Cube, созданного через Create:");
            Cube cube2 = Cube.Create("cube2", 9.1, new[] { 3.9, 3.7, 9.2 }, "Blue");
            Console.WriteLine("\nСоздан куб с помощью метода Create. Вывод информации:");
            cube2.PrintInfo();
            Console.WriteLine("\nПопытка задать некорректную сторону:");
            try
            {
                cube2.Side = -2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n---------------------------------------------------------------\n\n9. Проверка работы Ball, созданного через конструктор:");
            Ball ball1;
            Console.WriteLine("\nПопытка создать Ball с некорректным радиусом:");
            try
            {
                ball1 = new Ball("ball1", -1, new[] { 9.2, 8.3, 3.7 }, "Red");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("\nСоздан шар. Вывод информации:");
            ball1 = new Ball("ball1", 5, new[] { 9.2, 8.3, 3.7 }, "Red");
            ball1.PrintInfo();

            Console.WriteLine("\n---------------------------------------------------------------\n\n10. Проверка работы Ball, созданного через Create:");
            Ball ball2 = Ball.Create("ball2", 9.1, new[] { 3.9, 3.7, 7.7 }, "Green");
            Console.WriteLine("\nСоздан шар с помощью метода Create. Вывод информации:");
            ball2.PrintInfo();
            Console.WriteLine("\nПопытка задать некорректный радиус:");
            try
            {
                ball2.Radius = -2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n---------------------------------------------------------------\n\n11. Проверка работы сравнения:");
            Console.WriteLine("\nФигуры из предыдущих тестов занесены в список.");
            List<Figure> figures = new List<Figure>() { shape2D, shape3D, square1, circle1, cube1, ball1, square2, circle2, cube2, ball2 };
            FigureComparer figureComparer = new FigureComparer() { Field = FigureComparer.CompareField.ByName };
            figures.Sort(figureComparer);
            Console.WriteLine("\nПорядок после сортировки по имени:");
            foreach (Figure figure in figures)
            {
                Console.WriteLine($"{figure.Name} ");
            }
            figureComparer.Field = FigureComparer.CompareField.ByColor;
            figures.Sort(figureComparer);
            Console.WriteLine("\nПорядок после сортировки по цвету:");
            foreach (Figure figure in figures)
            {
                Console.WriteLine($"{figure.Name} ({figure.Color}) ");
            }
        }

        private static void StartMainLoop()
        {
            List<Figure> figures = new List<Figure>();

            bool closeApp = false;

            while (!closeApp)
            {
                Console.Clear();
                PrintMainMenu();

                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        HandleCreation(figures);
                        break;
                    case '2':
                        HandleListPrinting(figures);
                        break;
                    case '3':
                        HandleInfoPrinting(figures);
                        break;
                    case '4':
                        HandleSorting(figures);
                        break;
                    case '5':
                        HandleCounting(figures);
                        break;
                    case '6':
                        HandleSearching(figures);
                        break;
                    case '7':
                        HandleSummation(figures);
                        break;
                    case '0':
                        closeApp = true;
                        break;
                }
            }
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine("Работа с фигурами");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1 – Создать фигуру");
            Console.WriteLine("2 – Вывести список фигур");
            Console.WriteLine("3 – Посмотреть информацию о фигурах");
            Console.WriteLine("4 - Сортировка фигур");
            Console.WriteLine("5 - Подсчёт количества фигур");
            Console.WriteLine("6 - Поиск среди фигур");
            Console.WriteLine("7 - Подсчёт суммы площадей фигур");
            Console.WriteLine("0 - Выход");
            Console.WriteLine("-------------------------------");
        }

        private static void HandleCreation(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleListPrinting(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleInfoPrinting(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleSorting(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleCounting(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleSearching(List<Figure> figures)
        {
            throw new NotImplementedException();
        }

        private static void HandleSummation(List<Figure> figures)
        {
            throw new NotImplementedException();
        }
    }
}
