namespace StructuralPatternsExamples
{
    /*
     Bridge (Мост)
     - Цел: Разделя абстракцията (Abstraction) от реализацията (IImplementation),
       така че и двете да могат да се променят независимо.
     - В примера: RefinedAbstraction използва IImplementation; можем да комбинираме
       всяка абстракция с различна реализация.
    */
    public interface IImplementation { string OperationImpl(); }

    public class ConcreteImplA : IImplementation { public string OperationImpl() => "ImplA"; }
    public class ConcreteImplB : IImplementation { public string OperationImpl() => "ImplB"; }

    public abstract class Abstraction
    {
        protected IImplementation _impl;
        protected Abstraction(IImplementation impl) { _impl = impl; }
        public abstract string Operation();
    }

    public class RefinedAbstraction : Abstraction
    {
        public RefinedAbstraction(IImplementation impl) : base(impl) { }
        public override string Operation() => $"Refined + {_impl.OperationImpl()}";
    }

    public static class BridgeExample
    {
        /// <summary>
        /// Демонстрация: комбинираме една абстракция с различни реализации.
        /// Очакван изход:
        /// Refined + ImplA
        /// Refined + ImplB
        /// </summary>
        public static void Demo()
        {
            var a = new RefinedAbstraction(new ConcreteImplA());
            var b = new RefinedAbstraction(new ConcreteImplB());
            System.Console.WriteLine(a.Operation());
            System.Console.WriteLine(b.Operation());
        }
    }
}
