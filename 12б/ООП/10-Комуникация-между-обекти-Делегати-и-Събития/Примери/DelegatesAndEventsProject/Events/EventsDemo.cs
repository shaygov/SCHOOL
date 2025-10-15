using System;

namespace DelegatesAndEventsExample.Events
{
    public class EventsDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== ДЕМОНСТРАЦИЯ НА СЪБИТИЯ ===\n");

            var account = new BankAccount("BG123456789", 1000);
            var logger = new TransactionLogger();
            var monitor = new BalanceMonitor();
            var notifications = new NotificationService();

            account.BalanceChanged += monitor.OnBalanceChanged;
            account.TransactionCompleted += logger.OnTransactionCompleted;
            account.TransactionCompleted += notifications.OnTransactionCompleted;
            account.LowBalanceWarning += monitor.OnLowBalanceWarning;
            account.LowBalanceWarning += notifications.OnLowBalanceWarning;
            account.HighBalanceNotification += monitor.OnHighBalanceNotification;
            account.HighBalanceNotification += notifications.OnHighBalanceNotification;

            Console.WriteLine($"Начален баланс: {account.Balance:C}");
            Console.WriteLine();

            Console.WriteLine("Извършване на транзакции:");
            account.Deposit(500);
            account.Withdraw(200);
            account.Withdraw(1000);
            account.Withdraw(200);
            account.Deposit(15000);

            logger.DisplayTransactions();
        }
    }
}
