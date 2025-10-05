using System;

abstract class Animal
{
    protected string name;
    protected int age;
    protected string habitat;

    public Animal(string name, int age, string habitat)
    {
        this.name = name;
        this.age = age;
        this.habitat = habitat;
    }

    public abstract void MakeSound();
    public abstract void Move();
    public abstract void Eat();

    public virtual void Sleep()
    {
        Console.WriteLine($"{name} спи спокойно");
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Име: {name}");
        Console.WriteLine($"Възраст: {age} години");
        Console.WriteLine($"Местообитание: {habitat}");
    }

    protected void ShowAge()
    {
        Console.WriteLine($"{name} е на {age} години");
    }
}
