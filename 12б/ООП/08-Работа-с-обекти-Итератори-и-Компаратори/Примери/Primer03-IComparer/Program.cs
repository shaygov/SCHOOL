using System;
using System.Collections.Generic;

class Student
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
    
    public override string ToString()
    {
        return $"{Name} ({Age}г, {Grade:F1})";
    }
}

class StudentNameComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
    }
}

class StudentAgeComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return x.Age.CompareTo(y.Age);
    }
}

class StudentGradeComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return y.Grade.CompareTo(x.Grade);
    }
}

class StudentMultiComparer : IComparer<Student>
{
    private string primaryCriteria;
    private string secondaryCriteria;
    
    public StudentMultiComparer(string primary, string secondary)
    {
        primaryCriteria = primary;
        secondaryCriteria = secondary;
    }
    
    public int Compare(Student x, Student y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        int primaryComparison = CompareByCriteria(x, y, primaryCriteria);
        if (primaryComparison != 0) return primaryComparison;
        
        return CompareByCriteria(x, y, secondaryCriteria);
    }
    
    private int CompareByCriteria(Student x, Student y, string criteria)
    {
        switch (criteria.ToLower())
        {
            case "name":
                return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            case "age":
                return x.Age.CompareTo(y.Age);
            case "grade":
                return y.Grade.CompareTo(x.Grade);
            default:
                return 0;
        }
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
            new Student("Георги", 22, 5.5),
            new Student("Елена", 19, 6.0)
        };
        
        Console.WriteLine("=== Оригинален списък ===");
        PrintStudents(students);
        
        Console.WriteLine("\n=== Сортиране по име ===");
        students.Sort(new StudentNameComparer());
        PrintStudents(students);
        
        Console.WriteLine("\n=== Сортиране по възраст ===");
        students.Sort(new StudentAgeComparer());
        PrintStudents(students);
        
        Console.WriteLine("\n=== Сортиране по оценка ===");
        students.Sort(new StudentGradeComparer());
        PrintStudents(students);
        
        Console.WriteLine("\n=== Сортиране по оценка, след това по име ===");
        students.Sort(new StudentMultiComparer("grade", "name"));
        PrintStudents(students);
        
        Console.WriteLine("\n=== Сортиране по възраст, след това по име ===");
        students.Sort(new StudentMultiComparer("age", "name"));
        PrintStudents(students);
    }
    
    static void PrintStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }
}

