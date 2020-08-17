using System;
using Moneybox.App.Domain;

namespace Moneybox.App.DataAccess
{
    /// <summary>
    /// Account access interface
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieves the account for a given ID
        /// </summary>
        /// <param name="accountId">The id of the account to retrieve</param>
        /// <returns>The Account</returns>
        Account GetAccountById(Guid accountId);

        /// <summary>
        /// Update a given account's details
        /// </summary>
        /// <param name="account">The account to update</param>
        void Update(Account account);
    }
}
