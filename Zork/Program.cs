namespace Zork;

public class Program
{
    // Initialize the rooms array
    private static readonly Room[,] Rooms =
    {
        { new("Rocky Trail"), new("South of House"), new("Canyon View") },
        { new("Forest"), new("West of House"), new("Behind House") },
        { new("Dense Woods"), new("North of House"), new("Clearing") }
    };

    private static (int X, int Y) _location = (1, 1);

    private static Room CurrentRoom => Rooms[_location.Y, _location.X];

    private static void InitializeDescriptions()
    {
        Rooms[0, 0].Description = "You are on a rock-strewn trail.";
        Rooms[0, 1].Description =
            "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
        Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";

        Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";
        Rooms[1, 1].Description =
            "You are facing the west side of a white house. There is no door here, and all the windows are barred.";
        Rooms[1, 2].Description =
            "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";

        Rooms[2, 0].Description =
            "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
        Rooms[2, 1].Description =
            "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
        Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Zork!");

        // Initialize the room descriptions
        InitializeDescriptions();

        // Initialize the command
        var command = Commands.UNKNOWN;

        // Game loop
        while (command != Commands.QUIT)
        {
            // Display the current room
            Console.WriteLine($"--{CurrentRoom}--");
            
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
                    outputMessage = CurrentRoom.Description;
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