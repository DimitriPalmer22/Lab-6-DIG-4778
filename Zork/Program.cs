namespace Zork;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zork!");

        // Get the command from the player
        string input = Input();

        if (input == "quit")
            Console.WriteLine("Thank you for playing!");
        else if (input == "look")
            Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
        else
            Console.WriteLine("Unrecognized command.");
    }

    /// <summary>
    /// Print out a '>' and get the input from the user.
    /// Get the input from the user.
    /// Trim the input and make it lowercase.
    /// </summary>
    /// <returns>The trimmed lowercase input from the player</returns>
    private static string Input()
    {
        Console.Write("> ");
        var input = Console.ReadLine().Trim().ToLower();

        return input;
    }
}