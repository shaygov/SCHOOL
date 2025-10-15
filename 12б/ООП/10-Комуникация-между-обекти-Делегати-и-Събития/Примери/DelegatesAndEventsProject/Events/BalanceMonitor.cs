using System;

namespace DelegatesAndEventsExample.Events
{
    public class BalanceMonitor
    {
        public void OnBalanceChanged(object sender, decimal newBalance)
        {
            Console.WriteLine($"Монитор: Балансът е променен на {newBalance:C}");
        }
        
        public void OnLowBalanceWarning(object sender, decimal balance)
        {
            Console.WriteLine($"ПРЕДУПРЕЖДЕНИЕ: Ниско салдо! Текущ баланс: {balance:C}");
        }
        
        public void OnHighBalanceNotification(object sender, decimal balance)
        {
            Console.WriteLine($"ИЗВЕСТИЕ: Високо салдо! Текущ баланс: {balance:C}");
        }
    }
}
