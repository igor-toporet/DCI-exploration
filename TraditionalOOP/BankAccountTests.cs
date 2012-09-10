namespace DCI.Exploration.TraditionalOOP
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class BankAccountTests
    {
        private readonly BankAccount _bankAccount = new BankAccount();

        // ReSharper disable InconsistentNaming

        [Fact]
        public void BankAccount_when_created_has_balace_zero()
        {
            _bankAccount.Balance.Should().Be(decimal.Zero);
        }

        [Fact]
        public void BankAccount_when_deposited_has_increased_balace()
        {
            const decimal amount = 10;

            _bankAccount.Deposit(amount);

            _bankAccount.Balance.Should().Be(amount);
        }

        [Fact]
        public void BankAccount_when_withdrawn_has_decreased_balace()
        {
            _bankAccount.Deposit(100);
            
            _bankAccount.Withdraw(1);

            _bankAccount.Balance.Should().Be(99);
        }

        [Fact]
        public void BankAccount_withdrawal_throws_when_exceeds_balance()
        {
            _bankAccount.Deposit(10);

            var exception = Record.Exception(() => _bankAccount.Withdraw(11));

            exception.Should().BeOfType<InvalidOperationException>();
            exception.Message.Should().Be("Insufficient funds. Current balace: 10, attempted withdrawal: 11.");
        }

        [Fact]
        public void BankAccount_failed_withdrawal_keeps_balance_intact()
        {
            _bankAccount.Deposit(10);

            Record.Exception(() => _bankAccount.Withdraw(11));

            _bankAccount.Balance.Should().Be(10);
        }

        [Fact]
        public void BankAccount_transfer_changes_balances()
        {
            _bankAccount.Deposit(10);
            var destinationAccount = new BankAccount();

            _bankAccount.TransferTo(destinationAccount,7);

            _bankAccount.Balance.Should().Be(3);
            destinationAccount.Balance.Should().Be(7);
        }

        [Fact]
        public void BankAccount_transfer_throws_when_exceeds_balance()
        {
            _bankAccount.Deposit(10);
            var destinationAccount = new BankAccount();

            var exception = Record.Exception(() => _bankAccount.TransferTo(destinationAccount, 11));

            exception.Should().BeOfType<InvalidOperationException>();
            exception.Message.Should().Be("Insufficient funds. Current balace: 10, attempted withdrawal: 11.");
        }

        [Fact]
        public void BankAccount_failed_transfer_keeps_balances_of_both_involved_accounts_intact()
        {
            _bankAccount.Deposit(10);
            var destinationAccount = new BankAccount();

            Record.Exception(() => _bankAccount.TransferTo(destinationAccount, 11));

            _bankAccount.Balance.Should().Be(10);
            destinationAccount.Balance.Should().Be(decimal.Zero);
        }

        // ReSharper restore InconsistentNaming
    }
}
