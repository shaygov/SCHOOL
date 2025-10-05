using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация на класове и обекти ===\n");

        Student student1 = new Student("Айше", "Дагон", 17, "2024001");
        Student student2 = new Student("Анифе", "Кусарова", 18, "2024002");
        Student student3 = new Student("Атидже", "Ходжова", 17, "2024003");

        student1.SetAverageGrade(5.8);
        student2.SetAverageGrade(4.5);
        student3.SetAverageGrade(6.0);

        Console.WriteLine("Информация за студентите:");
        student1.DisplayInfo();
        student2.DisplayInfo();
        student3.DisplayInfo();

        Console.WriteLine("Проверка за отлични студенти:");
        student1.Celebrate();
        student2.Celebrate();
        student3.Celebrate();

        Console.WriteLine("\n=== Експериментиране с обектите ===");
        
        student1.SetAverageGrade(5.9);
        Console.WriteLine($"Нова оценка за {student1.GetFullName()}: {student1.GetAverageGrade():F2}");
        
        student1.SetAverageGrade(7.0);
        student1.SetAverageGrade(1.5);
    }
}
