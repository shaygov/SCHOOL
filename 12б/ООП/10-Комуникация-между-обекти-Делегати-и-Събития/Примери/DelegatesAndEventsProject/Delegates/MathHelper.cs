using System;

namespace DelegatesAndEventsExample.Delegates
{
    public static class MathHelper
    {
        public static int Add(int x, int y)
        {
            Console.WriteLine($"Събиране: {x} + {y}");
            return x + y;
        }
        
        public static int Multiply(int x, int y)
        {
            Console.WriteLine($"Умножение: {x} * {y}");
            return x * y;
        }
        
        public static int Subtract(int x, int y)
        {
            Console.WriteLine($"Изваждане: {x} - {y}");
            return x - y;
        }
        
        public static int Divide(int x, int y)
        {
            if (y == 0)
            {
                Console.WriteLine("Грешка: Деление на нула!");
                return 0;
            }
            Console.WriteLine($"Деление: {x} / {y}");
            return x / y;
        }
    }
}
