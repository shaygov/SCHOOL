using System;

class Program {
    static void Main() {
        Console.WriteLine("=== Калкулатор на успех ===");
        
        Console.Write("Въведете вашата оценка: ");
        double grade = double.Parse(Console.ReadLine());
        
        // Проверка на оценката
        if (grade >= 5.5) {
            Console.WriteLine("Отличен успех! 🎉");
            Console.WriteLine("Вие сте много добър ученик!");
        }
        else if (grade >= 4.5) {
            Console.WriteLine("Много добър успех! 👍");
            Console.WriteLine("Продължавайте така!");
        }
        else if (grade >= 3.5) {
            Console.WriteLine("Добър успех! ✅");
            Console.WriteLine("Можете да се подобрите още!");
        }
        else if (grade >= 3.0) {
            Console.WriteLine("Среден успех. 📚");
            Console.WriteLine("Трябва да учите повече!");
        }
        else {
            Console.WriteLine("Слаб успех. ⚠️");
            Console.WriteLine("Необходимо е сериозно подобрение!");
        }
        
        Console.WriteLine("\nНатисни Enter за изход...");
        Console.ReadLine();
    }
}
