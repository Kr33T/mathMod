using System;

namespace mathMod
{
    class program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задача 5.\nНахождение первоначального распределения транспортной задачи по методу минимального элемента.");
            Console.Write("Введите размерность матрицы и векторов: ");
            uint r = Convert.ToUInt32(Console.ReadLine());
            uint[,] arr = new uint[r, r];
            uint[] m = new uint[r];
            uint[] n = new uint[r];
            uint valueM = 0, valueN = 0;
            uint min = uint.MaxValue;
            int indI = 0, indJ = 0;
            uint[,] raspr = new uint[r, r];
            uint F = 0;
            Console.Write("Введите номер способа, которым хотите инициализировать массивы:\n1.Вручную\n2.Рандом\nОтвет: ");
            int mode = Convert.ToInt32(Console.ReadLine());
            string[] postavki = new string[0];
            //Enter matrix and vectors
            switch (mode)
            {
                case 1:
                    Console.WriteLine("Ввод матрицы:");
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            Console.Write($"Введите элемент {i + 1}{j + 1}: ");
                            arr[i, j] = Convert.ToUInt32(Console.ReadLine());
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Ввод вектора мощности (m):");
                        for (int i = 0; i < m.Length; i++)
                        {
                            Console.Write($"Введите элемент {i + 1}: ");
                            m[i] = Convert.ToUInt32(Console.ReadLine());
                        }
                        Console.WriteLine("Ввод вектора спроса (n):");
                        for (int i = 0; i < n.Length; i++)
                        {
                            Console.Write($"Введите элемент {i + 1}: ");
                            n[i] = Convert.ToUInt32(Console.ReadLine());
                        }
                        for (int i = 0; i < m.Length; i++)
                        {
                            valueM += m[i];
                            valueN += n[i];
                        }
                        if (valueM == valueN)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Вектор мощности (m) и cпроса (n) должны быть равны, повторите ввод!");
                            valueM = 0;
                            valueN = 0;
                        }
                    }
                    break;
                case 2:
                    Random rnd = new Random();
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            arr[i, j] = (uint)rnd.Next(1, 10);
                        }
                    }
                    while (true)
                    {
                        for (int i = 0; i < m.Length; i++)
                        {
                            m[i] = (uint)rnd.Next(1, 10);
                            n[i] = (uint)rnd.Next(1, 10);
                            valueM += m[i];
                            valueN += n[i];
                        }
                        if (valueM == valueN)
                        {
                            break;
                        }
                        else
                        {
                            valueM = 0;
                            valueN = 0;
                        }
                    }
                    break;
            }
            //output
            Console.WriteLine("\nИсходная матрица:");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Вектор мощности (m):");
            for (int i = 0; i < m.Length; i++)
            {
                Console.Write($"{m[i]} ");
            }
            Console.WriteLine("\nВектор спроса (n):");
            for (int i = 0; i < n.Length; i++)
            {
                Console.Write($"{n[i]} ");
            }

            while (true)
            {
                Console.Write("\nХотите изменить элементы матрицы?\nОтвет(y/n): ");
                char otv = Convert.ToChar(Console.ReadLine());
                if (otv.Equals('n') || otv.Equals('т'))
                {
                    break;
                }
                else
                {
                    Console.Write("Введите номера элемента матрицы, который вы хотите изменить\nОтвет:\n");
                    while (true)
                    {
                        int i = Convert.ToInt32(Console.ReadLine()), j = Convert.ToInt32(Console.ReadLine());
                        if (i < r + 1 && j < r + 1)
                        {
                            Console.Write("Введите значение, на которое вы хотите поменять элемент: ");
                            arr[i - 1, j - 1] = Convert.ToUInt32(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введенная размерность не соответствует матрице! Повторите ввод");
                        }
                    }
                }
            }

            Console.Clear();

            //output all
            Console.WriteLine("Матрица тарифов:");
            Console.Write("  ");
            for (int i = 0; i < n.Length; i++)
            {
                Console.Write($"{n[i]} ");
            }
            Console.WriteLine();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write($"{m[i]} ");
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < raspr.GetLength(0); i++)
            {
                for (int j = 0; j < raspr.GetLength(1); j++)
                {
                    raspr[i, j] = 0;
                }
            }

            while (true)
            {
                min = uint.MaxValue;
                valueM = 0;
                valueN = 0;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        if (arr[i, j] < min && raspr[i, j] == 0 && m[i] != 0 && n[j] != 0)
                        {
                            min = arr[i, j];
                            indI = i;
                            indJ = j;
                        }
                    }
                }
                if (n[indJ] != 0 && m[indI] != 0)
                {
                    if(n[indJ] > m[indI])
                    {
                        raspr[indI, indJ] = m[indI];
                        n[indJ] -= m[indI];
                        m[indI] = 0;
                    }
                    else
                    {
                        raspr[indI, indJ] = n[indJ];
                        m[indI] -= n[indJ];
                        n[indJ] = 0;
                    }
                }
                Array.Resize(ref postavki, postavki.Length + 1);
                postavki[postavki.Length - 1] = $"Поставщик №{indI + 1} отправил {raspr[indI, indJ]} единиц продукции клиенту №{indJ + 1}";
                for (int i = 0; i < m.Length; i++)
                {
                    valueM += m[i];
                    valueN += n[i];
                }
                if(valueM == 0 && valueN == 0)
                {
                    break;
                }
            }

            Console.WriteLine("\nИсходная матрица:");

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nМатрица, в которой проставлены элементы, куда прошла поставка:");
            for (int i = 0; i < raspr.GetLength(0); i++)
            {
                for (int j = 0; j < raspr.GetLength(1); j++)
                {
                    Console.Write($"{raspr[i, j]} ");
                }
                Console.WriteLine();
            }

            string[,] aboba = new string[r, r];

            Console.WriteLine("\nИтоговая матрица:");

            for (int i = 0; i < aboba.GetLength(0); i++)
            {
                for (int j = 0; j < aboba.GetLength(1); j++)
                {
                    if(raspr[i, j] != 0)
                    {
                        aboba[i, j] = $"{arr[i, j]}/{raspr[i, j]}";
                    }
                    else
                    {
                        aboba[i, j] = $" {arr[i, j]} ";
                    }
                    Console.Write($"{aboba[i, j]} ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < raspr.GetLength(0); i++)
            {
                for (int j = 0; j < raspr.GetLength(1); j++)
                {
                    if(raspr[i, j] != 0)
                    {
                        F += raspr[i, j] * arr[i, j];
                    }
                }
            }

            Console.WriteLine($"\nF = {F} у.д.е");

            for (int i = 0; i < postavki.Length; i++)
            {
                Console.WriteLine(postavki[i]);
            }
        }
    }
}