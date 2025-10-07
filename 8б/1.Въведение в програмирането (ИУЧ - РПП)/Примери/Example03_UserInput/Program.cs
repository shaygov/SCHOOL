using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Добре дошли в програмата! ===");
        
        // Четене на име
        Console.Write("Въведете вашето име: ");
        string name = Console.ReadLine();
        
        // Четене на възраст
        Console.Write("Въведете вашата възраст: ");
        string ageText = Console.ReadLine();
        int age = int.Parse(ageText);
        
        // Извеждане на резултата
        Console.WriteLine($"\nЗдравейте, {name}!");
        Console.WriteLine($"Вие сте на {age} години.");
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
