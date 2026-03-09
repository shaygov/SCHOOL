namespace StructuralPatternsExamples
{
    /*
     Facade (Фасада)
     - Цел: Предоставя опростен интерфейс към сложна подсистема, така че клиентът
       да не трябва да взаимодейства директно с множество класове.
     - В примера: Facade комбинира три подсистеми (A, B, C) и предоставя един метод Operation().
    */
    public class SubsystemA { public string A() => "A"; }
    public class SubsystemB { public string B() => "B"; }
    public class SubsystemC { public string C() => "C"; }

    public class Facade
    {
        private SubsystemA _a = new();
        private SubsystemB _b = new();
        private SubsystemC _c = new();
        public string Operation() => $"{_a.A()} + {_b.B()} + {_c.C()}";
    }

    public static class FacadeExample
    {
        /// <summary>
        /// Демонстрация: извикваме фасадата, която абстрахира детайлите на подсистемите.
        /// Очакван изход: A + B + C
        /// </summary>
        public static void Demo()
        {
            var facade = new Facade();
            System.Console.WriteLine(facade.Operation());
        }
    }
}
