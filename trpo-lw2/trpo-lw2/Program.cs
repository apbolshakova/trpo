using System;
using System.Collections.Generic;
using System.Globalization;

namespace trpo_lw2
{
    class Program
    {
        class Matrix : ICloneable
        {
            private double[,] data;


            public Matrix(int nRows, int nCols)
            {
                data = new double[nRows, nCols];
            }

            public Matrix(double[,] initData)
            {
                data = new double[initData.GetLength(0), initData.GetLength(1)];
                Array.Copy(initData, 0, data, 0, initData.Length);
            }

            public Matrix(double[][] initData)
            {
                data = new double[initData.Length, initData[0].Length];
                for (int j = 0; j < initData.Length; j++)
                {
                    if (initData[j].Length != initData[0].Length)
                        throw new Exception(
                            "Ошибка: попытка создать матрицу из двумерного массива с разной длиной строк!");

                    for (int i = 0; i < initData[j].Length; i++)
                    {
                        data[j, i] = initData[j][i];
                    }
                }
            }

            public double this[int i1, int i2]
            {
                get => data[i1, i2];
                set => data[i1, i2] = value;
            }


            public int Rows => data.GetLength(0);

            public int Columns => data.GetLength(1);

            // Размер квадратной матрицы
            public int? Size
            {
                get
                {
                    if (IsSquared) return Rows;
                    return null;
                }
            }


            // Является ли матрица квадратной
            public bool IsSquared => (Rows == Columns);

            // Является ли матрица нулевой
            public bool IsEmpty
            {
                get
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        for (int i = 0; i < Columns; i++)
                        {
                            if (data[j, i] != 0) return false;
                        }
                    }

                    return true;
                }
            }

            // Является ли матрица единичной
            public bool IsUnity
            {
                get
                {
                    if (!IsSquared) return false;

                    for (int j = 0; j < Rows; j++)
                    {
                        for (int i = 0; i < Columns; i++)
                        {
                            if ((j == i && data[j, i] != 1) || (j != i && data[j, i] != 0)) return false;
                        }
                    }

                    return true;
                }
            }

            // Является ли матрица диагональной
            public bool IsDiagonal
            {
                get
                {
                    if (!IsSquared) return false;

                    for (int j = 0; j < Rows; j++)
                    {
                        for (int i = 0; i < Columns; i++)
                        {
                            if (j != i && data[j, i] != 0) return false;
                        }
                    }

                    return true;
                }
            }

            // Является ли матрица симметричной
            public bool IsSymmetric
            {
                get
                {
                    if (!IsSquared) return false;

                    for (int j = 0; j < Rows; j++)
                    {
                        for (int i = 0; i < Columns; i++)
                        {
                            if (data[i, j] != data[j, i]) return false;
                        }
                    }

                    return true;
                }
            }


            public static Matrix operator +(Matrix m1, Matrix m2)
            {
                if ((m1.Rows != m2.Rows) || (m1.Columns != m2.Columns))
                    throw new Exception("Сложение матриц разного размера");

                Matrix result = (Matrix) m1.Clone();
                for (int j = 0; j < result.Rows; j++)
                {
                    for (int i = 0; i < result.Columns; i++)
                    {
                        result[j, i] += m2[j, i];
                    }
                }

                return result;
            }

            public static Matrix operator -(Matrix m1, Matrix m2)
            {
                if ((m1.Rows != m2.Rows) || (m1.Columns != m2.Columns))
                    throw new Exception("Ошибка: вычитание матриц разного размера!");

                Matrix result = (Matrix) m1.Clone();
                for (int j = 0; j < result.Rows; j++)
                {
                    for (int i = 0; i < result.Columns; i++)
                    {
                        result[j, i] -= m2[j, i];
                    }
                }

                return result;
            }

            public static Matrix operator *(Matrix m1, double d)
            {
                Matrix result = (Matrix) m1.Clone();
                for (int j = 0; j < result.Rows; j++)
                {
                    for (int i = 0; i < result.Columns; i++)
                    {
                        result[j, i] *= d;
                    }
                }

                return result;
            }

            public static Matrix operator *(Matrix m1, Matrix m2)
            {
                if (m1.Columns != m2.Rows) 
                    throw new Exception("Ошибка: количество столбцов первой матрицы не равно количеству строк второй матрицы!");

                Matrix result = new Matrix(m1.Rows, m2.Columns);

                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m2.Columns; j++)
                    {
                        result[i, j] = 0;

                        for (int k = 0; k < m1.Columns; k++)
                        {
                            result[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                }

                return result;
            }


            public static explicit operator Matrix(double[,] arr)
            { 
                return new Matrix(arr);
            }

            public static explicit operator Matrix(double[][] arr)
            {
                return new Matrix(arr);
            }


            public Matrix Transpose()
            {
                Matrix newMatrix = new Matrix(Columns, Rows);
                for (int j = 0; j < Rows; j++)
                {
                    for (int i = 0; i < Columns; i++)
                    {
                        newMatrix[i, j] = data[j, i];
                    }
                }

                return newMatrix;
            }

            public double Trace()
            {
                if (!IsSquared) throw new Exception("Ошибка: попытка вычислить след не квадратной матрицы!");

                double trace = 0;
                for (int i = 0; i < Columns; i++)
                {
                   trace += data[i, i];
                }
                return trace;
            }


            public override string ToString()
            {
                const int precision = 5;
                int posLength = GetOneNumberLength(precision);
                string asString = "";

                for (int j = 0; j < Rows; j++)
                {
                    for (int i = 0; i < Columns; i++)
                    {
                        asString += Math.Round(data[j, i], precision).ToString().PadRight(posLength + 1);
                    }

                    asString += "\n";
                }

                return asString;
            }

            private int GetOneNumberLength(int precision)
            {
                int maxNumLength = 0;
                for (int j = 0; j < Rows; j++)
                {
                    for (int i = 0; i < Columns; i++)
                    {
                        int numLength = Math.Round(data[j, i], precision).ToString().Length;
                        if (numLength > maxNumLength) maxNumLength = numLength;
                    }
                }

                return maxNumLength;
            }

            public static Matrix GetUnity(int size)
            {
                Matrix unityMatrix = new Matrix(size, size);

                for (int j = 0; j < size; j++)
                {
                    for (int i = 0; i < size; i++)
                    {
                        unityMatrix[j, i] = i == j ? 1 : 0;
                    }
                }

                return unityMatrix;
            }

            public static Matrix GetEmpty(int size)
            {
                Matrix emptyMatrix = new Matrix(size, size);

                for (int j = 0; j < size; j++)
                {
                    for (int i = 0; i < size; i++)
                    {
                        emptyMatrix[j, i] = 0;
                    }
                }

                return emptyMatrix;
            }


            public static Matrix Parse(string s)
            {
                if (TryParse(s, out Matrix result)) return result;
                throw new FormatException("Ошибка: из данной строки невозможно создать матрицу!");
            }

            public static bool TryParse(string s, out Matrix m)
            {
                string[] rows = s.Split(',');
                
                int columnSize = rows.Length;
                if (columnSize == 0)
                {
                    m = new Matrix(0, 0);
                    return false;
                }

                int rowSize = rows[0].Split(' ').Length;
                if (rowSize == 0)
                {
                    m = new Matrix(0, 0);
                    return false;
                }

                Matrix parsedMatrix = new Matrix(columnSize, rowSize);

                int curRowNum = 0;
                foreach (string row in rows)
                {
                    string[] numbersFromOneRow = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbersFromOneRow.Length != rowSize)
                    {
                        m = new Matrix(0, 0);
                        return false;
                    }

                    for (int i = 0; i < numbersFromOneRow.Length; i++)
                    {
                        parsedMatrix[curRowNum, i] = Double.Parse(numbersFromOneRow[i], CultureInfo.InvariantCulture);
                    }

                    curRowNum++;
                }

                m = parsedMatrix;
                return true;
            }

            public object Clone()
            {
                return new Matrix(data);
            }
        }

        static void Main(string[] args)
        {
            //TestMatrixClass();
            StartMainLoop();
        }

        private static void StartMainLoop()
        {
            Dictionary<string, Matrix> matrices = new Dictionary<string, Matrix>();

            bool closeApp = false;

            while (!closeApp)
            {
                Console.Clear();
                PrintMainMenu();

                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        HandleInput(matrices);
                        break;
                    case '2':
                        HandleOperation(matrices);
                        break;
                    case '3':
                        HandleResultInfo(matrices);
                        break;
                    case '0':
                        closeApp = true;
                        break;
                }
            }
        }
        private static void PrintMainMenu()
        {
            Console.WriteLine("Работа с матрицами");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1 – Ввод матрицы");
            Console.WriteLine("2 – Операции");
            Console.WriteLine("3 – Вывод результатов");
            Console.WriteLine("0 - Выход");
            Console.WriteLine("-------------------------------");
        }
        private static void HandleInput(Dictionary<string, Matrix> matrices)
        {
            Console.Clear();
            Console.WriteLine("Введите название новой матрицы:");
            string name = GetNameForNewMatrix(matrices);

            Console.WriteLine("\nВыберите заготовку для новой матрицы:");
            Console.WriteLine("1 - Обычная");
            Console.WriteLine("2 - Нулевая");
            Console.WriteLine("3 - Единичная");
            Console.WriteLine("Любая другая клавиша - Отменить создание");

            try
            {
                Matrix newMatrix;
                int size;
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1':
                        Console.WriteLine("\nВведите данные для матрицы (отделите числа пробелом, а строки запятой):");
                        string data = Console.ReadLine();
                        newMatrix = Matrix.Parse(data);
                        AddMatrixToDictionary(name, newMatrix, matrices);
                        Console.WriteLine("\nМатрица успешно создана!\n");
                        PrintMatrixInfo(newMatrix);
                        break;

                    case '2':
                        Console.WriteLine("\nВведите размер для нулевой матрицы:");
                        size = Int32.Parse(Console.ReadLine());
                        newMatrix = Matrix.GetEmpty(size);
                        AddMatrixToDictionary(name, newMatrix, matrices);
                        Console.WriteLine("\nМатрица успешно создана!\n");
                        PrintMatrixInfo(newMatrix);
                        break;

                    case '3':
                        Console.WriteLine("\nВведите размер для единичной матрицы:");
                        size = Int32.Parse(Console.ReadLine());
                        newMatrix = Matrix.GetUnity(size);
                        AddMatrixToDictionary(name, newMatrix, matrices);
                        Console.WriteLine("\nМатрица успешно создана!\n");
                        PrintMatrixInfo(newMatrix);
                        break;

                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка! {e.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в главное меню");
            Console.ReadKey();
        }

        private static void AddMatrixToDictionary(string name, Matrix newMatrix, Dictionary<string, Matrix> matrices)
        {
            if (matrices.ContainsKey(name))
            {
                matrices[name] = newMatrix;
            }
            else
            {
                matrices.Add(name, newMatrix);
            }
        }

        private static string GetNameForNewMatrix(Dictionary<string, Matrix> matrices)
        {
            string name;
            do
            {
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Нельзя создать матрицу без названия!");
                    continue;
                }
                if (matrices.ContainsKey(name)) Console.WriteLine("Предупреждение: такая матрица уже существует и будет перезаписана!");
            } while (string.IsNullOrEmpty(name));

            return name;
        }

        private static void PrintMatrixInfo(Matrix matrix)
        {
            Console.Write($"Матрица:\n{matrix}");
            Console.WriteLine($"Количество строк: {matrix.Rows}");
            Console.WriteLine($"Количество столбцов: {matrix.Columns}");
            Console.WriteLine($"Является ли нулевой: {matrix.IsEmpty}");
            Console.WriteLine($"Является ли квадратной: {matrix.IsSquared}");

            if (!matrix.IsSquared) return;

            Console.WriteLine($"Размер квадратной матрицы: {matrix.IsSquared}");
            Console.WriteLine($"Является ли единичной: {matrix.IsUnity}");
            Console.WriteLine($"Является ли диагональной: {matrix.IsDiagonal}");
            Console.WriteLine($"Является ли симметричной: {matrix.IsSymmetric}");
            Console.WriteLine($"След матрицы: {matrix.Trace()}");
        }

        private static void HandleOperation(Dictionary<string, Matrix> matrices)
        {
            if (matrices.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Сначала добавьте хотя бы одну матрицу!");
                Console.WriteLine("Нажмите любую клавишу чтобы вернуться в главное меню");
                Console.ReadKey();
                return;
            }


            bool closeMenu = false;

            while (!closeMenu)
            {
                Console.Clear();
                PrintAllMatrices(matrices);
                Console.WriteLine("\nВыберите выполняемую операцию:");
                Console.WriteLine("1 - Транспонирование матрицы");
                Console.WriteLine("2 - Умножение матрицы на число");
                Console.WriteLine("3 - Сложение матриц");
                Console.WriteLine("4 - Вычитание матриц");
                Console.WriteLine("5 - Перемножение матриц");
                Console.WriteLine("Любая другая клавиша - Вернуться в главное меню");

                try
                {
                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                    {
                        case '1':
                            HandleTransposition(matrices);
                            break;
                        case '2':
                            HandleMultiplicationByNumber(matrices);
                            break;
                        case '3':
                            HandleAdding(matrices);
                            break;
                        case '4':
                            HandleSubtraction(matrices);
                            break;
                        case '5':
                            HandleMultiplicationByMatrix(matrices);
                            break;
                        default:
                            closeMenu = true;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nПроизошла ошибка! {e.Message}");
                }

                Console.WriteLine("Нажмите любую клавишу для возврата к меню операций\n");
                Console.ReadKey();
            }
        }

        private static void HandleTransposition(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("\nВведите название транспонируемой матрицы:");
            string operandName = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название новой матрицы:");
            string resultName = GetNameForNewMatrix(matrices);

            AddMatrixToDictionary(resultName, matrices[operandName].Transpose(), matrices);
            Console.WriteLine("\nОперация выполнена!");
        }

        private static string GetNameOfExistingMatrix(Dictionary<string, Matrix> matrices)
        {
            string operandName;
            do
            {
                operandName = Console.ReadLine();
                if (string.IsNullOrEmpty(operandName))
                {
                    Console.WriteLine("Нельзя выбрать матрицу без названия!");
                    continue;
                }

                if (!matrices.ContainsKey(operandName)) Console.WriteLine("Такой матрицы не существует!");
            } while (!matrices.ContainsKey(operandName ?? string.Empty));

            return operandName;
        }

        private static void HandleMultiplicationByNumber(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("\nВведите название умножаемой матрицы:");
            string operandName = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите множитель:");
            double operandNumber = Double.Parse(Console.ReadLine() ?? string.Empty, CultureInfo.InvariantCulture);

            Console.WriteLine("\nВведите название новой матрицы:");
            string resultName = GetNameForNewMatrix(matrices);

            AddMatrixToDictionary(resultName, matrices[operandName]*operandNumber, matrices);
            Console.WriteLine("\nОперация выполнена!");
        }

        private static void HandleAdding(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("\nВведите название первого слагаемого:");
            string operandName1 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название второго слагаемого:");
            string operandName2 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название новой матрицы:");
            string resultName = GetNameForNewMatrix(matrices);

            AddMatrixToDictionary(resultName, matrices[operandName1] + matrices[operandName2], matrices);
            Console.WriteLine("\nОперация выполнена!");
        }

        private static void HandleSubtraction(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("\nВведите название уменьшаемого:");
            string operandName1 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название вычитаемого:");
            string operandName2 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название новой матрицы:");
            string resultName = GetNameForNewMatrix(matrices);

            AddMatrixToDictionary(resultName, matrices[operandName1] - matrices[operandName2], matrices);
            Console.WriteLine("\nОперация выполнена!");
        }

        private static void HandleMultiplicationByMatrix(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("\nВведите название первого множителя:");
            string operandName1 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название второго множителя:");
            string operandName2 = GetNameOfExistingMatrix(matrices);

            Console.WriteLine("\nВведите название новой матрицы:");
            string resultName = GetNameForNewMatrix(matrices);

            AddMatrixToDictionary(resultName, matrices[operandName1] * matrices[operandName2], matrices);
            Console.WriteLine("\nОперация выполнена!");
        }

        private static void PrintAllMatrices(Dictionary<string, Matrix> matrices)
        {
            Console.WriteLine("Список доступных матриц:");
            foreach (KeyValuePair<string, Matrix> matrix in matrices)
            {
                Console.WriteLine($"{matrix.Key} размера {matrix.Value.Rows}х{matrix.Value.Columns}");
            }
        }

        private static void HandleResultInfo(Dictionary<string, Matrix> matrices)
        {
            if (matrices.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Сначала добавьте хотя бы одну матрицу!");
                Console.WriteLine("Нажмите любую клавишу чтобы вернуться в главное меню");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            PrintAllMatrices(matrices);
            Console.WriteLine("\nВводите названия матриц для отображения информации о них или пустую строку для возврата в главное меню:");
            do
            {
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name)) return;

                if (!matrices.ContainsKey(name)) Console.WriteLine("Такой матрицы не существует!");
                else PrintMatrixInfo(matrices[name]);
                Console.WriteLine();
            } while (true);
        }

        // Тестирование работы матриц
        private static void TestMatrixClass()
        {
            // Работа матрицы с поэлементным заполнением
            Matrix test1 = new Matrix(3, 3);
            Console.Write("Значения для матрицы 3х3: ");
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    test1[j, i] = i + j;
                    Console.Write($"{test1[j, i]} ");
                }
            }
            Console.Write($"\nИтоговая матрица:\n{test1}");
            Console.Write($"Размер квадратной матрицы: {test1.IsSquared}\n");
            Console.Write($"Является ли квадратной: {test1.IsSquared}\n");
            Console.Write($"Является ли нулевой: {test1.IsEmpty}\n");
            Console.Write($"Является ли единичной: {test1.IsUnity}\n");
            Console.Write($"Является ли диагональной: {test1.IsDiagonal}\n");
            Console.Write($"Является ли симметричной: {test1.IsSymmetric}\n\n");

            // Работа матрицы из диагонального массива
            double[,] initData2 = {
                {1, 2}, 
                {3, 4}, 
                {5, 6},
            };
            Matrix test2 = new Matrix(initData2);
            Console.Write($"Матрица из прямоугольного массива с помощью конструктора:\n{test2}");
            Matrix testCast2 = (Matrix)initData2;
            Console.Write($"Матрица из прямоугольного массива с помощью преобразования:\n{testCast2}");
            Console.Write($"Количество строк: {test2.Rows}\n");
            Console.Write($"Количество столбцов: {test2.Columns}\n");
            Console.Write($"Является ли квадратной: {test2.IsSquared}\n");
            if (test2.Size == null)
            {
                Console.Write("Не квадратная, Size возвращает null\n\n");
            }

            // Работа матрицы из двумерного массива
            double[][] initData3 = {
                new double[] {10000, 2, 3.12345, 4, -5.555555555},
                new double[] {6, 7654321, 8, 9, 10},
            };
            Matrix test3 = new Matrix(initData3);
            Console.Write($"Матрица из двумерного массива с помощью конструктора:\n{test3}");
            Matrix testCast3 = (Matrix)initData3;
            Console.Write($"Матрица из двумерного массива с помощью преобразования:\n{testCast3}");
            Console.Write($"Количество строк: {test3.Rows}\n");
            Console.Write($"Количество столбцов: {test3.Columns}\n");
            Console.Write($"Транспонированная матрица:\n{test3.Transpose()}");
            try
            {
                Console.Write($"След матрицы: {test2.Trace()}\n");
            }
            catch (Exception e)
            {
                Console.Write($"{e.Message}\n\n");
            }

            // Работа с нулевой и единичной матрицами
            Matrix emptyMatrix = Matrix.GetEmpty(3);
            Console.Write($"Нулевая матрица с размером 3:\n{emptyMatrix}");
            Console.Write($"Является ли нулевой: {emptyMatrix.IsEmpty}\n\n");

            Matrix unityMatrix = Matrix.GetUnity(4);
            Console.Write($"Единичная матрица с размером 4:\n{unityMatrix}");
            Console.Write($"Является ли единичной: {unityMatrix.IsUnity}\n\n");

            Console.ReadKey();
        }
    }
}
