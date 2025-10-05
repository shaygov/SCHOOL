using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация на интерфейси ===\n");

        Book book = new Book("Програмиране с C#", "Муса Шехов", "Това е книга за C# програмиране...", 350);

        Console.WriteLine("=== Информация за книгата ===");
        book.DisplayInfo();

        Console.WriteLine("\n=== Работа с IReadable интерфейс ===");
        book.Read();
        Console.WriteLine($"Налична: {book.IsAvailable()}");

        Console.WriteLine("\n=== Работа с IPrintable интерфейс ===");
        book.Print();
        Console.WriteLine($"Брой страници: {book.GetPageCount()}");

        Console.WriteLine("\n=== Тестване на библиотеката ===");
        book.Borrow();
        book.Read();
        book.Return();
        book.Read();

        Console.WriteLine("\n=== Демонстрация на полиморфизъм ===");
        IReadable readable = book;
        IPrintable printable = book;
        
        Console.WriteLine("Чрез интерфейс IReadable:");
        readable.Read();
        
        Console.WriteLine("\nЧрез интерфейс IPrintable:");
        printable.Print();
    }
}
