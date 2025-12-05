using System;
using System.Collections.Generic;
using System.Text;

// ---------------------------------------------------------------------------
// Базовий абстрактний клас
// Виконує вимогу ЛР №5: "описати батьківський клас та використати віртуальні методи"
// ---------------------------------------------------------------------------
public abstract class Function
{
    // Віртуальна властивість для амплітуди (може бути перевизначена)
    public virtual double Amplitude { get; protected set; }

    // Віртуальний метод для обчислення значення функції в точці x
    public abstract double Evaluate(double x);

    // Віртуальний метод для виведення інформації про об'єкт
    public virtual void Show()
    {
        Console.Write("Абстрактна функція");
    }
}

// ---------------------------------------------------------------------------
// Похідний клас: Функція Sin(ax + b)
// Виконує вимогу ЛР №5: "описати похідний клас"
// ---------------------------------------------------------------------------
public class SinFunction : Function
{
    // Поля коефіцієнтів
    private double A;
    private double a;
    private double b;

    // Конструктор
    public SinFunction(double A, double a, double b)
    {
        this.A = A;
        this.a = a;
        this.b = b;
        
        // Встановлюємо значення властивості базового класу
        this.Amplitude = Math.Abs(A);
    }

    // Перевизначення (override) віртуального методу Evaluate
    public override double Evaluate(double x)
    {
        return A * Math.Sin(a * x + b);
    }

    // Перевизначення (override) віртуального методу Show
    public override void Show()
    {
        // Використовуємо інтерполяцію для гарного виводу: y = 5.00 * sin(2.00x + 1.50)
        Console.WriteLine($"y = {A:F2} * sin({a:F2} * x + {b:F2})  (Амплітуда: {Amplitude:F2})");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Налаштування кодування для коректного відображення кирилиці
        Console.OutputEncoding = Encoding.UTF8;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        Console.WriteLine("--- Лабораторна робота №5 (Варіант 6) ---");
        Console.WriteLine("Тема: Віртуальні функції та поліморфізм\n");

        int n = ReadInt("Введіть кількість функцій (n): ");

        // Виконання вимоги ЛР №5: "Динамічне створення об’єктів та вказівники на екземпляр класу"
        // У C# масив об'єктів базового типу Function[] зберігає посилання.
        Function[] functions = new Function[n];

        // Заповнення масиву
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\nВведення даних для функції №{i + 1}:");
            double A = ReadDouble("  A = ");
            double a = ReadDouble("  a = ");
            double b = ReadDouble("  b = ");

            // Поліморфізм: записуємо об'єкт похідного класу SinFunction у змінну типу Function
            functions[i] = new SinFunction(A, a, b);
        }

        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine("Список введених функцій:");
        
        // Демонстрація поліморфізму: викликаємо метод Show(), який визначено у базовому класі,
        // але виконається реалізація з похідного класу SinFunction.
        for (int i = 0; i < n; i++)
        {
            Console.Write($"{i + 1}. ");
            functions[i].Show();
        }

        // --- Основна логіка завдання (з попередніх вимог) ---
        
        // 1. Пошук максимальної амплітуди (використовуючи властивість базового класу)
        double maxAmp = -1;
        foreach (var f in functions)
        {
            if (f.Amplitude > maxAmp)
                maxAmp = f.Amplitude;
        }
        Console.WriteLine($"\nМаксимальна амплітуда: {maxAmp:F4}");

        // 2. Введення точки X
        double x = ReadDouble("Введіть значення x для оцінки: ");

        // 3. Пошук найкращої функції серед тих, що мають максимальну амплітуду
        Function bestFunc = null;
        double maxVal = double.MinValue;
        double epsilon = 1e-9;
        bool found = false;

        Console.WriteLine("\nАналіз значень в точці x:");
        
        for (int i = 0; i < n; i++)
        {
            // Перевірка на рівність float/double з похибкою
            if (Math.Abs(functions[i].Amplitude - maxAmp) < epsilon)
            {
                // Поліморфний виклик Evaluate(x)
                double currentVal = functions[i].Evaluate(x);
                Console.Write($"  Функція №{i + 1}: y({x}) = {currentVal:F4} ... ");

                if (!found || currentVal > maxVal)
                {
                    maxVal = currentVal;
                    bestFunc = functions[i];
                    found = true;
                    Console.WriteLine("Новий максимум!");
                }
                else
                {
                    Console.WriteLine("");
                }
            }
        }

        // Результат
        Console.WriteLine("------------------------------------------------");
        if (bestFunc != null)
        {
            Console.WriteLine("РЕЗУЛЬТАТ:");
            Console.WriteLine("Функція з максимальною амплітудою та найбільшим значенням в точці x:");
            bestFunc.Show(); // Поліморфний виклик
            Console.WriteLine($"Значення y({x}) = {maxVal:F4}");
        }
        else
        {
            Console.WriteLine("Функції не знайдено.");
        }

        Console.ReadKey();
    }

    // --- Допоміжні методи для зчитування ---
    static double ReadDouble(string msg)
    {
        Console.Write(msg);
        while (true)
        {
            if (double.TryParse(Console.ReadLine().Replace(',', '.'), out double res)) return res;
            Console.Write("Помилка. Введіть число (напр. 2.5): ");
        }
    }

    static int ReadInt(string msg)
    {
        Console.Write(msg);
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int res) && res > 0) return res;
            Console.Write("Помилка. Введіть ціле число > 0: ");
        }
    }
}
