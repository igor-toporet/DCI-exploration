namespace DCI.Exploration.TraditionalOOP
{
    using System;

    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void TransferTo(BankAccount destinationAccount, decimal amount)
        {
            Withdraw(amount);
            destinationAccount.Deposit(amount);
        }

        public void Withdraw(decimal amount)
        {
            if (amount>Balance)
            {
                string message = string.Format(
                    "Insufficient funds. Current balace: {0}, attempted withdrawal: {1}.",
                    Balance, amount);
                throw new InvalidOperationException(message);
            }
            Balance -= amount;
        }
    }
}
