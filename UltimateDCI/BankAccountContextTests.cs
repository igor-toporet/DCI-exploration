namespace DCI.Exploration.UltimateDCI
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class BankAccountContextTests
    {
        // ReSharper disable InconsistentNaming

        private readonly IDictionary<Guid, BankAccount> _accountsRegistry=new Dictionary<Guid, BankAccount>();
        private BankAccount checkingAccount = new BankAccount(100);
        private BankAccount savingsAccount = new BankAccount();

        public BankAccountContextTests()
        {
            _accountsRegistry.Add(checkingAccount.Id,checkingAccount);
            _accountsRegistry.Add(savingsAccount.Id,savingsAccount);
        }

        [Fact]
        public void Play_UseCase_TransferFunds()
        {
            var accountContext = new BankAccountContext(
                _accountsRegistry, checkingAccount.Id, savingsAccount.Id);

            accountContext.TransferFunds(70);

            checkingAccount.Balance.Should().Be(30);
            savingsAccount.Balance.Should().Be(70);
        }

        // ReSharper restore InconsistentNaming
    }
}
