using System;

class Lion : Animal
{
    private string pride;
    private bool isAlpha;

    public Lion(string name, int age, string habitat, string pride, bool isAlpha) 
        : base(name, age, habitat)
    {
        this.pride = pride;
        this.isAlpha = isAlpha;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{name} реве: РРРРРРРРР!");
    }

    public override void Move()
    {
        Console.WriteLine($"{name} ходи гордо като лъв");
    }

    public override void Eat()
    {
        Console.WriteLine($"{name} яде месо от плячка");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Група: {pride}");
        Console.WriteLine($"Алфа лъв: {(isAlpha ? "Да" : "Не")}");
    }

    public void Hunt()
    {
        Console.WriteLine($"{name} лови плячка");
    }

    public void ProtectTerritory()
    {
        if (isAlpha)
        {
            Console.WriteLine($"{name} защитава територията като алфа лъв");
        }
        else
        {
            Console.WriteLine($"{name} помага в защитата на територията");
        }
    }
}
