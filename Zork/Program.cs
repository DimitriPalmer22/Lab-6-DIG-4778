using Newtonsoft.Json;

namespace Zork;

public class Program
{
    private static void Main(string[] args)
    {
        const string defaultGameFilename = "Content/Rooms.json";

        var gameFilename = args.Length > 0 ? args[0] : defaultGameFilename;

        var game = Game.Load(gameFilename);

        Console.WriteLine("Welcome to Zork!");
        game.Run();
        Console.WriteLine("Thank you for playing!");
    }

    /// <summary>
    /// Print out a '>' and get the input from the user.
    /// Get the input from the user.
    /// Trim the input and make it lowercase.
    /// </summary>
    /// <returns>The trimmed lowercase input from the player</returns>
    public static string Input()
    {
        Console.Write("> ");
        var input = Console.ReadLine().Trim().ToLower();

        return input;
    }

    public static void Output(string output)
    {
        Console.WriteLine($"{output}\n");
    }

}