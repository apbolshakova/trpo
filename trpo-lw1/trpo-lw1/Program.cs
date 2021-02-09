using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Reflection;

namespace trpo_lw1
{
    class Program
    {
        struct MethodMetrics
        {
            public int NumOfOverloads;
            public int MinParametersNum;
            public int MaxParametersNum;
        }

        static void Main(string[] args)
        {
            StartMainLoop();
        }

        static void StartMainLoop()
        {
            bool closeApp = false;

            while (!closeApp)
            {
                Console.Clear();
                PrintMainMenu();

                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        HandleAllTypesMenu();
                        break;
                    case '2':
                        HandleOneTypeMenu();
                        break;
                    case '3':
                        HandleConsoleView();
                        break;
                    case '0':
                        closeApp = true;
                        break;
                }
            }
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine("Информация по типам:");
            Console.WriteLine("1 – Общая информация по типам");
            Console.WriteLine("2 – Выбрать тип из списка");
            Console.WriteLine("3 – Параметры консоли");
            Console.WriteLine("0 - Выход из программы");
        }

        public static void HandleAllTypesMenu()
        {
            Console.Clear();
            Console.WriteLine("Общая информация по типам");

            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            
            Console.WriteLine("Подключенные сборки: " + refAssemblies.Length);
            foreach (Assembly asm in refAssemblies)
            {
                types.AddRange(asm.GetTypes());
            }
            Console.WriteLine("Всего типов по всем подключенным сборкам: " + types.Count);

            int numOfRefTypes = 0;
            int numOfValueTypes = 0;
            int numOfInterfaces = 0;
            string typeWithMaxNumOfMethods = types[0].Name;
            int maxNumOfParameters = types[0].GetMethods().Length;
            string longestMethodName = types[0].GetMethods()[0].Name;
            string methodWithMaxNumOfArgs = types[0].GetMethods()[0].Name;
            int maxNumOfArgs = types[0].GetMethods()[0].GetParameters().Length;

            foreach (Type type in types)
            {
                if (type.IsClass)
                {
                    numOfRefTypes++;
                }
                else
                {
                    numOfValueTypes++;
                }

                if (type.IsInterface)
                {
                    numOfInterfaces++;
                }

                if (maxNumOfParameters < type.GetMethods().Length)
                {
                    maxNumOfParameters = type.GetMethods().Length;
                    typeWithMaxNumOfMethods = type.Name;
                }

                foreach (MethodInfo method in type.GetMethods())
                {
                    if (longestMethodName.Length < method.Name.Length)
                    {
                        longestMethodName = method.Name;
                    }

                    if (maxNumOfArgs < method.GetParameters().Length)
                    {
                        maxNumOfArgs = method.GetParameters().Length;
                        methodWithMaxNumOfArgs = method.Name;
                    }
                }
            }

            Console.WriteLine($"Ссылочные типы: {numOfRefTypes}");
            Console.WriteLine($"Значимые типы: {numOfValueTypes}");
            Console.WriteLine($"Типы-интерфейсы: {numOfInterfaces}");
            Console.WriteLine($"Тип с максимальным числом методов: {typeWithMaxNumOfMethods}");
            Console.WriteLine($"Самое длинное название метода: {longestMethodName}");
            Console.WriteLine($"Метод с наибольшим числом аргументов: {methodWithMaxNumOfArgs}");
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню");
            Console.ReadKey();
        }

        public static void HandleOneTypeMenu()
        {
            Type type = typeof(void);
            if (!SelectType(ref type))
            {
                return;
            }
            Console.Clear();
            PrintTypeInfo(type);

            Console.WriteLine("Нажмите ‘M’ для вывода дополнительной информации по методам");
            Console.WriteLine("Нажмите ‘0’ для выхода в главное меню");
            char pressedKey = char.ToLower(Console.ReadKey(true).KeyChar);
            while (pressedKey != 'm' && pressedKey != '0')
            {
                pressedKey = char.ToLower(Console.ReadKey(true).KeyChar);
            }

            if (pressedKey != 'm') return;
            Console.Clear();
            PrintAdditionalInfo(type);

            Console.WriteLine("Нажмите ‘0’ для выхода в главное меню");
            while (pressedKey != '0')
            {
                pressedKey = char.ToLower(Console.ReadKey(true).KeyChar);
            }
        }

        private static bool SelectType(ref Type type)
        {
            while (true)
            {
                Console.Clear();
                PrintTypesList();

                char pressedKey = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (pressedKey)
                {
                case '0':
                    return false;
                case '1':
                    type = typeof(uint);
                    return true;
                case '2':
                    type = typeof(int);
                    return true;
                case '3':
                    type = typeof(long);
                    return true;
                case '4':
                    type = typeof(float);
                    return true;
                case '5':
                    type = typeof(double);
                    return true;
                case '6':
                    type = typeof(char);
                    return true;
                case '7':
                    type = typeof(string);
                    return true;
                case '8':
                    type = typeof(Vector);
                    return true;
                case '9':
                    type = typeof(Matrix);
                    return true;
                }
            }
        }

        private static void PrintTypesList()
        {
            Console.WriteLine("Информация по типам");
            Console.WriteLine("Выберите тип:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1 - uint");
            Console.WriteLine("2 - int");
            Console.WriteLine("3 - long");
            Console.WriteLine("4 - float");
            Console.WriteLine("5 - double");
            Console.WriteLine("6 - char");
            Console.WriteLine("7 - string");
            Console.WriteLine("8 - Vector");
            Console.WriteLine("9 - Matrix");
            Console.WriteLine("0 - Назад в главное меню");
        }

        private static void PrintTypeInfo(Type type)
        {
            List<string> methodNames = new List<string>();
            for (int i = 0; i < type.GetMethods().Length; i++)
            {
                if (methodNames.Contains(type.GetMethods()[i].Name))
                    continue;
                methodNames.Add(type.GetMethods()[i].Name);
            }

            int numOfMethods = methodNames.Count;
            int numOfProperties = type.GetProperties().Length;
            int numOfFields = type.GetFields().Length;

            Console.WriteLine($"Информация по типу: {type.FullName}");
            Console.WriteLine($"\tЗначимый тип: {(type.IsValueType ? '+' : '-')}");
            Console.WriteLine($"\tПространство имен: {type.Namespace}");
            Console.WriteLine($"\tСборка: {type.Assembly.GetName().Name}");
            Console.WriteLine($"\tОбщее число элементов: {numOfMethods + numOfProperties + numOfFields}");
            Console.WriteLine($"\tЧисло методов с учётом перегрузок: {numOfMethods}");
            Console.WriteLine($"\tЧисло методов без учёта перегрузок: {type.GetMethods().Length}");
            Console.WriteLine($"\tЧисло свойств: {numOfProperties}");
            Console.WriteLine($"\tЧисло полей: {numOfFields}");

            string fields = "";
            foreach (FieldInfo field in type.GetFields())
            {
                fields += string.IsNullOrEmpty(fields) ? field.Name : ", " + field.Name;
            }
            Console.WriteLine($"\tСписок полей: {fields}");

            string properties = "";
            foreach (PropertyInfo property in type.GetProperties())
            {
                properties += string.IsNullOrEmpty(properties) ? property.Name : ", " + property.Name;
            }
            Console.WriteLine($"\tСписок свойств: {properties}");
        }

        private static void PrintAdditionalInfo(Type type)
        {
            Console.WriteLine("Методы типа " + type.FullName);
            Console.WriteLine("{0, -25} {1, -16} {2, -10}", "Название", "Число перегрузок", "Число параметров");

            Dictionary<string, MethodMetrics> methods = new Dictionary<string, MethodMetrics>();
            foreach (MethodInfo method in type.GetMethods())
            {
                if (methods.ContainsKey(method.Name))
                {
                    MethodMetrics methodMetrics = methods[method.Name];
                    methodMetrics.NumOfOverloads++;

                    if (method.GetParameters().Length < methodMetrics.MinParametersNum)
                    {
                        methodMetrics.MinParametersNum = method.GetParameters().Length;
                    }

                    if (method.GetParameters().Length > methodMetrics.MaxParametersNum)
                    {
                        methodMetrics.MaxParametersNum = method.GetParameters().Length;
                    }

                    methods[method.Name] = methodMetrics;
                }
                else
                {
                    MethodMetrics methodMetrics = new MethodMetrics
                    {
                        NumOfOverloads = 1,
                        MinParametersNum = method.GetParameters().Length,
                        MaxParametersNum = method.GetParameters().Length
                    };
                    methods.Add(method.Name, methodMetrics);
                }
            }

            foreach (KeyValuePair<string, MethodMetrics> method in methods)
            {
                string numOfParameters = "";
                if (method.Value.MinParametersNum == method.Value.MaxParametersNum)
                {
                    numOfParameters += method.Value.MinParametersNum;
                }
                else
                {
                    numOfParameters += method.Value.MinParametersNum + ".." + method.Value.MaxParametersNum;
                }
                Console.WriteLine("{0, -25} {1, -16} {2, -10}", method.Key, method.Value.NumOfOverloads, numOfParameters);
            }
        }

        public static void HandleConsoleView()
        {
            bool closeConsoleChangeMenu = false;

            while (!closeConsoleChangeMenu)
            {
                Console.Clear();
                PrintConsoleChangeMenu();

                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case '2':
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case '3':
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case '0':
                        closeConsoleChangeMenu = true;
                        break;
                }
            }
        }

        private static void PrintConsoleChangeMenu()
        {
            Console.WriteLine("Смена цветовой темы консоли:");
            Console.WriteLine("1 – Тёмная тема");
            Console.WriteLine("2 – Светлая тема");
            Console.WriteLine("3 – Цветная тема");
            Console.WriteLine("0 - Вернуться в главное меню");
        }
    }
}
