using CommandDotNet;
using greenfield_cli.Commands;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;

public class Program
{
    public static int Main(string[] args)
    {
        return new AppRunner<RootCommands>()
            .UseDefaultMiddleware()
            .Run(args);
    }
}