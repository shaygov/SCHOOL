namespace StructuralPatternsExamples
{
    using System.Collections.Generic;

    /*
     Composite (Композит)
     - Цел: Представя йерархия от обекти като дърво, така че листата и контейнерите
       да се третират по един и същ начин чрез един общ интерфейс.
     - В примера: IComponent е общият интерфейс; Leaf е единичен обект, Composite
       съдържа деца и ги показва рекурсивно.
    */
    public interface IComponent { void Display(string indent = ""); }

    public class Leaf : IComponent
    {
        private string _name;
        public Leaf(string name) => _name = name;
        public void Display(string indent = "") => System.Console.WriteLine($"{indent}{_name}");
    }

    public class Composite : IComponent
    {
        private readonly List<IComponent> _children = new();
        private string _name;
        public Composite(string name) => _name = name;
        public void Add(IComponent c) => _children.Add(c);
        public void Display(string indent = "")
        {
            System.Console.WriteLine($"{indent}{_name}/");
            foreach (var c in _children) c.Display(indent + "  ");
        }
    }

    public static class CompositeExample
    {
        /// <summary>
        /// Демонстрация: създава дърво с листа и суб-композити и го отпечатва.
        /// Изход: йерархична структура с отстъпи.
        /// </summary>
        public static void Demo()
        {
            var root = new Composite("root");
            root.Add(new Leaf("Leaf A"));
            var comp = new Composite("sub");
            comp.Add(new Leaf("Leaf B1"));
            comp.Add(new Leaf("Leaf B2"));
            root.Add(comp);
            root.Display();
        }
    }
}
