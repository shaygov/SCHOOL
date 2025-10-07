using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Калкулатор за площ на правоъгълник ===");
        
        Console.Write("Въведете дължината (см): ");
        double length = double.Parse(Console.ReadLine());
        
        Console.Write("Въведете ширината (см): ");
        double width = double.Parse(Console.ReadLine());
        
        double area = length * width;
        
        Console.WriteLine($"\nПлощта на правоъгълника е: {area} кв. см");
        
        if (area > 100) {
            Console.WriteLine("Това е голям правоъгълник!");
        } else if (area > 50) {
            Console.WriteLine("Това е среден правоъгълник.");
        } else {
            Console.WriteLine("Това е малък правоъгълник.");
        }
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
