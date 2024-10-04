namespace Zork;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zork!");

        // Initialize the command
        var command = Commands.UNKNOWN;

        // Game loop
        while (command != Commands.QUIT)
        {
            // Convert the input to a command
            command = ToCommand(Input());

            // Handle the command
            switch (command)
            {
                case Commands.LOOK:
                    Output("A rubber mat saying 'Welcome to Zork!' lies by the door.");
                    break;

                case Commands.NORTH:
                    Output("You moved North.");
                    break;

                case Commands.SOUTH:
                    Output("You moved South.");
                    break;

                case Commands.EAST:
                    Output("You moved East.");
                    break;

                case Commands.WEST:
                    Output("You moved West.");
                    break;

                case Commands.UNKNOWN:
                    Output("Unknown command.");
                    break;
            }
        }

        // Exit message
        Console.WriteLine("Thank you for playing!");
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

    private static void Output(string output)
    {
        Console.WriteLine($"{output}\n");
    }

    /// <summary>
    /// Convert the input to a command.
    /// </summary>
    /// <param name="input">A lowercase string</param>
    /// <returns></returns>
    private static Commands ToCommand(string input)
    {
        if (Enum.TryParse(input, true, out Commands cmd))
            return cmd;

        return Commands.UNKNOWN;
    }

    public enum Commands
    {
        QUIT,
        LOOK,

        NORTH,
        SOUTH,
        EAST,
        WEST,

        UNKNOWN
    }
}