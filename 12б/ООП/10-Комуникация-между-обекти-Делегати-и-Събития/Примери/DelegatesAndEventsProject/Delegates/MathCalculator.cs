using System;

namespace DelegatesAndEventsExample.Delegates
{
    public class MathCalculator
    {
        private MathOperation currentOperation;

        public void RunCalculator()
        {
            Console.WriteLine("=== МАТЕМАТИЧЕСКИ КАЛКУЛАТОР ===");
            Console.WriteLine("Достъпни операции: Add, Multiply, Subtract, Divide");
            Console.WriteLine("Въведете операция и два числа (например: Add 10 5)");
            Console.WriteLine("За изход въведете 'exit'");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                if (string.IsNullOrEmpty(input))
                    continue;

                var parts = input.Split(' ');
                if (parts.Length != 3)
                {
                    Console.WriteLine("Невалиден формат. Използвайте: операция число1 число2");
                    continue;
                }

                string operation = parts[0];
                if (!int.TryParse(parts[1], out int num1) || !int.TryParse(parts[2], out int num2))
                {
                    Console.WriteLine("Невалидни числа.");
                    continue;
                }

                currentOperation = operation.ToLower() switch
                {
                    "add" => MathHelper.Add,
                    "multiply" => MathHelper.Multiply,
                    "subtract" => MathHelper.Subtract,
                    "divide" => MathHelper.Divide,
                    _ => null
                };

                if (currentOperation == null)
                {
                    Console.WriteLine("Невалидна операция.");
                    continue;
                }

                int result = currentOperation(num1, num2);
                Console.WriteLine($"Резултат: {result}\n");
            }
        }
    }
}
