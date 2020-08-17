using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    /// <summary>
    /// Operations for transfering money from one account to another
    /// </summary>
    public class TransferMoney
    {
        #region Fields

        private IAccountRepository _accountRepository;
        private INotificationService _notificationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accountRepository">The data accessor for accounts</param>
        /// <param name="notificationService">A notification service provider</param>
        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Transfers the provided amount from one account to another, as per
        /// the account ids provided.
        /// </summary>
        /// <param name="fromAccountId">The source account id</param>
        /// <param name="toAccountId">The destination account id</param>
        /// <param name="amount">The amount of money to transfer in pounds sterling</param>
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var sourceAccount = _accountRepository.GetAccountById(fromAccountId);
            var destinationAccount = _accountRepository.GetAccountById(toAccountId);

            // Withdraw from the provided source account
            sourceAccount.Withdraw(amount);

            // Deposit into the provided destination account
            destinationAccount.Deposit(amount);

            // Update each respective account
            _accountRepository.Update(sourceAccount);
            _accountRepository.Update(destinationAccount);

            try
            {
                if (sourceAccount.AreAccountFundsLow())
                {
                    _notificationService.NotifyFundsLow(sourceAccount.User.Email);
                }

                if (destinationAccount.IsAccountReachingPayInLimit())
                {
                    _notificationService.NotifyApproachingPayInLimit(destinationAccount.User.Email);
                }
            }
            catch (NullReferenceException exception)
            {
                Console.WriteLine($"Unable to send notification to user - invalid user object for account. Exception: {exception}");
            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        #endregion
    }
}
