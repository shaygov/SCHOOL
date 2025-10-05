using System;

class Counter
{
    private static int totalCount = 0;
    private static int instanceCount = 0;
    private int currentValue;

    public Counter()
    {
        instanceCount++;
        currentValue = 0;
        Console.WriteLine($"Създаден е нов брояч. Общо създадени: {instanceCount}");
    }

    public static int GetTotalCount()
    {
        return totalCount;
    }

    public static int GetInstanceCount()
    {
        return instanceCount;
    }

    public void Increment()
    {
        currentValue++;
        totalCount++;
    }

    public void Decrement()
    {
        if (currentValue > 0)
        {
            currentValue--;
            totalCount--;
        }
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }

    public static void ResetTotalCount()
    {
        totalCount = 0;
        Console.WriteLine("Общият брой е нулиран!");
    }

    public static void DisplayStatistics()
    {
        Console.WriteLine($"=== Статистика ===");
        Console.WriteLine($"Общ брой операции: {totalCount}");
        Console.WriteLine($"Брой създадени броячи: {instanceCount}");
    }
}
