using ALMCourseGit.Web.Database;
using ALMCourseGit.Web.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ALMCourseGit.Tests
{
    public class WithdrawalTest
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
                }
            }
        };

        [Fact]
        public void SuccessfullWithdrawal()
        {
            var account = Customer.Accounts.First();
            var withdrawalAmount = "500";
            var expectedReturn = "The selected amount has been withdrawn from account:" + account.AccountId + ".";
            var expectedBalance = 4500M;

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Withdraw(account.AccountId.ToString(), withdrawalAmount);

            Assert.Equal(result, expectedReturn);
            Assert.Equal(account.Balance, expectedBalance);
        }

        [Fact]
        public void WithdrawalNegativeNumbers()
        {
            var account = Customer.Accounts.First();
            var withdrawalAmount = "-500";
            var expected = "The amount has to be a number greater than 0.";

            var result = BankRepository.Withdraw(account.AccountId.ToString(), withdrawalAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void WithdrawalAmountTooHigh()
        {
            var account = Customer.Accounts.First();
            var withdrawalAmount = "5500";
            var expected = "Insufficient funds.";

            var result = BankRepository.Withdraw(account.AccountId.ToString(), withdrawalAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AccountIdIsNegative()
        {
            var withdrawalAmount = "500";
            var expected = "Id must be a number greater than 0.";

            var result = BankRepository.Withdraw("-5", withdrawalAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AccountIdIsLetters()
        {
            var withdrawalAmount = "500";
            var expected = "Id must be a number greater than 0.";

            var result = BankRepository.Withdraw("Five", withdrawalAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AmountIsLetters()
        {
            var account = Customer.Accounts.First();
            var withdrawalAmount = "Five Hundred";
            var expected = "The amount has to be a number greater than 0.";

            var result = BankRepository.Withdraw(account.AccountId.ToString(), withdrawalAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AccountNotFound()
        {
            var withdrawalAmount = "500";
            var expected = "Account not found.";

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Withdraw("123142", withdrawalAmount);

            Assert.Equal(result, expected);
        }
    }
}
