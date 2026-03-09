using System;

namespace StructuralPatternsExamples
{
    public class Program
    {
        public static void Main()
        {
            // Кратка последователна демонстрация на всички шаблони.
            Console.WriteLine("Adapter Demo:");
            AdapterExample.Demo();
            Console.WriteLine();

            Console.WriteLine("Bridge Demo:");
            BridgeExample.Demo();
            Console.WriteLine();

            Console.WriteLine("Composite Demo:");
            CompositeExample.Demo();
            Console.WriteLine();

            Console.WriteLine("Decorator Demo:");
            DecoratorExample.Demo();
            Console.WriteLine();

            Console.WriteLine("Facade Demo:");
            FacadeExample.Demo();
        }
    }
}
