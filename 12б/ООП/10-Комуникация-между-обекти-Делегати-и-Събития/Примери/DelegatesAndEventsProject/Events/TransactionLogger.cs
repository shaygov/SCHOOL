using System;
using System.Collections.Generic;

namespace DelegatesAndEventsExample.Events
{
    public class TransactionLogger
    {
        private List<string> transactions;
        
        public TransactionLogger()
        {
            transactions = new List<string>();
        }
        
        public void OnTransactionCompleted(object sender, string transaction)
        {
            string logEntry = $"[{DateTime.Now:HH:mm:ss}] {transaction}";
            transactions.Add(logEntry);
            Console.WriteLine($"LOG: {logEntry}");
        }
        
        public void DisplayTransactions()
        {
            Console.WriteLine("\n=== История на транзакциите ===");
            if (transactions.Count == 0)
            {
                Console.WriteLine("Няма транзакции.");
                return;
            }
            
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
