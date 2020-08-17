using System;
using Moneybox.App.Domain.Constants;

namespace Moneybox.App.Domain
{
    /// <summary>
    /// Account Data
    /// </summary>
    public class Account
    {
        #region Properties

        /// <summary>
        /// The unique id of the account
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The user who this account belongs to
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// The current balance of the account
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// The amount withdrawn from the account
        /// </summary>
        public decimal Withdrawn { get; private set; }

        /// <summary>
        /// The amount paid into the account
        /// </summary>
        public decimal PaidIn { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user">The user who this account belongs to.</param>
        public Account(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Withdraws an amount from the account.
        /// </summary>
        /// <param name="amount">The amount in Pounds Sterling to withdraw</param>
        public void Withdraw(decimal amount)
        {
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException($"The amount ({amount}) to withdraw" +
                    $" exceeds the available balance ({Balance}) in the account.");
            }

            Balance -= amount;
            Withdrawn -= amount;
        }

        /// <summary>
        /// Deposits an amount into the account
        /// </summary>
        /// <param name="amount">The amount in Pounds Serling to deposit</param>
        public void Deposit(decimal amount)
        {
            if (PaidIn + amount > AccountBalanceConstants.PayInLimit)
            {
                throw new InvalidOperationException("The pay in limit for the account has reached its limit");
            }

            Balance += amount;
            PaidIn += amount;
        }

        /// <summary>
        /// For the current balance of the account, is it on the lower end of the
        /// spectrum? (defined in 'AccountBalanceConstants.AccountFundsLowAmount'
        /// </summary>
        /// <returns>If the account funds are low or not</returns>
        public bool AreAccountFundsLow()
        {
            return Balance < AccountBalanceConstants.AccountFundsLowAmount;
        }

        /// <summary>
        /// Is the account reaching close to its 'Pay In' limit?
        /// </summary>
        /// <returns>If the account is close to the pay in limit</returns>
        public bool IsAccountReachingPayInLimit()
        {
            return AccountBalanceConstants.PayInLimit - PaidIn < AccountBalanceConstants.AccountFundsLowAmount;
        }

        #endregion
    }
}
