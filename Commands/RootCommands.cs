using CommandDotNet;

namespace greenfield_cli.Commands;

[Command(name: "greenfield", Description = "Greenfield CLI")]
public class RootCommands
{
    [Subcommand]
    public BucketCommand Bucket { get; set; }

    [Subcommand]
    public ObjectCommand Object { get; set; } 
}
