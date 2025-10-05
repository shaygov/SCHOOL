using System;

class Student
{
    private string firstName;
    private string lastName;
    private int age;
    private string facultyNumber;
    private double averageGrade;

    public Student(string firstName, string lastName, int age, string facultyNumber)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.facultyNumber = facultyNumber;
        this.averageGrade = 0.0;
    }

    public string GetFullName()
    {
        return $"{firstName} {lastName}";
    }

    public string GetFacultyNumber()
    {
        return facultyNumber;
    }

    public int GetAge()
    {
        return age;
    }

    public double GetAverageGrade()
    {
        return averageGrade;
    }

    public void SetAverageGrade(double grade)
    {
        if (grade >= 2.0 && grade <= 6.0)
        {
            averageGrade = grade;
        }
        else
        {
            Console.WriteLine("Невалидна оценка! Оценката трябва да бъде между 2.0 и 6.0");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Студент: {GetFullName()}");
        Console.WriteLine($"Факултетен номер: {facultyNumber}");
        Console.WriteLine($"Възраст: {age} години");
        Console.WriteLine($"Среден успех: {averageGrade:F2}");
        Console.WriteLine(new string('-', 40));
    }

    public bool IsExcellentStudent()
    {
        return averageGrade >= 5.5;
    }

    public void Celebrate()
    {
        if (IsExcellentStudent())
        {
            Console.WriteLine($"🎉 {GetFullName()} е отличен студент!");
        }
        else
        {
            Console.WriteLine($"👍 {GetFullName()} продължава да се старае!");
        }
    }
}
