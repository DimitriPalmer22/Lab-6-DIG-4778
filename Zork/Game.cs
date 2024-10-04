using Newtonsoft.Json;

namespace Zork;

public class Game
{
    public World World { get; private set; }

    [JsonIgnore] public Player Player { get; set; }

    [JsonIgnore] private bool IsRunning { get; set; }

    public Game(World world, Player player)
    {
        World = world;
        Player = player;
    }

    public void Run()
    {
        IsRunning = true;

        // Set up the previous room
        Room previousRoom = null;

        var outputMessage = string.Empty;

        while (IsRunning)
        {
            Console.WriteLine(Player.Location);

            if (Player.Location != previousRoom)
            {
                Console.WriteLine(Player.Location.Description);
                previousRoom = Player.Location;
            }

            var command = Program.ToCommand(Program.Input());

            switch (command)
            {
                case Commands.QUIT:
                    IsRunning = false;
                    break;

                case Commands.LOOK:
                    outputMessage = Player.Location.Description;
                    break;

                case Commands.NORTH:
                case Commands.SOUTH:
                case Commands.EAST:
                case Commands.WEST:
                    var direction = Enum.Parse<Directions>(command.ToString());

                    if (Player.Move(direction) == false)
                        outputMessage = "The way is shut!";
                    else
                        outputMessage = $"Moved {direction}.";

                    break;

                case Commands.UNKNOWN:
                    outputMessage = "Unknown command.";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(outputMessage))
                Program.Output(outputMessage);
        }
    }


    public static Game Load(string filename)
    {
        var game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
        game.Player = game.World.SpawnPlayer();

        return game;
    }
}