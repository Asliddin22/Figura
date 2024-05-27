using System;

abstract class Figura
{
    public abstract double CalculateArea();
    public abstract double CalculatePerimeter();
}

class Circle : Figura
{
    public double Radius { get; }

    public Circle(double radius)
    {
        if (radius <= 0)
        {
            throw new ArgumentException("Радиус должен быть положительным.");
        }
        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Circle other = (Circle)obj;
        return Radius == other.Radius;
    }

    public override int GetHashCode()
    {
        return Radius.GetHashCode();
    }
}

class Triangle : Figura
{
    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }

    public Triangle(double sideA, double sideB, double sideC)
    {
        if (sideA <= 0 || sideB <= 0 || sideC <= 0)
        {
            throw new ArgumentException("Стороны должны быть положительными.");
        }
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public override double CalculateArea()
    {
        double s = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }

    public override double CalculatePerimeter()
    {
        return SideA + SideB + SideC;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Triangle other = (Triangle)obj;
        return SideA == other.SideA && SideB == other.SideB && SideC == other.SideC;
    }

    public override int GetHashCode()
    {
        return Tuple.Create(SideA, SideB, SideC).GetHashCode();
    }
}

class RightTriangle : Triangle
{
    public RightTriangle(double sideA, double sideB, double sideC)
        : base(sideA, sideB, sideC)
    {
        if (!IsRightTriangle(sideA, sideB, sideC))
        {
            throw new ArgumentException("Треугольник не является прямоугольным.");
        }
    }

    private bool IsRightTriangle(double a, double b, double c)
    {
        double[] sides = { a, b, c };
        Array.Sort(sides);
        return Math.Abs(sides[2] * sides[2] - (sides[0] * sides[0] + sides[1] * sides[1])) < 1e-10;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Выберите фигуру: 1 - Окружность, 2 - Прямоугольный треугольник");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Введите радиус окружности:");
                double radius = double.Parse(Console.ReadLine());
                Circle circle = new Circle(radius);
                Console.WriteLine($"Площадь окружности: {circle.CalculateArea()}");
                Console.WriteLine($"Периметр окружности: {circle.CalculatePerimeter()}");
            }
            else if (choice == 2)
            {
                Console.WriteLine("Введите три стороны прямоугольного треугольника:");
                double sideA = double.Parse(Console.ReadLine());
                double sideB = double.Parse(Console.ReadLine());
                double sideC = double.Parse(Console.ReadLine());

                RightTriangle rightTriangle = new RightTriangle(sideA, sideB, sideC);
                Console.WriteLine($"Площадь треугольника: {rightTriangle.CalculateArea()}");
                Console.WriteLine($"Периметр треугольника: {rightTriangle.CalculatePerimeter()}");
            }
            else
            {
                Console.WriteLine("Некорректный выбор фигуры.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
