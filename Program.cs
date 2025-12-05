using System;
using System.Globalization;
using System.Text;

namespace MatrixPolymorphism
{
    /// <summary>
    /// Базовий абстрактний клас.
    /// Визначає інтерфейс для роботи з матрицями різної вимірності.
    /// </summary>
    public abstract class MatrixBase
    {
        /// <summary>
        /// Назва типу структури (для відображення).
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Віртуальний метод для виведення структури на екран.
        /// </summary>
        public virtual void Display()
        {
            Console.WriteLine($"\n--- {Name} ---");
        }

        /// <summary>
        /// Абстрактний метод заповнення даними з клавіатури.
        /// </summary>
        public abstract void FillFromKeyboard();

        /// <summary>
        /// Абстрактний метод заповнення випадковими числами.
        /// </summary>
        /// <param name="rnd">Екземпляр генератора випадкових чисел.</param>
        public abstract void FillRandom(Random rnd);

        /// <summary>
        /// Абстрактний метод пошуку мінімального елемента.
        /// </summary>
        /// <returns>Найменше значення в матриці.</returns>
        public abstract double FindMin();

        // Допоміжний метод для безпечного зчитування double
        protected double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string input = Console.ReadLine();
                // Дозволяємо вводити і крапку, і кому
                if (!string.IsNullOrWhiteSpace(input))
                {
                    input = input.Replace(',', '.');
                    if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    {
                        return result;
                    }
                }
                Console.Write("Помилка. Введіть число (напр. -5.2): ");
            }
        }
    }

    /// <summary>
    /// Клас двовимірної матриці розміром 3x3.
    /// </summary>
    public class Matrix2D : MatrixBase
    {
        // Фіксований розмір 3x3 згідно з умовою
        private readonly double[,] _data = new double[3, 3];

        public Matrix2D()
        {
            Name = "Матриця [3x3]";
        }

        public override void FillFromKeyboard()
        {
            Console.WriteLine($"Заповніть {Name} (9 елементів):");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _data[i, j] = ReadDouble($"Елемент [{i},{j}]: ");
                }
            }
        }

        public override void FillRandom(Random rnd)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Випадкові числа від -10.0 до 10.0
                    _data[i, j] = Math.Round(rnd.NextDouble() * 20 - 10, 2);
                }
            }
        }

        public override double FindMin()
        {
            double min = double.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_data[i, j] < min) min = _data[i, j];
                }
            }
            return min;
        }

        public override void Display()
        {
            base.Display();
            for (int i = 0; i < 3; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{_data[i, j],7:F2} ");
                }
                Console.WriteLine("|");
            }
        }
    }

    /// <summary>
    /// Клас тривимірної матриці розміром 3x3x3.
    /// </summary>
    public class Matrix3D : MatrixBase
    {
        // Фіксований розмір 3x3x3 згідно з умовою
        private readonly double[,,] _data = new double[3, 3, 3];

        public Matrix3D()
        {
            Name = "Матриця [3x3x3]";
        }

        public override void FillFromKeyboard()
        {
            Console.WriteLine($"Заповніть {Name} (27 елементів):");
            for (int z = 0; z < 3; z++)
            {
                Console.WriteLine($"Шар (Z) {z}:");
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _data[z, i, j] = ReadDouble($"  Елемент [{z},{i},{j}]: ");
                    }
                }
            }
        }

        public override void FillRandom(Random rnd)
        {
            for (int z = 0; z < 3; z++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _data[z, i, j] = Math.Round(rnd.NextDouble() * 20 - 10, 2);
                    }
                }
            }
        }

        public override double FindMin()
        {
            double min = double.MaxValue;
            // Прохід по всіх вимірах
            for (int z = 0; z < 3; z++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_data[z, i, j] < min) min = _data[z, i, j];
                    }
                }
            }
            return min;
        }

        public override void Display()
        {
            base.Display();
            for (int z = 0; z < 3; z++)
            {
                Console.WriteLine($"Шар Z={z}:");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write("  |");
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($"{_data[z, i, j],7:F2} ");
                    }
                    Console.WriteLine("|");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування виводу
            Console.OutputEncoding = Encoding.UTF8;
            
            // Єдиний екземпляр Random для всієї програми
            Random rnd = new Random();

            Console.WriteLine("=== Лабораторна робота №5: Поліморфізм (2D та 3D Матриці) ===");

            int n = ReadInt("Введіть кількість об'єктів для створення: ");

            // Масив посилань на базовий клас (демонстрація поліморфізму)
            MatrixBase[] matrices = new MatrixBase[n];

            // 1. Створення об'єктів
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n[Створення об'єкту {i + 1} з {n}]");
                Console.WriteLine("1. Двовимірна матриця (3x3)");
                Console.WriteLine("2. Тривимірна матриця (3x3x3)");
                int type = ReadInt("Виберіть тип (1 або 2): ");

                // Створення конкретного екземпляра
                if (type == 1)
                    matrices[i] = new Matrix2D();
                else
                    matrices[i] = new Matrix3D();

                // Вибір способу заповнення
                Console.WriteLine("Спосіб заповнення:");
                Console.WriteLine("1. Випадкові числа");
                Console.WriteLine("2. Ввід з клавіатури");
                int fillType = ReadInt("Ваш вибір (1 або 2): ");

                if (fillType == 1)
                {
                    // Поліморфний виклик FillRandom
                    matrices[i].FillRandom(rnd);
                    Console.WriteLine("Заповнено випадковими числами.");
                }
                else
                {
                    // Поліморфний виклик FillFromKeyboard
                    matrices[i].FillFromKeyboard();
                }
            }

            Console.WriteLine("\n================ РЕЗУЛЬТАТИ ================");
            
            // 2. Обробка масиву
            for (int i = 0; i < n; i++)
            {
                // Поліморфний виклик відображення
                matrices[i].Display();

                // Поліморфний виклик пошуку мінімуму
                double minVal = matrices[i].FindMin();

                Console.WriteLine($"-> Мінімальний елемент: {minVal:F2}");
            }

            Console.WriteLine("\nРоботу завершено. Натисніть Enter...");
            Console.ReadLine();
        }

        /// <summary>
        /// Безпечне зчитування цілого числа.
        /// </summary>
        static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                Console.Write("Помилка! Введіть ціле число: ");
            }
        }
    }
}
