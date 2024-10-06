namespace Inlamningsuppgiftobjektorienteradprogrammering
{
    using System;

    namespace SimpleBankSystem
    {
        // BankAccount-klassen representerar ett konto med grundläggande funktioner som insättning och uttag
        class BankAccount
        {
            public string AccountHolderName { get; private set; } // Namn på kontoinnehavaren
            public string AccountNumber { get; private set; } // Kontonummer (unik identifierare)
            public decimal Balance { get; private set; } // Kontots saldo

            // Konstruktor för att skapa ett konto med kontoinnehavarens namn, kontonummer och startsaldo
            public BankAccount(string accountHolderName, string accountNumber, decimal initialBalance)
            {
                AccountHolderName = accountHolderName;
                AccountNumber = accountNumber;
                Balance = initialBalance;
            }

            // Metod för insättning av pengar till kontot
            public void Deposit(decimal amount)
            {
                if (amount > 0) // Säkerställ att beloppet är positivt
                {
                    Balance += amount; // Lägg till insättningsbeloppet till saldot
                    Console.WriteLine($"Successfully deposited {amount:C} to account {AccountNumber}. New balance: {Balance:C}");
                }
                else
                {
                    // Hantering om insättningen är negativ eller noll
                    Console.WriteLine("Deposit amount must be positive.");
                }
            }

            // Metod för att ta ut pengar från kontot
            public void Withdraw(decimal amount)
            {
                if (amount > 0 && amount <= Balance) // Kontrollera att beloppet är positivt och inte överstiger saldot
                {
                    Balance -= amount; // Minska saldot med uttagsbeloppet
                    Console.WriteLine($"Successfully withdrew {amount:C} from account {AccountNumber}. New balance: {Balance:C}");
                }
                else if (amount > Balance)
                {
                    // Hantering om uttagsbeloppet överstiger saldot
                    Console.WriteLine("Insufficient funds.");
                }
                else
                {
                    // Hantering om uttagsbeloppet är negativt eller noll
                    Console.WriteLine("Withdrawal amount must be positive.");
                }
            }

            // Metod för att kontrollera saldo
            public void CheckBalance()
            {
                Console.WriteLine($"Account {AccountNumber} owned by {AccountHolderName} has a balance of {Balance:C}");
            }
        }

        // BankSystem-klassen hanterar kontohantering och transaktioner
        class BankSystem
        {
            private BankAccount personalAccount; // Personkonto
            private BankAccount savingsAccount;  // Sparkonto
            private BankAccount investmentAccount; // Investeringskonto

            // Konstruktor för att skapa bankkonton med initiala saldon
            public BankSystem()
            {
                Console.WriteLine("Welcome to the Simple Bank System!");

                // Skapa tre olika konton med standardvärden
                personalAccount = new BankAccount("Alice", "101", 5000);
                savingsAccount = new BankAccount("Alice", "102", 10000);
                investmentAccount = new BankAccount("Alice", "103", 20000);
            }

            // Metod för att starta systemet och hantera användarinmatningar
            public void Start()
            {
                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. Deposit");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Transfer");
                    Console.WriteLine("4. Check Balance");
                    Console.WriteLine("5. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            HandleDeposit();
                            break;
                        case "2":
                            HandleWithdraw();
                            break;
                        case "3":
                            HandleTransfer();
                            break;
                        case "4":
                            HandleCheckBalance();
                            break;
                        case "5":
                            isRunning = false; // Avsluta programmet
                            Console.WriteLine("Exiting the system. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
            }

            // Metod för att hantera insättningar
            private void HandleDeposit()
            {
                Console.WriteLine("Enter the account number for deposit:");
                string accountNumber = Console.ReadLine();

                BankAccount account = GetAccountByNumber(accountNumber);

                if (account != null)
                {
                    Console.WriteLine("Enter the amount to deposit:");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        account.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }

            // Metod för att hantera uttag
            private void HandleWithdraw()
            {
                Console.WriteLine("Enter the account number for withdrawal:");
                string accountNumber = Console.ReadLine();

                BankAccount account = GetAccountByNumber(accountNumber);

                if (account != null)
                {
                    Console.WriteLine("Enter the amount to withdraw:");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        account.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }

            // Metod för att hantera överföringar
            private void HandleTransfer()
            {
                Console.WriteLine("Enter the account number to transfer from:");
                string fromAccountNumber = Console.ReadLine();

                BankAccount fromAccount = GetAccountByNumber(fromAccountNumber);

                if (fromAccount != null)
                {
                    Console.WriteLine("Enter the account number to transfer to:");
                    string toAccountNumber = Console.ReadLine();

                    BankAccount toAccount = GetAccountByNumber(toAccountNumber);

                    if (toAccount != null)
                    {
                        Console.WriteLine("Enter the amount to transfer:");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            if (fromAccount.Balance >= amount)
                            {
                                fromAccount.Withdraw(amount);
                                toAccount.Deposit(amount);
                                Console.WriteLine($"Successfully transferred {amount:C} from {fromAccountNumber} to {toAccountNumber}.");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds for the transfer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Destination account not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Source account not found.");
                }
            }

            // Metod för att hantera saldoavfrågningar
            private void HandleCheckBalance()
            {
                Console.WriteLine("Enter the account number to check balance:");
                string accountNumber = Console.ReadLine();

                BankAccount account = GetAccountByNumber(accountNumber);

                if (account != null)
                {
                    account.CheckBalance();
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }

            // Metod för att hitta ett konto baserat på kontonummer
            private BankAccount GetAccountByNumber(string accountNumber)
            {
                if (personalAccount.AccountNumber == accountNumber)
                    return personalAccount;
                else if (savingsAccount.AccountNumber == accountNumber)
                    return savingsAccount;
                else if (investmentAccount.AccountNumber == accountNumber)
                    return investmentAccount;
                else
                    return null; // Om inget konto matchar
            }
        }

        // Main-programmet startar banksystemet
        class Program
        {
            static void Main(string[] args)
            {
                BankSystem bankSystem = new BankSystem(); // Skapa bankens system
                bankSystem.Start(); // Starta systemet och hantera användarinmatningar
            }
        }
    }

}
