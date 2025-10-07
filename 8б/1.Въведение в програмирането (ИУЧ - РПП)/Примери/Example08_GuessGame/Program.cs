using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Игра 'Познай числото' ===");
        Console.WriteLine("Компютърът ще мисли за число между 1 и 100.");
        Console.WriteLine("Опитайте се да го познаете!");
        Console.WriteLine();
        
        // Генериране на случайно число
        Random random = new Random();
        int secretNumber = random.Next(1, 101);
        
        int attempts = 0;
        int guess;
        
        Console.WriteLine("Числото е генерирано! Започнете да познавате...");
        
        // Основен цикъл на играта
        do {
            Console.Write($"Опит {attempts + 1}: Въведете вашето предположение: ");
            guess = int.Parse(Console.ReadLine());
            attempts++;
            
            if (guess < secretNumber) {
                Console.WriteLine("Числото е по-голямо! ⬆️");
            }
            else if (guess > secretNumber) {
                Console.WriteLine("Числото е по-малко! ⬇️");
            }
            else {
                Console.WriteLine($"🎉 Поздравления! Познахте числото {secretNumber}!");
                Console.WriteLine($"Направихте {attempts} опита.");
            }
            
        } while (guess != secretNumber);
        
        // Оценка на резултата
        if (attempts <= 3) {
            Console.WriteLine("Отлично! Много бързо познахте!");
        }
        else if (attempts <= 7) {
            Console.WriteLine("Добре! Добър резултат!");
        }
        else {
            Console.WriteLine("Можете да се подобрите! Практикувайте повече!");
        }
        
        Console.WriteLine("\nБлагодарим за играта!");
        Console.WriteLine("Натисни Enter за изход...");
        Console.ReadLine();
    }
}
