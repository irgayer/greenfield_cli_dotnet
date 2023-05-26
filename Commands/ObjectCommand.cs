using CommandDotNet;

namespace greenfield_cli.Commands;

[Command(name: "object", Description = "Perform object-related operations")]
public class ObjectCommand
{
    [Command(name:"create", Description = "Create an object")]
    public void Create([Operand(name: "name", Description = "Name of the object")] string name)
    {
        // Perform create object logic using name and other options
        Console.WriteLine($"Creating object: {name}");
    }

    [Command(name: "update", Description = "Update an object")]
    public void Update([Operand(name: "name", Description = "Name of the object")] string name)
    {
        // Perform update object logic using name and other options
        Console.WriteLine($"Updating object: {name}");
    }

    [Command(name: "delete", Description = "Delete an object")]
    public void Delete([Operand(name: "name", Description = "Name of the object")] string name)
    {
        // Perform delete object logic using name and other options
        Console.WriteLine($"Deleting object: {name}");
    }

    [DefaultCommand]
    public void OnExecute()
    {
        // Handle the case when no action is specified
        Console.WriteLine("Please specify an action: create, update, delete");
    }
}