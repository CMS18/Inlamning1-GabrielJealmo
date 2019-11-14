using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALMCourseGit.Web.Database;
using ALMCourseGit.Web.Database.Entities;
using Xunit;

namespace ALMCourseGit.Tests
{
    public class TransferTests
    {
        private readonly Customer Customer = new Customer()
        {
            CustomerId = 1,
            Name = "Test",
            Accounts = new List<Account>()
            {
                new Account()
                {
                    AccountId = 1,
                    Balance = 5000.0M
                },
                new Account()
                {
                    AccountId = 2,
                    Balance = 5000.0M
                }
            }
        };

        [Fact]
        public void SuccessfullTransfer()
        {
            var accountSender = Customer.Accounts.First();
            var accountReceiver = Customer.Accounts.Last();
            decimal transferAmount = 500M;
            var expectedBalanceOnSenderAccount = 4500M;
            var expectedBalanceOnReceiverAccount = 5500M;
            var expectedMsg = $"Successfully transferred {transferAmount} from account {accountSender.AccountId}. " +
                  $"Current balance on accounts: {accountSender.AccountId} = {accountSender.Balance}. " +
                  $"{accountReceiver.AccountId} = {accountReceiver.Balance}";

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Transfer(accountSender.AccountId, accountReceiver.AccountId, transferAmount);

            Assert.Equal(result, expectedMsg);
            Assert.Equal(expectedBalanceOnSenderAccount, accountSender.Balance);
            Assert.Equal(expectedBalanceOnReceiverAccount, accountReceiver.Balance);
        }

        [Fact]
        public void TransferAmountTooHigh()
        {
            var accountSender = Customer.Accounts.First();
            var accountReceiver = Customer.Accounts.Last();
            decimal transferAmount = 50000M;
            var expectedBalanceOnSenderAccount = 5000M;
            var expectedBalanceOnReceiverAccount = 5000M;
            var expectedMsg = "You have insufficient founds";

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Transfer(accountSender.AccountId, accountReceiver.AccountId, transferAmount);

            Assert.Equal(result, expectedMsg);
            Assert.Equal(expectedBalanceOnSenderAccount, accountSender.Balance);
            Assert.Equal(expectedBalanceOnReceiverAccount, accountReceiver.Balance);
        }
    }
}
