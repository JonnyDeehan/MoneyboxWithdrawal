using System;

namespace Moneybox.App.Domain.Services
{
    /// <summary>
    /// Notification Service to inform a given user about their account balance status.
    /// </summary>
    public class NotificationService : INotificationService
    {

        #region INotificationService Implementation

        /// <summary>
        /// Notifies a user that their pay-in limit has almost been reached.
        /// </summary>
        /// <param name="emailAddress">The email address of the user to send a notification to.</param>
        public void NotifyApproachingPayInLimit(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("Invalid email address supplied for the notification");
            }

            Console.WriteLine($"To: {emailAddress}. Your account balance is approaching its pay in limit. Please consider this before attempting to add further funds to your account.");
        }

        /// <summary>
        /// Notifies a user that their account balance funds are low.
        /// </summary>
        /// <param name="emailAddress">The email address of the user to send a notification to.</param>
        public void NotifyFundsLow(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException("Invalid email address supplied for the notification");
            }

            Console.WriteLine($"To: {emailAddress}. Your account balance funds are low. Please consider this before withdrawing further funds from your account.");
        }

        #endregion
    }
}
