using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация на абстракция ===\n");

        Lion simba = new Lion("Симба", 8, "Савана", "Гордост на Скалите", true);
        Lion nala = new Lion("Нала", 7, "Савана", "Гордост на Скалите", false);

        Console.WriteLine("=== Информация за лъвовете ===");
        simba.DisplayInfo();
        Console.WriteLine(new string('-', 40));
        nala.DisplayInfo();

        Console.WriteLine("\n=== Основни действия ===");
        Console.WriteLine("Симба:");
        simba.MakeSound();
        simba.Move();
        simba.Eat();
        simba.Sleep();

        Console.WriteLine("\nНала:");
        nala.MakeSound();
        nala.Move();
        nala.Eat();
        nala.Sleep();

        Console.WriteLine("\n=== Специфични действия ===");
        simba.Hunt();
        simba.ProtectTerritory();
        nala.Hunt();
        nala.ProtectTerritory();

        Console.WriteLine("\n=== Демонстрация на полиморфизъм ===");
        Animal[] animals = { simba, nala };
        Console.WriteLine("Всички животни правят звуци:");
        foreach (Animal animal in animals)
        {
            animal.MakeSound();
        }
    }
}
