using ALMCourseGit.Web.Database;
using ALMCourseGit.Web.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ALMCourseGit.Tests
{
    public class DepositTest
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
        public void SuccessfullDeposit()
        {
            var account = Customer.Accounts.First();
            var depositAmount = "500";
            var expectedReturn = "The selected amount has been deposited to account:" + account.AccountId + ".";
            var expectedBalance = 5500M;

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Deposit(account.AccountId.ToString(), depositAmount);

            Assert.Equal(result, expectedReturn);
            Assert.Equal(account.Balance, expectedBalance);
        }

        [Fact]
        public void DepositNegativeNumber()
        {
            var account = Customer.Accounts.First();
            var depositAmount = "-500";
            var expected = "The amount has to be a number greater than 0.";

            var result = BankRepository.Deposit(account.AccountId.ToString(), depositAmount);

            Assert.Equal(result, expected);  
        }

        [Fact]
        public void AccountIdIsNegative()
        {
            var depositAmount = "500";
            var expected = "Id must be a number greater than 0.";

            var result = BankRepository.Deposit("-5", depositAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AccountIdIsLetters()
        {
            var depositAmount = "500";
            var expected = "Id must be a number greater than 0.";

            var result = BankRepository.Deposit("Five", depositAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AmountIsLetters()
        {
            var account = Customer.Accounts.First();
            var depositAmount = "Five Hundred";
            var expected = "The amount has to be a number greater than 0.";

            var result = BankRepository.Deposit(account.AccountId.ToString(), depositAmount);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void AccountNotFound()
        {
            var depositAmount = "500";
            var expected = "Account not found.";

            BankRepository.AddAccounts(Customer.Accounts);

            var result = BankRepository.Deposit("123142", depositAmount);

            Assert.Equal(result, expected);
        }
    }
}
