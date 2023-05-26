using CommandDotNet;

namespace greenfield_cli.Commands;

[Command(
    name: "greenfield", 
    Description = $"{Constants.APP_NAME} - cmd tool for supporting making request to greenfield",
    Usage = $"{Constants.APP_NAME} [global options] command [command options] [arguments...]")]
public class RootCommands
{
    [Subcommand]
    public BucketCommand Bucket { get; set; }

    [Subcommand]
    public ObjectCommand Object { get; set; }

    [Command(name: "create-keystore", Usage = "create a new keystore file")]
    public void GenerateKey(string passwordFile, string outputFile)
    {
        string password = File.ReadAllText(passwordFile);
        
    }
}
