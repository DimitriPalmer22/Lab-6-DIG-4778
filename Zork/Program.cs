namespace Zork;

public class Program
{
    // Initialize the rooms array
    private static readonly string[] Rooms =
    {
        "Forest",
        "West of House",
        "Behind House",
        "Clearing",
        "Canyon View"
    };

    private static int _currentRoom = 1;

    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zork!");


        // Initialize the command
        var command = Commands.UNKNOWN;

        // Game loop
        while (command != Commands.QUIT)
        {
            // Display the current room
            Console.WriteLine($"--{Rooms[_currentRoom]}--");

            // Convert the input to a command
            command = ToCommand(Input());

            var outputMessage = string.Empty;

            // Handle the command
            switch (command)
            {
                case Commands.LOOK:
                    outputMessage = "A rubber mat saying 'Welcome to Zork!' lies by the door.";
                    break;

                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.EAST:
                case Commands.WEST:
                    // If the move was unsuccessful, print that the way is shut
                    outputMessage = Move(command) ? $"You moved {command}." : "The way is shut!";
                    break;

                case Commands.UNKNOWN:
                    outputMessage = "Unknown command.";
                    break;
            }

            // Output the output message if it is not empty
            if (!string.IsNullOrWhiteSpace(outputMessage))
                Output(outputMessage);
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

    private static bool Move(Commands input)
    {
        // If the input is not a direction, throw an error
        if (input != Commands.NORTH && input != Commands.SOUTH && input != Commands.EAST && input != Commands.WEST)
            throw new ArgumentException("Invalid direction", nameof(input));

        switch (input)
        {
            case Commands.NORTH:
            case Commands.SOUTH:
                return false;

            case Commands.EAST:
                if (_currentRoom < Rooms.Length - 1)
                    _currentRoom++;
                else
                    return false;

                break;

            case Commands.WEST:
                if (_currentRoom > 0)
                    _currentRoom--;
                else
                    return false;

                break;
        }

        return true;
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