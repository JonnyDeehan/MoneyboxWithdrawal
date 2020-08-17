namespace Moneybox.App.Domain.Services
{
    /// <summary>
    /// Notification Service Interface - informs users of their balance status.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notifies a user that their pay-in limit has almost been reached.
        /// </summary>
        /// <param name="emailAddress">The email address of the user to send a notification to.</param>
        void NotifyApproachingPayInLimit(string emailAddress);

        /// <summary>
        /// Notifies a user that their account balance funds are low.
        /// </summary>
        /// <param name="emailAddress">The email address of the user to send a notification to.</param>
        void NotifyFundsLow(string emailAddress);
    }
}
