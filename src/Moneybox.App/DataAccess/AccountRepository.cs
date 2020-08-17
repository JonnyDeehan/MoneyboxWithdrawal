using System;
using System.Collections.Generic;
using Moneybox.App.Domain;

namespace Moneybox.App.DataAccess
{
    /// <summary>
    /// Account Repository Accessor
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        #region Properties

        public Dictionary<Guid, Account> Accounts { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountRepository()
        {
             Accounts = new Dictionary<Guid, Account>();
        }

        #endregion

        #region IAccount Repository Implementation

        /// <summary>
        /// Retrieves the account for a given ID
        /// </summary>
        /// <param name="accountId">The id of the account to retrieve</param>
        /// <returns>The Account</returns>
        public Account GetAccountById(Guid accountId)
        {
            var account = Accounts[accountId];

            if(account == null)
            {
                throw new InvalidOperationException();
            }

            return account;
        }

        /// <summary>
        /// Update a given account's details
        /// </summary>
        /// <param name="account">The account to update</param>
        public void Update(Account account)
        {
            if(account == null)
            {
                throw new ArgumentNullException();
            }
            // Update an existing account
            else if (Accounts.ContainsKey(account.Id))
            {
                Accounts[account.Id] = account;
            }
            // Add a new account to the stored collection.
            else if(account.Id != Guid.Empty)
            {
                Accounts.Add(account.Id, account);
            }
            else
            {
                throw new InvalidOperationException("Account ID value is not a valid GUID.");
            }
        }

        #endregion
    }
}
