using System;

class Figure
{
    public virtual void Input()
    {
        Console.WriteLine("Фігура: немає даних.");
    }

    public virtual double Area()
    {
        return 0;
    }

    public virtual void Show()
    {
        Console.WriteLine("Площа = " + Area());
    }
}

class Rectangle : Figure
{
    double w, h;

    public override void Input()
    {
        Console.Write("Ширина: ");
        w = Convert.ToDouble(Console.ReadLine());
        Console.Write("Висота: ");
        h = Convert.ToDouble(Console.ReadLine());
    }

    public override double Area()
    {
        return w * h;
    }

    public override void Show()
    {
        Console.WriteLine("Прямокутник. Площа = " + Area());
    }
}

class Circle : Figure
{
    double r;

    public override void Input()
    {
        Console.Write("Радіус: ");
        r = Convert.ToDouble(Console.ReadLine());
    }

    public override double Area()
    {
        return Math.PI * r * r;
    }

    public override void Show()
    {
        Console.WriteLine("Коло. Площа = " + Area());
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("1 - Прямокутник");
        Console.WriteLine("2 - Коло");
        Console.Write("Ваш вибір: ");

        char user = Console.ReadKey().KeyChar;
        Console.WriteLine();

        Figure fig;   // вказівник на базовий клас

        if (user == '1')
            fig = new Rectangle();
        else
            fig = new Circle();

        fig.Input();
        fig.Show();

        Console.ReadKey();
    }
}
