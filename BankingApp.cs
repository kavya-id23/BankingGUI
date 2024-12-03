using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set console output encoding to UTF-8 to support the ₹ symbol
            Console.OutputEncoding = Encoding.UTF8;

            Bank bank = new Bank();
            bank.Start();
        }
    }

    public class Bank
    {
        private Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        public Bank()
        {
            // Pre-register 10 accounts
            string[] usernames = { "user1", "user2", "user3", "user4", "user5", "user6", "user7", "user8", "user9", "guru" };
            string password = "1234"; // All accounts have the same password for simplicity

            foreach (var username in usernames)
            {
                accounts[username] = new Account(username, password);
            }
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Banking Application!");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (accounts.ContainsKey(username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                return;
            }

            accounts[username] = new Account(username, password);
            Console.WriteLine("Registration successful!");
        }

        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (accounts.ContainsKey(username) && accounts[username].Password == password)
            {
                Console.WriteLine("Login successful!");
                AccountMenu(accounts[username]);
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
            }
        }

        private void AccountMenu(Account account)
        {
            while (true)
            {
                Console.WriteLine("\nAccount Menu");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. View Transaction History");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Deposit(account);
                        break;
                    case "2":
                        Withdraw(account);
                        break;
                    case "3":
                        CheckBalance(account);
                        break;
                    case "4":
                        ViewTransactionHistory(account);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void Deposit(Account account)
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            account.Deposit(amount);
            Console.WriteLine(string.Format("Successfully deposited ₹{0:N2}. New balance: ₹{1:N2}", amount, account.Balance));
        }

        private void Withdraw(Account account)
        {
            Console.Write("Enter amount to withdraw: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (account.Withdraw(amount))
            {
                Console.WriteLine(string.Format("Successfully withdrew ₹{0:N2}. New balance: ₹{1:N2}", amount, account.Balance));
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        private void CheckBalance(Account account)
        {
            Console.WriteLine(string.Format("Current balance: ₹{0:N2}", account.Balance));
        }

        private void ViewTransactionHistory(Account account)
        {
            Console.WriteLine("Transaction History:");
            foreach (var transaction in account.TransactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }
    }

    public class Account
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public decimal Balance { get; private set; }
        public List<string> TransactionHistory { get; private set; }

        public Account(string username, string password)
        {
            Username = username;
            Password = password;
            Balance = 0;
            TransactionHistory = new List<string>();
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            TransactionHistory.Add(string.Format("Deposited ₹{0:N2} on {1}", amount, DateTime.Now));
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                TransactionHistory.Add(string.Format("Withdrew ₹{0:N2} on {1}", amount, DateTime.Now));
                return true;
            }
            return false;
        }
    }
}
