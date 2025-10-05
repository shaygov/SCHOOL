using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация на статични членове ===\n");

        Console.WriteLine("1. Работа с Counter класа:");
        Counter counter1 = new Counter();
        Counter counter2 = new Counter();

        counter1.Increment();
        counter1.Increment();
        counter2.Increment();
        counter1.Decrement();

        Console.WriteLine($"Counter1 стойност: {counter1.GetCurrentValue()}");
        Console.WriteLine($"Counter2 стойност: {counter2.GetCurrentValue()}");
        Counter.DisplayStatistics();

        Console.WriteLine("\n2. Създаване на нови броячи:");
        Counter counter3 = new Counter();
        counter3.Increment();
        counter3.Increment();
        counter3.Increment();
        
        Counter.DisplayStatistics();
        
        Console.WriteLine("\n3. Нулиране на общия брой:");
        Counter.ResetTotalCount();
        Counter.DisplayStatistics();
    }
}
