using System;

class Program {
    static void Main() {
        // Деклариране на променливи
        string name = "Мария";
        int age = 14;
        double height = 1.65;
        bool isStudent = true;
        char grade = 'A';
        
        // Извеждане на информация
        Console.WriteLine("=== Информация за ученика ===");
        Console.WriteLine($"Име: {name}");
        Console.WriteLine($"Възраст: {age} години");
        Console.WriteLine($"Височина: {height} метра");
        Console.WriteLine($"Студент: {(isStudent ? "Да" : "Не")}");
        Console.WriteLine($"Оценка: {grade}");
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
