﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trpo_lw3
{
    abstract class Figure
    {
        private enum ColorEnum
        {
            Red, Green, Blue
        }

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

    class Shape2D : Figure {
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
            Console.WriteLine("2D фигура.");
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
            Console.WriteLine("3D фигура.");
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
