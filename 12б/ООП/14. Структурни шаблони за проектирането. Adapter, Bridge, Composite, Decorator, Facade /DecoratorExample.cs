namespace StructuralPatternsExamples
{
    /*
     Decorator (Декоратор)
     - Цел: Динамично добавяне на отговорности към обект без да се променя неговия код
       (композиция вместо наследяване).
     - В примера: ConcreteDecoratorA/B обвиват основния обект и добавят поведение към Operation().
    */
    public interface IComponent { string Operation(); }

    public class ConcreteComponent : IComponent { public string Operation() => "Core"; }

    public abstract class DecoratorBase : IComponent
    {
        protected IComponent _wrappee;
        protected DecoratorBase(IComponent c) { _wrappee = c; }
        public virtual string Operation() => _wrappee.Operation();
    }

    public class ConcreteDecoratorA : DecoratorBase
    {
        public ConcreteDecoratorA(IComponent c) : base(c) { }
        public override string Operation() => $"A({base.Operation()})";
    }

    public class ConcreteDecoratorB : DecoratorBase
    {
        public ConcreteDecoratorB(IComponent c) : base(c) { }
        public override string Operation() => $"B({base.Operation()})";
    }

    public static class DecoratorExample
    {
        /// <summary>
        /// Демонстрация: обвиваме основния компонент с два декоратора.
        /// Очакван изход: B(A(Core))
        /// </summary>
        public static void Demo()
        {
            IComponent component = new ConcreteComponent();
            component = new ConcreteDecoratorA(component);
            component = new ConcreteDecoratorB(component);
            System.Console.WriteLine(component.Operation());
        }
    }
}
