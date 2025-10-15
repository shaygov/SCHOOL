using System;
using System.Collections.Generic;

namespace DelegatesAndEventsExample.BuiltInDelegates
{
    public class AdvancedCalculator
    {
        private Dictionary<string, Func<double, double, double>> operations;
        private Dictionary<string, Action<string>> outputs;

        public AdvancedCalculator()
        {
            operations = new Dictionary<string, Func<double, double, double>>
            {
                { "add", (x, y) => x + y },
                { "subtract", (x, y) => x - y },
                { "multiply", (x, y) => x * y },
                { "divide", (x, y) => y != 0 ? x / y : double.NaN },
                { "power", Math.Pow },
                { "max", Math.Max },
                { "min", Math.Min }
            };

            outputs = new Dictionary<string, Action<string>>
            {
                { "console", Console.WriteLine },
                { "file", message => Console.WriteLine($"Файл: {message}") },
                { "database", message => Console.WriteLine($"База данни: {message}") }
            };
        }

        public void RunCalculator()
        {
            Console.WriteLine("=== РАЗШИРЕН КАЛКУЛАТОР ===");
            Console.WriteLine("Достъпни операции: add, subtract, multiply, divide, power, max, min");
            Console.WriteLine("Достъпни изходи: console, file, database");
            Console.WriteLine("Формат: операция число1 число2 изход");
            Console.WriteLine("За изход въведете 'exit'");

            Action<string> currentOutput = outputs["console"];

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                    break;

                if (string.IsNullOrEmpty(input))
                    continue;

                var parts = input.Split(' ');
                if (parts.Length != 4)
                {
                    Console.WriteLine("Невалиден формат. Използвайте: операция число1 число2 изход");
                    continue;
                }

                string operation = parts[0].ToLower();
                if (!double.TryParse(parts[1], out double num1) || !double.TryParse(parts[2], out double num2))
                {
                    Console.WriteLine("Невалидни числа.");
                    continue;
                }

                string outputType = parts[3].ToLower();
                if (outputs.ContainsKey(outputType))
                {
                    currentOutput = outputs[outputType];
                }

                if (operations.ContainsKey(operation))
                {
                    double result = operations[operation](num1, num2);
                    if (double.IsNaN(result))
                    {
                        currentOutput("Грешка: Деление на нула!");
                    }
                    else
                    {
                        currentOutput($"{num1} {operation} {num2} = {result}");
                    }
                }
                else
                {
                    currentOutput("Невалидна операция.");
                }
            }
        }
    }
}
