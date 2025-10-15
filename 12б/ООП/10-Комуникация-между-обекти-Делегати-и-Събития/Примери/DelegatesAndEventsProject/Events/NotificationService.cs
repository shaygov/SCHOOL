using System;

namespace DelegatesAndEventsExample.Events
{
    public class NotificationService
    {
        public void OnTransactionCompleted(object sender, string transaction)
        {
            Console.WriteLine($"Уведомление: {transaction}");
        }
        
        public void OnLowBalanceWarning(object sender, decimal balance)
        {
            Console.WriteLine($"SMS: Вашият баланс е нисък: {balance:C}");
        }
        
        public void OnHighBalanceNotification(object sender, decimal balance)
        {
            Console.WriteLine($"Email: Поздравления! Имате висок баланс: {balance:C}");
        }
    }
}
