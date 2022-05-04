using System;

namespace mathMod
{
    class program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Задача 5.\nНахождение первоначального распределения транспортной задачи по методу минимального элемента.");//постановка задачи
                uint r1, r2;//размерности матрицы и векторов
                while (true)//ввод потребителей(размерности по столбцам)
                {
                    try
                    {
                        Console.Write("Введите количество потребителей: ");
                        r1 = Convert.ToUInt32(Console.ReadLine());
                        if (r1 < 2)
                        {
                            Console.WriteLine("Потребителей должно быть не менее двух! Повторите ввод");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                while (true)//ввод поставщиков(размерности по строкам)
                {
                    try
                    {
                        Console.Write("Введите количество поставщиков: ");
                        r2 = Convert.ToUInt32(Console.ReadLine());
                        if (r2 < 2)
                        {
                            Console.WriteLine("Поставщиков должно быть не менее двух! Повторите ввод");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                char otv;//переменная для диалога
                uint[,] arr = new uint[r2, r1];//матрица затрат
                uint[] m = new uint[r2];//вектор мощности
                uint[] n = new uint[r1];//вектор спроса
                uint valueM = 0, valueN = 0;//переменные для подсчета суммы
                uint min = uint.MaxValue;//переменная для поиска минимального элемента
                int indI = 0, indJ = 0;//переменные для хранения индекса минимального элемента
                uint[,] raspr = new uint[r2, r1];//массив в котором хранятся значения, куда и сколько поставили продукции
                uint F = 0;//целевая функция
                metka: int mode;//переменная для ввода способа заполнения массива и векторов
                while (true)//ввод способа инициализации
                {
                    try
                    {
                        Console.Write("Каким способом вы хотите ввести данные:\n1.Вручную\n2.Рандом\nОтвет(1/2): ");
                        mode = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                string[] postavki = new string[0];//массив строк в котором прописано, кто с кем заключил договор
                string[,] itog = new string[r2, r1];//матрица, которая схожа с исходной, но в ней проставлены поставки через слеш
                switch (mode)
                {
                    case 1://инициализация матрицы и векторов вручную
                        Console.WriteLine("Ввод таблицы затрат на перевозку продукции:");
                        for (int i = 0; i < arr.GetLength(0); i++)
                        {
                            for (int j = 0; j < arr.GetLength(1); j++)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write($"Введите стоимость перевозки от {i + 1} поставщика  к {j + 1} потребителю: ");
                                        arr[i, j] = Convert.ToUInt32(Console.ReadLine());
                                        break;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Введены некорректные данные!");
                                    }
                                }
                            }
                        }
                        while (true)
                        {
                            Console.WriteLine("Ввод данных о количестве продукции поставщиков (m):");
                            for (int i = 0; i < m.Length; i++)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write($"Введите количество продукции у {i + 1} поставщика: ");
                                        m[i] = Convert.ToUInt32(Console.ReadLine());
                                        break;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Введены некорректные данные!");
                                    }
                                }
                            }
                            Console.WriteLine("Ввод данных о запрашиваемом количестве продукции потребителями(n):");
                            for (int i = 0; i < n.Length; i++)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write($"Введите количество необходимой продукции для {i + 1} потребителя: ");
                                        n[i] = Convert.ToUInt32(Console.ReadLine());
                                        break;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Введены некорректные данные!");
                                    }
                                }
                            }
                            for (int i = 0; i < m.Length; i++)
                            {
                                valueM += m[i];
                            }
                            for (int i = 0; i < n.Length; i++)
                            {
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
                    case 2://инициализация матрицы и векторов рандомными значениями
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
                                valueM += m[i];
                            }
                            for (int i = 0; i < n.Length; i++)
                            {
                                n[i] = (uint)rnd.Next(1, 10);
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
                    default:
                        Console.WriteLine("Выбор предоставлен лишь из двух способов!");
                        goto metka;
                        break;
                }
                //вывод таблицы затрат и векторов
                Console.WriteLine("\nТаблица затрат:");
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

                //Изменение матрицы
                while (true)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write("\nХотите изменить данные о таблице?\nЕсли нет, то нажмите на кнопку N (на англ) на клавиатуре\nЕсли да, то можете нажать на любую другую кнопку\nОтвет: ");
                            otv = Convert.ToChar(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Введены некорректные данные!");
                        }
                    }
                    if (otv.Equals('n') || otv.Equals('т') || otv.Equals('N') || otv.Equals('Т'))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Введите номер поставщика, затем номер потребителя, который вы хотите изменить\nОтвет:\n");
                        while (true)
                        {
                            int i = Convert.ToInt32(Console.ReadLine()), j = Convert.ToInt32(Console.ReadLine());
                            if (i < r2 + 1 && j < r1 + 1)
                            {
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Введите новое значение: ");
                                        uint temp = Convert.ToUInt32(Console.ReadLine());
                                        if (temp != 0)
                                        {
                                            Console.WriteLine($"Вы изменили {i} {j} ячейку таблицы c {arr[i - 1, j - 1]} на {temp}");
                                            arr[i - 1, j - 1] = temp;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Вы ввели нулевое значение, а их быть не должно! Повторите ввод");
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Введены некорректные данные!");
                                    }
                                }
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

                //вывод матрицы тарифов
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

                //алгоритм решения
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
                        if (n[indJ] > m[indI])
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
                    //Договор был заключен между {indI + 1} поставщиком и {indJ + 1} потребителем на {raspr[indI, indJ]} единиц продукции
                    for (int i = 0; i < m.Length; i++)
                    {
                        valueM += m[i];
                    }
                    for (int i = 0; i < n.Length; i++)
                    {
                        valueN += n[i];
                    }
                    if (valueM == 0 && valueN == 0)
                    {
                        break;
                    }
                }

                //вывод матрицы, в которой через слеш написаны поставки
                Console.WriteLine("\nПервоначальное распределение:");

                for (int i = 0; i < itog.GetLength(0); i++)
                {
                    for (int j = 0; j < itog.GetLength(1); j++)
                    {
                        if (raspr[i, j] != 0)
                        {
                            itog[i, j] = $"{arr[i, j]}/{raspr[i, j]}";
                        }
                        else
                        {
                            itog[i, j] = $" {arr[i, j]} ";
                        }
                        Console.Write($"{itog[i, j]} ");
                    }
                    Console.WriteLine();
                }

                //подсчет и вывод целевой функции
                for (int i = 0; i < raspr.GetLength(0); i++)
                {
                    for (int j = 0; j < raspr.GetLength(1); j++)
                    {
                        if (raspr[i, j] != 0)
                        {
                            F += raspr[i, j] * arr[i, j];
                        }
                    }
                }

                Console.WriteLine($"\nF = {F} у.д.е");

                //вывод строк, кто и куда поставил какое количество продукции
                for (int i = 0; i < postavki.Length; i++)
                {
                    Console.WriteLine(postavki[i]);
                }

                //повтор выполнения программы
                while (true)
                {
                    try
                    {
                        Console.Write("\nХотите повторить выполнение программы?\nЕсли да, то нажмите на кнопку Y (на англ) на клавиатуре\nЕсли нет, то можете нажать на любую другую кнопку\nОтвет: ");
                        otv = Convert.ToChar(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                if(!(otv.Equals('Y') || otv.Equals('y') || otv.Equals('н') || otv.Equals('Н')))
                {
                    Console.WriteLine("Программу выполнил студент группы 31П\nМорозов Андрей");
                    break;
                }
                else
                {
                    Console.Clear();
                }
            }
        }
    }
}