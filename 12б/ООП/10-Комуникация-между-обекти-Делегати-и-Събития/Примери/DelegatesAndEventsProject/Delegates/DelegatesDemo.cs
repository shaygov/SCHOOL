using System;

namespace DelegatesAndEventsExample.Delegates
{
    public class DelegatesDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== ДЕМОНСТРАЦИЯ НА ДЕЛЕГАТИ ===\n");

            Console.WriteLine("1. Основни операции с делегати:");
            MathOperation operation = MathHelper.Add;
            int result = operation(5, 3);
            Console.WriteLine($"Резултат: {result}\n");

            operation = MathHelper.Multiply;
            result = operation(4, 6);
            Console.WriteLine($"Резултат: {result}\n");

            Console.WriteLine("2. Множествено извикване:");
            DisplayMessage display = MessageHelper.PrintToConsole;
            display += MessageHelper.PrintToFile;
            display += MessageHelper.PrintToDatabase;
            display += MessageHelper.PrintWithTimestamp;

            display("Важно съобщение");
            Console.WriteLine();

            Console.WriteLine("3. Валидация с делегати:");
            ValidationRule validator = ValidationHelper.IsNotEmpty;
            validator += ValidationHelper.IsEmail;
            validator += ValidationHelper.IsLongEnough;

            string email = "user@example.com";
            bool isValid = validator(email);
            Console.WriteLine($"Email '{email}' е валиден: {isValid}");
            Console.WriteLine();

            Console.WriteLine("4. Анонимни методи:");
            MathOperation anonymousOperation = delegate(int x, int y) 
            {
                Console.WriteLine($"Анонимна операция: {x} + {y} = {x + y}");
                return x + y;
            };
            anonymousOperation(10, 5);
            Console.WriteLine();

            Console.WriteLine("5. Ламбда изрази:");
            MathOperation lambdaOperation = (x, y) => 
            {
                Console.WriteLine($"Ламбда операция: {x} * {y} = {x * y}");
                return x * y;
            };
            lambdaOperation(7, 8);
            Console.WriteLine();

            Console.WriteLine("6. Математически калкулатор:");
            var calculator = new MathCalculator();
            calculator.RunCalculator();
        }
    }
}
