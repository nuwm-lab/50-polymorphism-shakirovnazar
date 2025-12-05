using System;
using System.Text;
using System.Globalization;

namespace MatrixPolymorphism
{
    /// <summary>
    /// Базовий абстрактний клас для матриць.
    /// Виконує вимогу: батьківський клас з віртуальними/абстрактними методами.
    /// </summary>
    public abstract class MatrixBase
    {
        /// <summary>
        /// Назва типу матриці (для виводу).
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Абстрактний метод для обчислення визначника (детермінанта).
        /// Це основний метод для демонстрації поліморфізму.
        /// </summary>
        /// <returns>Значення визначника.</returns>
        public abstract double CalculateDeterminant();

        /// <summary>
        /// Віртуальний метод для виведення матриці на екран.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine($"--- {Name} ---");
        }
    }

    /// <summary>
    /// Клас двовимірної квадратної матриці (2x2).
    /// </summary>
    public class Matrix2D : MatrixBase
    {
        // Приватні поля з використанням конвенції _camelCase
        private double[,] _data;

        /// <summary>
        /// Конструктор матриці 2x2.
        /// </summary>
        public Matrix2D()
        {
            Name = "Матриця 2x2";
            _data = new double[2, 2];
        }

        /// <summary>
        /// Метод для заповнення матриці даними.
        /// Не віртуальний, специфічний для цього класу.
        /// </summary>
        public void Fill(double a11, double a12, double a21, double a22)
        {
            _data[0, 0] = a11; _data[0, 1] = a12;
            _data[1, 0] = a21; _data[1, 1] = a22;
        }

        /// <summary>
        /// Обчислення визначника для 2x2: ad - bc.
        /// </summary>
        public override double CalculateDeterminant()
        {
            return (_data[0, 0] * _data[1, 1]) - (_data[0, 1] * _data[1, 0]);
        }

        public override void Display()
        {
            base.Display();
            Console.WriteLine($"| {_data[0, 0],6:F2} {_data[0, 1],6:F2} |");
            Console.WriteLine($"| {_data[1, 0],6:F2} {_data[1, 1],6:F2} |");
        }
    }

    /// <summary>
    /// Клас тривимірної квадратної матриці (3x3).
    /// </summary>
    public class Matrix3D : MatrixBase
    {
        private double[,] _data;

        public Matrix3D()
        {
            Name = "Матриця 3x3";
            _data = new double[3, 3];
        }

        public void Fill(double[] values)
        {
            if (values.Length < 9) throw new ArgumentException("Need 9 values for 3x3 matrix");
            int k = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _data[i, j] = values[k++];
        }

        /// <summary>
        /// Обчислення визначника для 3x3 (правило трикутника або Саррюса).
        /// </summary>
        public override double CalculateDeterminant()
        {
            return _data[0, 0] * (_data[1, 1] * _data[2, 2] - _data[1, 2] * _data[2, 1]) -
                   _data[0, 1] * (_data[1, 0] * _data[2, 2] - _data[1, 2] * _data[2, 0]) +
                   _data[0, 2] * (_data[1, 0] * _data[2, 1] - _data[1, 1] * _data[2, 0]);
        }

        public override void Display()
        {
            base.Display();
            for (int i = 0; i < 3; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{_data[i, j],6:F2} ");
                }
                Console.WriteLine("|");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування культури один раз для всієї програми (виправлення зауваження №3)
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture; 

            Console.WriteLine("=== Лабораторна робота №5: Поліморфізм (Матриці) ===");

            int n = ReadInt("Введіть кількість матриць для створення: ");
            
            // Масив посилань на базовий клас (динамічний поліморфізм)
            MatrixBase[] matrices = new MatrixBase[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Створення об'єкту №{i + 1} ---");
                Console.WriteLine("1. Матриця 2x2");
                Console.WriteLine("2. Матриця 3x3");
                int type = ReadInt("Виберіть тип (1 або 2): ");

                if (type == 1)
                {
                    var m2 = new Matrix2D();
                    Console.WriteLine("Введіть 4 елементи (зліва направо, зверху вниз):");
                    m2.Fill(ReadDouble("a11: "), ReadDouble("a12: "), 
                            ReadDouble("a21: "), ReadDouble("a22: "));
                    matrices[i] = m2;
                }
                else
                {
                    var m3 = new Matrix3D();
                    Console.WriteLine("Введіть 9 елементів:");
                    double[] vals = new double[9];
                    for(int k=0; k<9; k++) vals[k] = ReadDouble($"el_{k+1}: ");
                    m3.Fill(vals);
                    matrices[i] = m3;
                }
            }

            Console.WriteLine("\n\n=== РЕЗУЛЬТАТИ (Поліморфний виклик методів) ===");
            
            // Демонстрація поліморфізму
            for (int i = 0; i < matrices.Length; i++)
            {
                // Виклик віртуального методу Display()
                matrices[i].Display();

                // Виклик абстрактного методу CalculateDeterminant()
                // Програма сама визначає, яку формулу використати (2x2 чи 3x3)
                double det = matrices[i].CalculateDeterminant();
                
                Console.WriteLine($"Визначник (Determinant): {det:F4}");
                Console.WriteLine();
            }

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        /// <summary>
        /// Безпечне зчитування числа double з консолі.
        /// Враховано зауваження про null та CultureInfo.
        /// </summary>
        static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string? input = Console.ReadLine();
                
                // Перевірка на null або порожній рядок
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Ввід не може бути порожнім. Спробуйте ще раз: ");
                    continue;
                }

                // Оскільки CultureInfo.CurrentCulture встановлено в InvariantCulture, 
                // очікується крапка як роздільник. Replace тут вже не потрібен, 
                // але користувачі можуть помилково вводити кому.
                // Для надійності можна замінити кому на крапку:
                input = input.Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    return result;
                }

                Console.Write("Помилка! Введіть дійсне число (наприклад, 2.5): ");
            }
        }

        /// <summary>
        /// Безпечне зчитування цілого числа.
        /// </summary>
        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result))
                {
                    return result;
                }
                Console.Write("Помилка! Введіть ціле число: ");
            }
        }
    }
}
