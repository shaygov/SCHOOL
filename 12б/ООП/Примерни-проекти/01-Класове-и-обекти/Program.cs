using System;

class Student
{
    private string name;
    private int age;
    private double grade;

    public Student(string name, int age, double grade)
    {
        this.name = name;
        this.age = age;
        this.grade = grade;
    }

    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public double GetGrade()
    {
        return grade;
    }

    public void SetGrade(double newGrade)
    {
        if (newGrade >= 2.0 && newGrade <= 6.0)
        {
            grade = newGrade;
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Студент: {name}, Възраст: {age}, Оценка: {grade}");
    }
}

class Program
{
    static void Main()
    {
        Student student1 = new Student("Анна Петрова", 17, 5.5);
        Student student2 = new Student("Иван Димитров", 18, 4.8);

        Console.WriteLine("=== Информация за студентите ===");
        student1.DisplayInfo();
        student2.DisplayInfo();

        Console.WriteLine("\n=== Промяна на оценка ===");
        student1.SetGrade(6.0);
        Console.WriteLine("Нова оценка за Анна:");
        student1.DisplayInfo();
    }
}
