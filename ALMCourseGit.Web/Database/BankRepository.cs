using ALMCourseGit.Web.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ALMCourseGit.Web.Database
{
    public static class BankRepository
    {
        private static List<Customer> Customers { get; set; }
        private static List<Account> Accounts { get; set; }
        public static List<Customer> GetCustomers()
        {
            return Customers;
        }

        public static List<Account> GetAccounts()
        {
            return Accounts;
        }

        public static void AddAccounts(List<Account> accounts)
        {
            Accounts = accounts;
        }

        public static void AddCustomers(List<Customer> customers)
        {
            Customers = customers;
        }

        public static string Withdraw(string id, string amount)
        {
            if (String.IsNullOrWhiteSpace(id) == false && String.IsNullOrWhiteSpace(amount) == false)
            {
                if (int.TryParse(id.Replace(" ", ""), out int accountId) && accountId > 0)
                {
                    if (decimal.TryParse(amount.Replace(" ", ""), out decimal withdrawAmount) && withdrawAmount > 0)
                    {
                        var accounts = GetAccounts();
                        foreach (var account in accounts)
                        {
                            if (account.AccountId == accountId)
                            {
                                if (account.Balance >= withdrawAmount)
                                {
                                    account.Balance -= withdrawAmount;
                                    return "The selected amount " + withdrawAmount + " has been withdrawn from account: " + account.AccountId + ". Current balance: " + account.Balance + ".";
                                }
                                return "Insufficient funds.";
                            }
                            return "Account not found.";
                        }
                    }
                    return "The amount has to be a number greater than 0.";
                }
                return "Id must be a number greater than 0.";
            }
            return "Please enter valid Id and amount.";
        }

        public static string Deposit(string id, string amount)
        {
            if (String.IsNullOrWhiteSpace(id) == false && String.IsNullOrWhiteSpace(amount) == false)
            {
                if (int.TryParse(id.Replace(" ", ""), out int accountId) && accountId > 0)
                {
                    if (decimal.TryParse(amount.Replace(" ", ""), out decimal depositAmount) && depositAmount > 0)
                    {
                        var accounts = GetAccounts();
                        foreach (var account in accounts)
                        {
                            if (account.AccountId == accountId)
                            {
                                account.Balance += depositAmount;
                                return "The selected amount " + depositAmount + " has been deposited to account: " + account.AccountId + ". Current balance: " + account.Balance + ".";
                            }
                            return "Account not found.";
                        }
                    }
                    return "The amount has to be a number greater than 0.";
                }
                return "Id must be a number greater than 0.";
            }
            return "Please enter valid Id and amount.";
        }

        public static string Transfer(int senderId, int receiverId, decimal amount)
        {
            string msg = "Something went wrong. Please check that given account numbers are correct.";

            var accountSender = GetAccounts().FirstOrDefault(m => m.AccountId == senderId);
            var accountReceiver = GetAccounts().FirstOrDefault(m => m.AccountId == receiverId);

            if (accountSender != null && accountReceiver != null)
            {
                if (amount <= 0)
                {
                    msg = "You can't transfer a negative amount";
                }

                else if (amount > accountSender.Balance)
                {
                    msg = "You have insufficient founds";
                }

                else
                {
                    accountSender.Balance = accountSender.Balance - amount;
                    accountReceiver.Balance = accountReceiver.Balance + amount;

                    msg = $"Successfully transferred {amount} from account {accountSender.AccountId}. " +
                                  $"Current balance on accounts: {accountSender.AccountId} = {accountSender.Balance}. " +
                                  $"{accountReceiver.AccountId} = {accountReceiver.Balance}";
                }
            }

            return msg;
        }
    }
}
