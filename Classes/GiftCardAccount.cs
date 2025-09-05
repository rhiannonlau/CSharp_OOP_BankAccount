using Classes;

// pre-paid gift card accounts start with a single deposit, and can only be paid off
// can be refilled once at the start of each month
public class GiftCardAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;
    // provides a default value for the monthlyDeposit value so callers can omit a 0 for no monthly deposit

    public override void PerformMonthEndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
        }
    }
}