namespace DCI.Exploration.UltimateDCI
{
    using System;
    using System.Collections.Generic;

    public class BankAccount : Entity
    {
        public BankAccount(decimal initialBalance)
        {
            IncreaseBalanceBy(initialBalance);
        }

        public BankAccount()
        {
        }

        public decimal Balance { get; private set; }

        public void IncreaseBalanceBy(decimal amount)
        {
            Balance += amount;
        }

        public void DecreaseBalanceBy(decimal amount)
        {
            if (amount > Balance)
            {
                string message = string.Format(
                    "Insufficient funds. Current balace: {0}, attempted withdrawal: {1}.",
                    Balance, amount);
                throw new InvalidOperationException(message);
            }
            Balance -= amount;
        }
    }

    public abstract class Entity : IIdentifyable<Guid>
    {
        protected Entity() : this(Guid.NewGuid())
        {
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public interface IIdentifyable<T> where T:struct 
    {
        T Id { get; set; }
    }

    public class BankAccountContext:Context
    {

        public BankAccountContext(
            IDictionary<Guid,BankAccount> accountsRegistry,
            Guid sourceAccountId,
            Guid destinationAccountId)
        {
            RegisterRole(new SourceAccount(accountsRegistry[sourceAccountId]));
            //RegisterRole<SourceAccount,BankAccount>(accountsRegistry[sourceAccountId]);
            RegisterRole(new DestinationAccount(accountsRegistry[destinationAccountId]));
        }

        public abstract class RoleSpecificToCurrentContextOnly<TDomain>: Role<TDomain>
        {
            protected RoleSpecificToCurrentContextOnly(TDomain roleContext) : base(roleContext)
            {
            }
        }

        public class SourceAccount : Role /*SpecificToCurrentContextOnly*/<BankAccount>
        {
            public SourceAccount(BankAccount roleContext) : base(roleContext)
            {
            }

            public SourceAccount() : base(null)
            {
                
            }

            public void Withdraw(decimal amount)
            {
                Self.DecreaseBalanceBy(amount);
            }
        }
        public class DestinationAccount : Role /*SpecificToCurrentContextOnly*/<BankAccount>
        {
            public DestinationAccount(BankAccount roleContext) : base(roleContext)
            {
            }

            public void Deposit(decimal amount)
            {
                Self.IncreaseBalanceBy(amount);
            }
        }
        // use case
        public void TransferFunds(decimal amount)
        {
            // this is use case algorithm (extremely straightforward thought)
            Role<SourceAccount>().Withdraw(amount);
            Role<DestinationAccount>().Deposit(amount);
        }
    }

}
