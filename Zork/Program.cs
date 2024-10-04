namespace Zork;

public class Program
{
    // Initialize the rooms array
    private static readonly string[,] Rooms =
    {
        { "Rocky Trail", "South of House", "Canyon View" },
        { "Forest", "West of House", "Behind House" },
        { "Dense Woods", "North of House", "Clearing" }
    };

    private static (int X, int Y) _location = (1, 1);

    private static string CurrentRoom => Rooms[_location.X, _location.Y];

    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zork!");


        // Initialize the command
        var command = Commands.UNKNOWN;

        // Game loop
        while (command != Commands.QUIT)
        {
            // Display the current room
            Console.WriteLine($"--{Rooms[_location.Y, _location.X]}--");

            // Convert the input to a command
            command = ToCommand(Input());

            var outputMessage = string.Empty;

            // Handle the command
            switch (command)
            {
                case Commands.QUIT:
                    outputMessage = "Thank you for playing!";
                    break;

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
                default:
                    outputMessage = "Unknown command.";
                    break;
            }

            // Output the output message if it is not empty
            if (!string.IsNullOrWhiteSpace(outputMessage))
                Output(outputMessage);
        }
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

    private static bool IsDirection(Commands command)
    {
        return command == Commands.NORTH || command == Commands.SOUTH || command == Commands.EAST ||
               command == Commands.WEST;
    }

    private static bool Move(Commands input)
    {
        // If the input is not a direction, throw an error
        if (!IsDirection(input))
            throw new ArgumentException("Invalid direction", nameof(input));

        switch (input)
        {
            case Commands.NORTH:
                if (_location.Y < Rooms.GetLength(1) - 1)
                    _location.Y++;
                else
                    return false;

                break;

            case Commands.SOUTH:
                if (_location.Y > 0)
                    _location.Y--;
                else
                    return false;

                break;

            case Commands.EAST:
                if (_location.X < Rooms.GetLength(0) - 1)
                    _location.X++;
                else
                    return false;

                break;

            case Commands.WEST:
                if (_location.X > 0)
                    _location.X--;
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