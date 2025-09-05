// tutorial from https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/classes

namespace Classes;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890; // data member
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance
    {
        get // compute the balance when someone asks
        {
            decimal balance = 0;

            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }

    private readonly decimal _minimumBalance;

    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { } // constructor chaining

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        _minimumBalance = minimumBalance;

        if (initialBalance > 0) // deposits must be positive, but credit accounts can open with a 0 balance
        {
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive.");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        // if (amount <= 0)
        // {
        //     throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive.");
        // }

        // if (Balance - amount < _minimumBalance)
        // {
        //     throw new InvalidOperationException("Insufficient funds for this withdrawal.");
        // }

        // var withdrawal = new Transaction(-amount, date, note);
        // _allTransactions.Add(withdrawal);

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive.");
        }

        Transaction? overdractTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);

        if (overdractTransaction != null)
        {
            _allTransactions.Add(overdractTransaction);
        }
    }

    // separated from MakeWithdrawal to allow LineOfCreditAccounts to go over the credit limit with a fee instead of refusing the transaction
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Insufficient funds for this withdrawal.");
        }

        else
        {
            return default;
        }
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder(); // use stringbuilder class to format a string that contains one line for each transaction

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");

        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }

    public virtual void PerformMonthEndTransactions() { }
}