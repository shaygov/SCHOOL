using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesAndEventsExample.BuiltInDelegates
{
    public class BuiltInDelegatesDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== ВГРАДЕНИ ДЕЛЕГАТИ ===\n");

            Console.WriteLine("1. Action делегати:");
            Action<string> printAction = message => Console.WriteLine($"Action: {message}");
            Action<int, int> mathAction = (x, y) => Console.WriteLine($"Сума: {x + y}");
            Action<string> printWithTime = message => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");

            printAction("Здравей от Action!");
            mathAction(10, 20);
            printWithTime("Съобщение с време");
            Console.WriteLine();

            Console.WriteLine("2. Func делегати:");
            Func<int, int, int> addFunc = (x, y) => x + y;
            Func<string, string, string> concatFunc = (s1, s2) => s1 + " " + s2;
            Func<int, bool> isEvenFunc = x => x % 2 == 0;
            Func<string, int> stringLength = s => s?.Length ?? 0;

            Console.WriteLine($"Събиране: {addFunc(5, 3)}");
            Console.WriteLine($"Свързване: {concatFunc("Здравей", "Свят")}");
            Console.WriteLine($"4 е четно: {isEvenFunc(4)}");
            Console.WriteLine($"Дължина на 'Здравей': {stringLength("Здравей")}");
            Console.WriteLine();

            Console.WriteLine("3. Predicate делегати:");
            Predicate<int> isPositivePredicate = x => x > 0;
            Predicate<string> isNotEmptyPredicate = s => !string.IsNullOrEmpty(s);
            Predicate<string> isEmailPredicate = s => s.Contains("@") && s.Contains(".");

            Console.WriteLine($"5 е положително: {isPositivePredicate(5)}");
            Console.WriteLine($"'Здравей' не е празно: {isNotEmptyPredicate("Здравей")}");
            Console.WriteLine($"'user@example.com' е email: {isEmailPredicate("user@example.com")}");
            Console.WriteLine();

            Console.WriteLine("4. Comparison делегати:");
            Comparison<int> compareNumbers = (x, y) => x.CompareTo(y);
            Comparison<string> compareStrings = (s1, s2) => string.Compare(s1, s2, StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Сравнение на 5 и 3: {compareNumbers(5, 3)}");
            Console.WriteLine($"Сравнение на 'a' и 'b': {compareStrings("a", "b")}");
            Console.WriteLine();

            Console.WriteLine("5. Комбиниране на Action делегати:");
            Action<string> combinedAction = printAction;
            combinedAction += printWithTime;
            combinedAction += message => Console.WriteLine($"Допълнително: {message.ToUpper()}");
            combinedAction("Тест на комбиниране");
            Console.WriteLine();

            Console.WriteLine("6. Работа с колекции:");
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            Console.WriteLine("Оригинални числа:");
            numbers.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine("\n");

            Console.WriteLine("Четни числа:");
            var evenNumbers = numbers.FindAll(new Predicate<int>(isEvenFunc));
            evenNumbers.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine("\n");

            Console.WriteLine("Положителни числа:");
            var positiveNumbers = numbers.FindAll(n => isPositivePredicate(n));
            positiveNumbers.ForEach(n => Console.Write($"{n} "));
            Console.WriteLine("\n");

            Console.WriteLine("7. Сортиране с Comparison:");
            var words = new List<string> { "Здравей", "Свят", "Програмиране", "C#", "Делегати" };
            Console.WriteLine("Преди сортиране:");
            words.ForEach(w => Console.Write($"{w} "));
            Console.WriteLine("\n");

            words.Sort(compareStrings);
            Console.WriteLine("След сортиране:");
            words.ForEach(w => Console.Write($"{w} "));
            Console.WriteLine("\n");

            Console.WriteLine("8. LINQ с Func делегати:");
            var products = new List<(string Name, decimal Price)>
            {
                ("Laptop", 1500),
                ("Mouse", 25),
                ("Keyboard", 80),
                ("Monitor", 300),
                ("Headphones", 120)
            };

            Func<(string Name, decimal Price), bool> expensiveFilter = p => p.Price > 100;
            Func<(string Name, decimal Price), string> nameSelector = p => p.Name;
            Func<(string Name, decimal Price), decimal> priceSelector = p => p.Price;

            Console.WriteLine("Скъпи продукти (над 100 лв):");
            var expensiveProducts = products.Where(expensiveFilter);
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"{nameSelector(product)} - {priceSelector(product):C}");
            }
            Console.WriteLine();

            Console.WriteLine("9. Кастомни операции с вградени делегати:");
            var calculator = new AdvancedCalculator();
            calculator.RunCalculator();
        }
    }
}
