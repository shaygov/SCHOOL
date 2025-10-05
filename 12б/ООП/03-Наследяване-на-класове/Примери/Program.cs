using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация на наследяване ===\n");

        Car myCar = new Car("Toyota", "Corolla", 2023, 50.0, 4, true);

        Console.WriteLine("=== Информация за автомобила ===");
        myCar.DisplayInfo();
        Console.WriteLine();

        Console.WriteLine("=== Функционалност ===");
        myCar.Start();
        myCar.Honk();
        myCar.UseAirConditioning();
        myCar.OpenTrunk();
        myCar.Stop();

        Console.WriteLine("\n=== Тестване на горивото ===");
        myCar.Refuel(10);

        Console.WriteLine("\nОбновена информация:");
        myCar.DisplayInfo();
    }
}
