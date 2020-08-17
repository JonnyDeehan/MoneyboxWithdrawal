namespace Moneybox.App.Domain.Constants
{
    /// <summary>
    /// Common Account Funds Constants
    /// </summary>
    public static class AccountBalanceConstants
    {
        /// <summary>
        /// The minimum amount of funds an account can have
        /// </summary>
        public const decimal MinimumAccountFundsAmount = 0m;

        /// <summary>
        /// The amount of funds available in an account that would trigger
        /// 'a notification of low funds' reported to the user of the account.
        /// </summary>
        public const decimal AccountFundsLowAmount = 500m;

        /// <summary>
        /// The pay in limit for the account in pounds sterling
        /// </summary>
        public const decimal PayInLimit = 4000m;
    }
}
