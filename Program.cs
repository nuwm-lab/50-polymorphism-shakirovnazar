using System;

public class Matrix2D
{
    protected int[,] data = new int[3, 3];
    protected Random rnd = new Random();

    public virtual void Input()
    {
        Console.WriteLine("Введення матриці 3x3:");

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"[{i},{j}] = ");
                while (!int.TryParse(Console.ReadLine(), out data[i, j]))
                    Console.Write("Помилка. Введіть число: ");
            }
    }

    public virtual void RandomFill()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                data[i, j] = rnd.Next(-50, 51);
    }

    public virtual void Show()
    {
        Console.WriteLine("\nМатриця 2D:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
                Console.Write($"{data[i, j],4}");
            Console.WriteLine();
        }
    }

    public virtual int MinElement()
    {
        int min = data[0, 0];

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (data[i, j] < min)
                    min = data[i, j];

        return min;
    }
}


public class Matrix3D : Matrix2D
{
    private int[,,] data3d = new int[3, 3, 3];

    public override void Input()
    {
        Console.WriteLine("Введення матриці 3x3x3:");

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                {
                    Console.Write($"[{i},{j},{k}] = ");
                    while (!int.TryParse(Console.ReadLine(), out data3d[i, j, k]))
                        Console.Write("Помилка. Введіть число: ");
                }
    }

    public override void RandomFill()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    data3d[i, j, k] = rnd.Next(-50, 51);
    }

    public override void Show()
    {
        Console.WriteLine("\nМатриця 3D:");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Шар Z={i}:");
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                    Console.Write($"{data3d[i, j, k],4}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public override int MinElement()
    {
        int min = data3d[0, 0, 0];

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                    if (data3d[i, j, k] < min)
                        min = data3d[i, j, k];

        return min;
    }
}


public class Program
{
    public static void Main()
    {
        Console.WriteLine("1 – Матриця 2D");
        Console.WriteLine("2 – Матриця 3D");
        Console.Write("Ваш вибір: ");

        char choice = Console.ReadKey().KeyChar;
        Console.WriteLine();

        Matrix2D matrix; // поліморфізм через базовий клас

        if (choice == '1')
            matrix = new Matrix2D();
        else
            matrix = new Matrix3D();

        Console.WriteLine("\n(1) Ввести вручну");
        Console.WriteLine("(2) Заповнити випадково");
        Console.Write("Ваш вибір: ");
        char mode = Console.ReadKey().KeyChar;
        Console.WriteLine();

        if (mode == '1')
            matrix.Input();
        else
            matrix.RandomFill();

        matrix.Show();
        Console.WriteLine($"\nМінімальний елемент = {matrix.MinElement()}");

        Console.WriteLine("\nГотово.");
    }
}
