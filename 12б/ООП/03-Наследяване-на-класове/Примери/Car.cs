using System;

class Car : Vehicle
{
    private int doors;
    private bool hasAirConditioning;

    public Car(string brand, string model, int year, double fuelCapacity, int doors, bool hasAirConditioning) 
        : base(brand, model, year, fuelCapacity)
    {
        this.doors = doors;
        this.hasAirConditioning = hasAirConditioning;
    }

    public override void Start()
    {
        base.Start();
        Console.WriteLine("Автомобилът стартира с ключ");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Брой врати: {doors}");
        Console.WriteLine($"Климатик: {(hasAirConditioning ? "Да" : "Не")}");
        Console.WriteLine($"Ниво на гориво: {CalculateFuelPercentage():F1}%");
    }

    public void Honk()
    {
        Console.WriteLine("Бииип! Бииип!");
    }

    public void OpenTrunk()
    {
        Console.WriteLine("Багажникът се отваря");
    }

    public void UseAirConditioning()
    {
        if (hasAirConditioning)
        {
            Console.WriteLine("Климатикът работи");
        }
        else
        {
            Console.WriteLine("Няма климатик");
        }
    }
}
