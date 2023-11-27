using System;
using System.Text;
using System.Globalization;
using System.Collections;


namespace OOP.LAB6.TEST
{
    class Matrix : IEnumerator, IEnumerable, ICloneable, IComparable, IFormattable, ICollection
    {
        private int row, col;
        private int[] arr;
        static int idx = 1;
        private int id;
        private int position = -1;
        public int Id
        {
            get 
            {
                return id;
            }
            set 
            {
                id = value;
            }
        }

        //IEnumerable  служит для перечисления элементов матрицы, 
        public object Current
        {                 
            get { return arr[position]; }  
        }                                 
        public IEnumerator GetEnumerator() // пронумеровывает каждый эл-т, нужен для IEnumerable
        {
            return (IEnumerator)this;
        }
        public bool MoveNext() // переводит курсор на следующую позицию в матрице и возвращает true, 
        {                      // если курсор не достиг конца матрицы (по сути проверка не вышли ли за границы)
            position++;
            return (position < arr.Length);
        }
        public void Reset() //сбрасывает курсор в начало матрицы
        {
            position = -1;
        }
        //end of numerable

        //clonable  реализует клонирование матрицы
        object ICloneable.Clone() 
        {                         // ICloneable для создания клонов объектов, возвращает ссылку на эту копию
            return Clone();
        }
        
        public Matrix Clone() //создает клон объекта Matrix и возвращает его как результат
        {
            var matr = new Matrix(this);
            return matr;
        }
        //end of clonable

        //IComparable содержит метод CompareTo для сравнения матриц
        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;  // возвращает 1 (указывая, что текущий объект больше null объекта)

            Matrix? otherMatr = obj as Matrix;
            int otherSize = (int)otherMatr.row * (int)otherMatr.col;
            int objSize = (int)this.row * (int)this.col;
            if (otherSize != null) // сравнение размеров матриц. Если количество элементов другой матрицы не равно null,
                 // для сравнения количества элементов текущей матрицы с количеством элементов другой матрицы
                return objSize.CompareTo(otherSize); // и возвращение соответствующего значения.
            else
                throw new ArgumentException("Error");
        }
        //end of compare

        //formatte  содержит метод ToString для форматирования вывода матрицы
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (String.IsNullOrEmpty(format))
            {
                format = "Q";
            }
            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }
            switch (format.ToUpperInvariant())
            {
                case "Q": //как матрица
                    {
                        var sb = new StringBuilder();

                        int count = 0;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++, count++)
                            {
                                sb.Append(arr[count].ToString() + " ");
                                if (j != col - 1)
                                {
                                    sb.Append('\t');
                                }
                            }
                            sb.AppendLine();
                        }
                        return sb.ToString();
                    }
                case "S": //в одну строку
                    {
                        string matrixString = "";
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                matrixString += arr[i * col + j].ToString() + "  ";
                                if (j != col - 1)
                                {
                                    matrixString += "\t";
                                }
                            }
                        }
                        return matrixString;
                    }
                default:
                    throw new FormatException(String.Format("{0} формат не поддерживается", format));
            }
        }
        //end of formatte

        //collection
        public int Count => ((ICollection)arr).Count;
        // Свойство Count возвращает количество элементов в массиве arr.
        public bool IsSynchronized => arr.IsSynchronized;
        //Свойство IsSynchronized сообщает о том, является ли доступ к массиву arr синхронизирован или нет.
        public object SyncRoot => arr.SyncRoot;  
        public void CopyTo(Array array, int index) 
        {// копирует элементы матрицы arr в другой массив, начиная с указанного индекса
            arr.CopyTo(array, index);
        }
        //end of collection
       
        // Конструкторы 
        public Matrix() : this(1, 3, 0) // по умолчанию
        {
            Console.WriteLine($"Создана матрица {id}.\n");
        }
        public Matrix(int Row, int Col, int num) : this(Row, Col, new int[Row * Col].Select(x => num).ToArray()) 
        {

        }
        public Matrix(int Razmer, int num) : this(Razmer, Razmer, new int[Razmer * Razmer].Select(x => num).ToArray())
        {
            Console.WriteLine($"Создана квадратная матрица {id}.\n");
        }
        public Matrix(int Row, int Col, int[] array)
        {
            if (Row == 0 && Col == 0)
            {
                throw new Exception("Неверные параметры матрицы!");
            }
            row = Row;
            col = Col;
            arr = new int[row * col];
            for (int i = 0; i < row * col; i++)
            {
                arr[i] = array[i];
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} с заданным размером.\n");
        }
        public Matrix(Matrix mat) // конструктор копирования
        {
            row = mat.Row;
            col = mat.Col;
            arr = new int[row * col];
            Array.Copy(mat.arr, arr, row * col);
            Console.WriteLine("Создана матрица(копия).\n");
        }
        public Matrix(int[,] array) //Копирование по массиву
        {
            row = array.GetLength(0); // передает количество строк и столбцов 
            col = array.GetLength(1);
            arr = new int[row * col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arr[i * col + j] = array[i, j];
                }
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} с заданным размером.\n");
        }
        public Matrix(int Row, int Col, in Func<int, int, int> func)
        {   //Делегаты используются для передачи методов в качестве параметров к другим методам.
            //Cлово in перед параметром означает, что передаваемый объект функции будет только для чтения

            if (Row == 0 && Col == 0)
            {
                throw new Exception("Неверные параметры матрицы!");
            }
            row = Row;
            col = Col;
            arr = new int[row * col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arr[i * col + j] = func(i, j);
                }
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} по заданной функции.\n");
        } 
       // Свойства
        public int Row
        {
            get
            {
                return row;
            }
        }
        public int Col
        {
            get 
            {
                return col; 
            }
        }
        public int this[int Row, int Col]
        {
            get // используется для получения значения элемента массива по заданным индексам
            {
                if (Row >= row || Row < 0 || Col >= col || Col < 0)
                {
                    throw new IndexOutOfRangeException("Выход за пределы массва!");
                }
                return arr[Row * col + Col];
            }
            set // используется для установки значения элемента массива по заданным индексам
            {
                if (Row >= row || Row < 0 || Col >= col || Col < 0)
                {
                    throw new IndexOutOfRangeException("Выход за пределы массва!");
                }
                arr[Row * col + Col] = value;
            }
        }
        // Функции
        public bool OppSum(Matrix a)
        {
            return a.row == row && a.col == col;
        }
        public bool OppMul(Matrix a)
        {
            return a.col == row;
        }
        public void Output()
        {
            int count = 0;
            for (int i = 0; i < row; i++) // перебор строк
            {
                for (int j = 0; j < col; j++, count++) // перебор столбцов
                {
                    Console.Write($"{arr[count]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public int Max()
        {
            int max = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }
        public int Min()
        {
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (min > arr[i])
                    min = arr[i];
            }
            return min;
        }

        // Операторы
        // A+B
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.OppSum(b) == false)
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя складывать");
            }

            int Row = a.Row;
            int Col = a.Col;
            int[] array = new int[Row * Col];
            for (int i = 0; i < Row * Col; i++)
            {
                array[i] = a.arr[i] + b.arr[i];
            }

            return new Matrix(Row, Col, array);
        }

        // A-B
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.OppSum(b) == true)
            {
                int Row = a.Row;
                int Col = a.Col;
                int[] array = new int[Row * Col];
                for (int i = 0; i < Row * Col; i++)
                {
                    array[i] = a.arr[i] - b.arr[i];
                }

                return new Matrix(Row, Col, array);
            }
            else 
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя вычитать"); 
            }
        }

        // A*B
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.OppMul(b) == true)
            {
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
            else 
            { 
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя перемножать");
            }
        }

        // A*k
        public static Matrix operator *(Matrix a, int x)
        {
            int Row = a.Row;
            int Col = a.Col;
            int[] array = new int[Row * Col];
            for (int i = 0; i < Row * Col; i++)
            {
                array[i] = a.arr[i] * x;
            }

            return new Matrix(Row, Col, array);
        }
    }
}