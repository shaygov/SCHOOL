using System;

class Vehicle
{
    protected string brand;
    protected string model;
    protected int year;
    protected double fuelCapacity;
    protected double currentFuel;

    public Vehicle(string brand, string model, int year, double fuelCapacity)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
        this.fuelCapacity = fuelCapacity;
        this.currentFuel = fuelCapacity;
    }

    public virtual void Start()
    {
        Console.WriteLine($"{brand} {model} започва движение");
    }

    public virtual void Stop()
    {
        Console.WriteLine($"{brand} {model} спира");
    }

    public virtual void Refuel(double amount)
    {
        if (currentFuel + amount <= fuelCapacity)
        {
            currentFuel += amount;
            Console.WriteLine($"Заредени {amount}л гориво. Текущо: {currentFuel}л");
        }
        else
        {
            Console.WriteLine("Резервоарът е пълен!");
        }
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Марка: {brand}");
        Console.WriteLine($"Модел: {model}");
        Console.WriteLine($"Година: {year}");
        Console.WriteLine($"Гориво: {currentFuel}/{fuelCapacity}л");
    }

    protected double CalculateFuelPercentage()
    {
        return (currentFuel / fuelCapacity) * 100;
    }
}
