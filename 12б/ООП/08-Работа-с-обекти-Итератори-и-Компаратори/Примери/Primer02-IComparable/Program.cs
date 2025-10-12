using System;
using System.Collections.Generic;

class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }
    
    public Student(string name, int age, double grade)
    {
        Name = name;
        Age = age;
        Grade = grade;
    }
    
    public int CompareTo(Student other)
    {
        if (other == null) return 1;
        
        int gradeComparison = other.Grade.CompareTo(this.Grade);
        if (gradeComparison != 0) return gradeComparison;
        
        int nameComparison = this.Name.CompareTo(other.Name);
        if (nameComparison != 0) return nameComparison;
        
        return this.Age.CompareTo(other.Age);
    }
    
    public override string ToString()
    {
        return $"{Name} ({Age}г, {Grade:F1})";
    }
}

class Program
{
    static void Main()
    {
        var students = new List<Student>
        {
            new Student("Иван", 20, 5.5),
            new Student("Мария", 19, 6.0),
            new Student("Петър", 21, 5.5),
            new Student("Анна", 18, 5.0),
            new Student("Иван", 20, 4.5),
            new Student("Георги", 22, 5.0),
            new Student("Елена", 19, 6.0)
        };
        
        Console.WriteLine("=== Преди сортиране ===");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
        
        students.Sort();
        
        Console.WriteLine("\n=== След сортиране (по оценка, име, възраст) ===");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
        
        Console.WriteLine("\n=== Търсене на студент ===");
        var searchStudent = new Student("Мария", 19, 6.0);
        int index = students.BinarySearch(searchStudent);
        if (index >= 0)
        {
            Console.WriteLine($"Намерен на позиция {index}: {students[index]}");
        }
        else
        {
            Console.WriteLine("Студентът не е намерен");
        }
    }
}

