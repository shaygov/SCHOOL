using System;

class Program {
    static void Main() {
        // Извеждане на текст
        Console.WriteLine("Първи ред");
        Console.WriteLine("Втори ред");
        
        // Извеждане без нов ред
        Console.Write("Текст на ");
        Console.Write("същия ред");
        Console.WriteLine(); // Преминаване на нов ред
        
        // Изчистване на екрана
        Console.WriteLine("Натисни Enter за изчистване на екрана...");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Екранът е изчистен!");
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
