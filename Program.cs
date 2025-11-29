using System;

public class Matrix2D
{
    protected const int SIZE = 3;

    private readonly int[,] _data;
    private static readonly Random _rnd = new Random();

    public Matrix2D()
    {
        _data = new int[SIZE, SIZE];
    }

    public virtual void Input()
    {
        Console.WriteLine("Введення матриці 3x3:");

        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
            {
                Console.Write($"[{i},{j}] = ");
                while (!int.TryParse(Console.ReadLine(), out _data[i, j]))
                    Console.Write("Помилка. Введіть число: ");
            }
    }

    public virtual void RandomFill()
    {
        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                _data[i, j] = _rnd.Next(-50, 51);
    }

    public virtual void Show()
    {
        Console.WriteLine("\nМатриця 2D:");
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
                Console.Write($"{_data[i, j],4}");
            Console.WriteLine();
        }
    }

    public virtual int MinElement()
    {
        int min = _data[0, 0];

        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                if (_data[i, j] < min)
                    min = _data[i, j];

        return min;
    }
}


public class Matrix3D : Matrix2D
{
    private readonly int[,,] _data3d;

    public Matrix3D() : base()
    {
        _data3d = new int[SIZE, SIZE, SIZE];
    }

    public override void Input()
    {
        Console.WriteLine("Введення матриці 3x3x3:");

        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                for (int k = 0; k < SIZE; k++)
                {
                    Console.Write($"[{i},{j},{k}] = ");
                    while (!int.TryParse(Console.ReadLine(), out _data3d[i, j, k]))
                        Console.Write("Помилка. Введіть число: ");
                }
    }

    public override void RandomFill()
    {
        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                for (int k = 0; k < SIZE; k++)
                    _data3d[i, j, k] = _rnd.Next(-50, 51);
    }

    public override void Show()
    {
        Console.WriteLine("\nМатриця 3D:");
        for (int layer = 0; layer < SIZE; layer++)
        {
            Console.WriteLine($"Шар Z={layer}:");
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                    Console.Write($"{_data3d[layer, i, j],4}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public override int MinElement()
    {
        int min = _data3d[0, 0, 0];

        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                for (int k = 0; k < SIZE; k++)
                    if (_data3d[i, j, k] < min)
                        min = _data3d[i, j, k];

        return min;
    }
}


public class Program
{
    public static void Main()
    {
        // створення двох об'єктів відповідно до умови
        Matrix2D m2 = new Matrix2D();
        Matrix2D m3 = new Matrix3D(); // поліморфізм

        Console.WriteLine("Заповнення 2D матриці:");
        m2.RandomFill();
        m2.Show();
        Console.WriteLine($"Мінімум 2D: {m2.MinElement()}");

        Console.WriteLine("\nЗаповнення 3D матриці:");
        m3.RandomFill();
        m3.Show();
        Console.WriteLine($"Мінімум 3D: {m3.MinElement()}");

        Console.WriteLine("\nГотово.");
    }
}
