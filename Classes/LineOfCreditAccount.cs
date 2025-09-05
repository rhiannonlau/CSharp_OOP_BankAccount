using Classes;

// line of credit accounts can have negative balance, but when a balance exists, there's an interest charge each month
public class LineOfCreditAccount : BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit) { }

    public override void PerformMonthEndTransactions()
    {
        if (Balance < 0)
        {
            // Negate the balance to get a positive interest charge:
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
        }
    }

    // charge a fee when the withdrawal limit is exceeded
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.Now, "apply overdraft fee") : default;
    // return a fee transaction if the account is overdrawn
    // otherwise, return a null transaction
}