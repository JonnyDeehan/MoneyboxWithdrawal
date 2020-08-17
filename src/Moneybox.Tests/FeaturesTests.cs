using Moneybox.App.Features;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain;
using System;
using Xunit;
using System.Collections.Generic;

namespace Moneybox.Tests
{
    public class FeaturesTests
    {
        #region Fields

        private User _userA;
        private User _userB;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FeaturesTests()
        {
            _userA = new User()
            {
                Name = "UserA",
                Email = "emailA"
            };

            _userB = new User()
            {
                Name = "UserB",
                Email = "emailB"
            };
        }

        #endregion

        #region Transfer Money Test Methods

        [Theory]
        [InlineData(50, 100)]
        public void TransferMoney_Execute_AmountToWithdrawExceedsBalance_ThrowsInvalidOperationException(decimal balance, decimal transferAmount)
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var sourceAccount = new Account(_userA);
            sourceAccount.Deposit(balance);
            accountRepository.Update(sourceAccount);

            var destinationAccount = new Account(_userB);
            accountRepository.Update(destinationAccount);

            var notificationService = new NotificationService();
            var transferMoneyFeature = new TransferMoney(accountRepository, notificationService);

            // Assert
            Assert.Throws<InvalidOperationException>(() => transferMoneyFeature.Execute(sourceAccount.Id, destinationAccount.Id, transferAmount));
        }

        [Fact]
        public void TransferMoney_Execute_AmountToPayInExceedsPayInLimit_ThrowsInvalidOperationException()
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var sourceAccount = new Account(_userA);
            sourceAccount.Deposit(3000m);
            accountRepository.Update(sourceAccount);

            var destinationAccount = new Account(_userB);
            destinationAccount.Deposit(2000m);
            accountRepository.Update(destinationAccount);

            var notificationService = new NotificationService();
            var transferMoneyFeature = new TransferMoney(accountRepository, notificationService);

            // Assert
            Assert.Throws<InvalidOperationException>(() => transferMoneyFeature.Execute(sourceAccount.Id, destinationAccount.Id, 2500m));
        }

        [Theory]
        [InlineData(100, 100)]
        public void TransferMoney_Execute_SuccessfullTransfer(decimal balance, decimal transferAmount)
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var sourceAccount = new Account(_userA);
            sourceAccount.Deposit(balance);
            accountRepository.Update(sourceAccount);

            var destinationAccount = new Account(_userB);
            accountRepository.Update(destinationAccount);

            var notificationService = new NotificationService();
            var transferMoneyFeature = new TransferMoney(accountRepository, notificationService);

            // Act
            transferMoneyFeature.Execute(sourceAccount.Id, destinationAccount.Id, transferAmount);

            // Assert
            Assert.Equal(0, accountRepository.GetAccountById(sourceAccount.Id).Balance);
            Assert.Equal(100, accountRepository.GetAccountById(destinationAccount.Id).Balance);
            Assert.Equal(100, accountRepository.GetAccountById(destinationAccount.Id).PaidIn);
        }

        #endregion

        #region Withdraw Money Test Methods

        [Fact]
        public void WithdrawMoney_Execute_InvalidAccountId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var account = new Account(_userA);
            account.Deposit(100);
            accountRepository.Update(account);

            var notificationService = new NotificationService();
            var withdrawMoneyFeature = new WithdrawMoney(accountRepository, notificationService);

            // Assert
            Assert.Throws<KeyNotFoundException>(() => withdrawMoneyFeature.Execute(Guid.NewGuid(), 100));
        }

        [Fact]
        public void WithdrawMoney_Execute_WithdrawAmountExceedsBalance_ThrowsInvalidOperationException()
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var account = new Account(_userA);
            account.Deposit(100);
            accountRepository.Update(account);

            var notificationService = new NotificationService();
            var withdrawMoneyFeature = new WithdrawMoney(accountRepository, notificationService);

            // Assert
            Assert.Throws<InvalidOperationException>(() => withdrawMoneyFeature.Execute(account.Id, 101));
        }

        [Fact]
        public void WithdrawMoney_Execute_WithdrawalSuccessful()
        {
            // Arrange
            var accountRepository = new AccountRepository();

            var account = new Account(_userA);
            account.Deposit(101);
            accountRepository.Update(account);

            var notificationService = new NotificationService();
            var withdrawMoneyFeature = new WithdrawMoney(accountRepository, notificationService);

            // Act
            withdrawMoneyFeature.Execute(account.Id, 100);

            // Assert
            Assert.Equal(1, accountRepository.GetAccountById(account.Id).Balance);
        }

        #endregion
    }
}
