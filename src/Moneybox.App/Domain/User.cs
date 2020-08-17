using System;

namespace Moneybox.App.Domain
{
    /// <summary>
    /// User data
    /// </summary>
    public class User
    {
        #region Properties

        /// <summary>
        /// The unique id for a given user
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid();
        }

        #endregion
    }
}
