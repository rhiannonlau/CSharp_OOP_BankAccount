// tutorial from https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/classes

namespace Classes;

public class BankAccount
{
    public string Number { get; }
    public string Ownder { get; set; }
    public decimal Balance { get; }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {

    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {

    }
}