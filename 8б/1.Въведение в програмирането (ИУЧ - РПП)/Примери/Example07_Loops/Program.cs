using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Примери с цикли ===");
        
        // For цикъл - броене от 1 до 5
        Console.WriteLine("For цикъл - числа от 1 до 5:");
        for (int i = 1; i <= 5; i++) {
            Console.WriteLine($"Число: {i}");
        }
        
        Console.WriteLine();
        
        // While цикъл - таблица за умножение
        Console.WriteLine("Таблица за умножение с 3:");
        int number = 1;
        while (number <= 10) {
            int result = 3 * number;
            Console.WriteLine($"3 × {number} = {result}");
            number++;
        }
        
        Console.WriteLine();
        
        // Do-while цикъл - четене до въвеждане на 0
        Console.WriteLine("Въведете числа (0 за изход):");
        int input;
        do {
            Console.Write("Въведете число: ");
            input = int.Parse(Console.ReadLine());
            Console.WriteLine($"Въведеното число: {input}");
        } while (input != 0);
        
        Console.WriteLine("Програмата приключи!");
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
