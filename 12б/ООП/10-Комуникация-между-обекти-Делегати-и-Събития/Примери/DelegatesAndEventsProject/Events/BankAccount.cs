using System;

namespace DelegatesAndEventsExample.Events
{
    public class BankAccount
    {
        private decimal balance;
        private string accountNumber;
        
        public event EventHandler<decimal> BalanceChanged;
        public event EventHandler<string> TransactionCompleted;
        public event EventHandler<decimal> LowBalanceWarning;
        public event EventHandler<decimal> HighBalanceNotification;
        
        public decimal Balance
        {
            get { return balance; }
            private set
            {
                decimal oldBalance = balance;
                balance = value;
                
                BalanceChanged?.Invoke(this, balance);
                
                if (balance < 100 && oldBalance >= 100)
                {
                    LowBalanceWarning?.Invoke(this, balance);
                }
                
                if (balance > 10000 && oldBalance <= 10000)
                {
                    HighBalanceNotification?.Invoke(this, balance);
                }
            }
        }
        
        public string AccountNumber
        {
            get { return accountNumber; }
        }
        
        public BankAccount(string accountNumber, decimal initialBalance = 0)
        {
            this.accountNumber = accountNumber;
            this.balance = initialBalance;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                TransactionCompleted?.Invoke(this, $"Депозит от {amount:C}");
            }
        }
        
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
                TransactionCompleted?.Invoke(this, $"Теглене от {amount:C}");
                return true;
            }
            TransactionCompleted?.Invoke(this, $"Неуспешно теглене от {amount:C} - недостатъчен баланс");
            return false;
        }
    }
}
