namespace StructuralPatternsExamples
{
    /*
     Adapter (Адаптер)
     - Цел: Позволява използването на вече съществуващ клас (Adaptee / OldService)
       чрез нов очакван интерфейс (ITarget).
     - В примера: OldService предоставя SpecificRequest(), но клиентът очаква Request().
       Класът Adapter превежда повикванията от Request() към SpecificRequest().
    */
    public interface ITarget { string Request(); }

    public class OldService { public string SpecificRequest() => "OldService result"; }

    public class Adapter : ITarget
    {
        private readonly OldService _adaptee;
        public Adapter(OldService adaptee) { _adaptee = adaptee; }
        public string Request() => _adaptee.SpecificRequest();
    }

    public static class AdapterExample
    {
        /// <summary>
        /// Демонстрация: показва как клиентът използва ITarget, а Adapter препраща към OldService.
        /// Очакван изход: "Adapter -> OldService result"
        /// </summary>
        public static void Demo()
        {
            var adaptee = new OldService();
            ITarget adapter = new Adapter(adaptee);
            System.Console.WriteLine($"Adapter -> {adapter.Request()}");
        }
    }
}
