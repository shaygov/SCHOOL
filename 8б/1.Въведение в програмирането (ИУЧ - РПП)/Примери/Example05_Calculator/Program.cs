using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Прост калкулатор ===");
        
        // Четене на числата
        Console.Write("Въведете първото число: ");
        double num1 = double.Parse(Console.ReadLine());
        
        Console.Write("Въведете второто число: ");
        double num2 = double.Parse(Console.ReadLine());
        
        // Извършване на операциите
        double sum = num1 + num2;
        double difference = num1 - num2;
        double product = num1 * num2;
        double quotient = num1 / num2;
        
        // Извеждане на резултатите
        Console.WriteLine($"\nРезултати:");
        Console.WriteLine($"{num1} + {num2} = {sum}");
        Console.WriteLine($"{num1} - {num2} = {difference}");
        Console.WriteLine($"{num1} * {num2} = {product}");
        Console.WriteLine($"{num1} / {num2} = {quotient:F2}");
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
