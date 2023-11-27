using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace OOP.LAB6.TEST
{
    internal class Program
    {
        static Matrix Sum(Matrix a, Matrix b)
        {
            if (!a.OppSum(b))
            {
                throw new Exception($"Матрицы {a.Id} и {b.Id} нельзя складывать");
            }
            int Row = a.Row; // создаем переменные для количества строк и столбцов в матрицах `a` и `b`
                             // и определяем новый двумерный массив `array` для хранения результата.
            int Col = a.Col;
            int[,] array = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            { 
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] + b[i, j];
                }
            }
            return new Matrix(array);
        }
        static Matrix Sub(Matrix a, Matrix b)
        {
            if (!a.OppSum(b))
            {
                throw new Exception($"Матрицы {a.Id} и {b.Id} нельзя вычитать");
            }
            int Row = a.Row;
            int Col = a.Col;
            int[,] array = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] - b[i, j];
                }
            }
            return new Matrix(array);
        }
        static Matrix Mul(Matrix a, Matrix b)
        {
            if (!a.OppMul(b))
            {
                throw new Exception($"Матрицы {a.Id} и {b.Id} нельзя перемножать");
            }
            int Row = a.Row;
            int Col = b.Col;
            int[,] array = new int[Row, Col];
            for (int row1 = 0; row1 < a.Row; row1++)
            {
                for (int col2 = 0; col2 < b.Col; col2++)
                {
                    for (int i = 0; i < b.Row; i++)
                    {
                        array[row1, col2] += a[row1, i] * b[i, col2];
                    }
                }
            }
            return new Matrix(array);
        }

        static Matrix MulOnSkul(Matrix a, int k)
        {
            int Row = a.Row;
            int Col = a.Col;
            int[,] array = new int[Row, Col]; // двумерный массив

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] * k;
                }
            }
            return new Matrix(array);
        }

        public static void Main(string[] args)
        {
            try
            {
                Matrix square = new Matrix(3, 5);
                Console.WriteLine(square);

                int rows = 3;
                int cols = 4;
                int maxValue = 5;
                Matrix m1 = new Matrix(rows, cols, maxValue);
                Random rnd = new Random();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        m1[i, j] = rnd.Next(maxValue + 1);
                    }
                }
                 Console.WriteLine(m1);

                Matrix m2 = new Matrix(4, 3, 3);
                Console.WriteLine(m2.ToString("Q", CultureInfo.CurrentCulture));

                int x = m1[0, 1];
                Console.WriteLine("Значение элемента матрицы m1 [0,1] ");
                Console.WriteLine(x);

                Matrix m3 = m1 * m2;
                Console.WriteLine(m3.ToString("Q", CultureInfo.CurrentCulture));

                Matrix m4 = Mul(m1, m2);
                Console.WriteLine(m4.ToString("Q", CultureInfo.CurrentCulture));


                Console.WriteLine("Проверка интерфейсов!");
                Matrix m5 = new Matrix(3, 3, 1);
                Console.WriteLine(m5.ToString("Q", CultureInfo.CurrentCulture));



                Console.WriteLine("Проверка numerable"); 
                foreach (var i in m5) 
                {
                    Console.Write(" {0}", i);
                }
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Проверка Clone");
                m5 = m1.Clone();
                Console.WriteLine(m5.ToString("Q", CultureInfo.CurrentCulture));

                Console.WriteLine("Проверка CompareTo");
                if (m1.CompareTo(m5) < 0) 
                    Console.WriteLine("m1 < m5");
                else if (m1.CompareTo(m5) == 0)
                    Console.WriteLine("m1 = m5");
                else
                    Console.WriteLine("m1 > m5");

                Console.WriteLine("Проверка formattable");
                Console.WriteLine(m5.ToString("S", CultureInfo.CurrentCulture)); 
                Console.WriteLine();
                Console.WriteLine(m5.ToString("Q", CultureInfo.CurrentCulture));
                Console.WriteLine();

                Console.WriteLine("Проверка collection");
                Console.WriteLine(m5.Count); 
            }
            catch (Exception mimi)
            {
                Console.WriteLine(mimi);
            }
        }
    }
}