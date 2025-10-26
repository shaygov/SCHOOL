using System;
using System.Collections.Generic;

class Program {
    static void Main() {
        Console.WriteLine("=== СИСТЕМА ЗА УПРАВЛЕНИЕ НА БИБЛИОТЕКА ===");
        
        LibraryManager library = new LibraryManager();
        
        Book book1 = new Book("Програмиране на C#", 2023, "Секция А1", 
                             "Иван Петров", "978-954-123-456-7", 450, "Техническа литература");
        
        Book book2 = new Book("История на България", 2022, "Секция Б2", 
                             "Мария Георгиева", "978-954-789-012-3", 320, "История");
        
        Magazine magazine1 = new Magazine("Наука и техника", 2024, "Секция В3", 
                                         15, new DateTime(2024, 1, 15), "Издателство Наука", "Техника");
        
        Newspaper newspaper1 = new Newspaper("Дневник", 2024, "Секция Г4", 
                                           new DateTime(2024, 1, 20), "Дневник АД", 50000, "Български");
        
        Thesis thesis1 = new Thesis("Изкуствен интелект в образованието", 2023, "Секция Д5",
                                   "Петър Стоянов", "Софийски университет", "ФМИ", 
                                   "проф. д-р Анна Иванова", 2023, "Магистър");
        
        library.AddDocument(book1);
        library.AddDocument(book2);
        library.AddDocument(magazine1);
        library.AddDocument(newspaper1);
        library.AddDocument(thesis1);
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.DisplayAllDocuments();
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.DisplayAvailableDocuments();
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.BorrowDocument(book1.Id, "Студент Иван");
        library.BorrowDocument(magazine1.Id, "Студент Иван");
        library.BorrowDocument(thesis1.Id, "Студент Мария");
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.DisplayBorrowedByUser("Студент Иван");
        library.DisplayBorrowedByUser("Студент Мария");
        
        Console.WriteLine("\n" + new string('=', 60));
        
        Console.WriteLine("=== ТЪРСЕНЕ ПО ТИП: КНИГИ ===");
        var books = library.SearchByType("Книга");
        foreach (var book in books) {
            book.DisplayInfo();
            Console.WriteLine(new string('-', 30));
        }
        
        Console.WriteLine("\n" + new string('=', 60));
        
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ НА ПОЛИМОРФИЗЪМ ===");
        List<Document> allDocuments = new List<Document> { book1, magazine1, newspaper1, thesis1 };
        
        foreach (var doc in allDocuments) {
            Console.WriteLine($"Тип: {doc.GetDocumentType()}");
            Console.WriteLine($"Заглавие: {doc.Title}");
            
            if (doc is Book book) {
                book.Read();
                Console.WriteLine($"Време за четене: {book.GetReadingTime()}");
            } else if (doc is Magazine magazine) {
                magazine.Browse();
                Console.WriteLine($"Възраст: {magazine.GetAge()}");
            } else if (doc is Newspaper newspaper) {
                newspaper.ReadNews();
                Console.WriteLine($"Свежест на новините: {newspaper.GetNewsAge()}");
            } else if (doc is Thesis thesis) {
                thesis.Study();
                Console.WriteLine($"Академично ниво: {thesis.GetAcademicLevel()}");
            }
            
            Console.WriteLine(new string('-', 40));
        }
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.DisplayLibraryStatistics();
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.ReturnDocument(book1.Id, "Студент Иван");
        library.ReturnDocument(thesis1.Id, "Студент Мария");
        
        Console.WriteLine("\n" + new string('=', 60));
        
        library.DisplayLibraryStatistics();
        
        Console.WriteLine("\n=== ПРОГРАМАТА ЗАВЪРШИ УСПЕШНО ===");
    }
}
