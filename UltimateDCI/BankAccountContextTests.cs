namespace DCI.Exploration.UltimateDCI
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class BankAccountContextTests
    {
        // ReSharper disable InconsistentNaming

        private readonly Guid _accountOneId;
        private readonly Guid _accountTwoId;
        private readonly IDictionary<Guid, BankAccount> _accountsRegistry;

        public BankAccountContextTests()
        {
            var account1 = new BankAccount(100);
            var account2 = new BankAccount();
            _accountOneId = account1.Id;
            _accountTwoId = account2.Id;
            _accountsRegistry = new Dictionary<Guid, BankAccount>
                                       {
                                           {_accountOneId,account1},
                                           {_accountTwoId,account2},
                                       };
        }

        [Fact]
        public void Play_UseCase_TransferFunds()
        {
            var accountContext = new BankAccountContext(_accountsRegistry, _accountOneId, _accountTwoId);

            accountContext.TransferFunds(70);


            _accountsRegistry[_accountOneId].Balance.Should().Be(30);
            destAcc.Balance.Should().Be(70);
        }

        // ReSharper restore InconsistentNaming
    }
}
