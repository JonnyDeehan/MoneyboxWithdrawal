using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    /// <summary>
    /// Withdraw money from a given account
    /// </summary>
    public class WithdrawMoney
    {
        #region Fields

        private IAccountRepository _accountRepository;
        private INotificationService _notificationService;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountRepository">The account repository to access account data from</param>
        /// <param name="notificationService">The notification service to inform a given account user of certain updates</param>
        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Perform the withdrawal of funds from the account
        /// </summary>
        /// <param name="fromAccountId">The account id to withdraws fund from</param>
        /// <param name="amount">The amount in Pounds Sterling to withdraw</param>
        public void Execute(Guid fromAccountId, decimal amount)
        {
            var account = _accountRepository.GetAccountById(fromAccountId);

            if(account == null)
            {
                throw new InvalidOperationException($"Failed to withdraw from account. " +
                    $"Could not find the account with the provided account id: {fromAccountId}");
            }

            // Withdraw the amount from the account
            account.Withdraw(amount);

            // Update the account with the new balance
            _accountRepository.Update(account);

            try
            {
                // Check if the balance is low
                if (account.AreAccountFundsLow())
                {
                    _notificationService.NotifyApproachingPayInLimit(account.User.Email);
                }
            }
            catch(NullReferenceException exception)
            {
                Console.WriteLine($"Unable to send notification to user - invalid user object for account. Exception: {exception}");
            }
            catch(ArgumentNullException exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        #endregion
    }
}
