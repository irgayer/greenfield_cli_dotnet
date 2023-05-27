using CommandDotNet;

namespace greenfield_cli.Commands;

[Command(name: "bank", Description = "Perform bank-related operations")]
public class BankCommand
{
    [Command(name:"balance", Description = "query a account's balance")]
    public void Balance()
    {
        
    }

    [Command(name: "transfer", Description = "transfer from your account to a dest account")]
    public void Transfer([Option(shortName:'a', longName:"amount")] double amount
        , [Option(shortName:'t', longName:"toAddress")] string address)
    {
        // Perform transfer logic using name and other options
        
    }
}
