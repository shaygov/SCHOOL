using System;
using System.Collections.Generic;

class NumberRange
{
    private int start;
    private int end;
    
    public NumberRange(int start, int end)
    {
        this.start = start;
        this.end = end;
    }
    
    public List<int> GetAllNumbers()
    {
        List<int> result = new List<int>();
        for (int i = start; i <= end; i++)
        {
            result.Add(i);
        }
        return result;
    }
    
    public List<int> GetEvenNumbers()
    {
        List<int> result = new List<int>();
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
            {
                result.Add(i);
            }
        }
        return result;
    }
    
    public List<int> GetOddNumbers()
    {
        List<int> result = new List<int>();
        for (int i = start; i <= end; i++)
        {
            if (i % 2 != 0)
            {
                result.Add(i);
            }
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        var numbers = new NumberRange(1, 10);
        
        Console.WriteLine("=== Всички числа от 1 до 10 ===");
        foreach (int num in numbers.GetAllNumbers())
        {
            Console.Write($"{num} ");
        }
        
        Console.WriteLine("\n\n=== Четни числа ===");
        foreach (int num in numbers.GetEvenNumbers())
        {
            Console.Write($"{num} ");
        }
        
        Console.WriteLine("\n\n=== Нечетни числа ===");
        foreach (int num in numbers.GetOddNumbers())
        {
            Console.Write($"{num} ");
        }
        
        Console.WriteLine();
    }
}
