using CommandDotNet;
using greenfield_cli.Commands;

public class Program
{
    public static int Main(string[] args)
    {
        return new AppRunner<RootCommands>()
            .UseDefaultMiddleware()
            .Run(args);
    }
}