using CommandDotNet;

[Command(name: "bucket", Description = "Description = \"Perform bucket-related operations\"")]
public class BucketCommand
{
    [Command("create", Description = "Create a bucket")]
    public void Create([Operand(name: "name", Description = "Name of the bucket")] string name)
    {
        // Perform create bucket logic using name and other options
        Console.WriteLine($"Creating bucket: {name}");
    }

    [Command("update", Description = "update a bucket")]
    public void Update([Operand(name: "name", Description = "Name of the bucket")] string name)
    {
        // Perform update bucket logic using name and other options
        Console.WriteLine($"Updating bucket: {name}");
    }

    [Command("delete", Description = "delete a bucket")]
    public void Delete([Operand(name: "name", Description = "Name of the bucket")] string name)
    {
        // Perform delete bucket logic using name and other options
        Console.WriteLine($"Deleting bucket: {name}");
    }

    [DefaultCommand]
    public void OnExecute()
    {
        // Handle the case when no action is specified
        Console.WriteLine("Please specify an action: create, update, delete");
    }
}