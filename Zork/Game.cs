using Newtonsoft.Json;

namespace Zork;

public class Game
{
    public World World { get; set; }

    [JsonIgnore] public Player Player { get; set; }

    [JsonIgnore] private bool IsRunning { get; set; }

    [JsonIgnore] public CommandManager CommandManager { get; set; }


    public Game()
    {
        var commands = new[]
        {
            new Command("look", ["look", "l"], (game, ctx) => { Program.Output(game.Player.Location.Description); }),
            new Command("quit", ["quit", "q"], (game, ctx) => { game.IsRunning = false; }),
            new Command("north", ["north", "n"], (game, ctx) => { game.Player.Move(Directions.NORTH); }),
            new Command("south", ["south", "s"], (game, ctx) => { game.Player.Move(Directions.SOUTH); }),
            new Command("east", ["east", "e"], (game, ctx) => { game.Player.Move(Directions.EAST); }),
            new Command("west", ["west", "w"], (game, ctx) => { game.Player.Move(Directions.WEST); }),
        };

        CommandManager = new CommandManager(commands);
    }

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
                CommandManager.PerformCommand(this, "look");
                previousRoom = Player.Location;
            }

            if (CommandManager.PerformCommand(this, Program.Input()))
                Player.Moves++;
            else
                Program.Output("That's not a valid command.");
        }
    }


    public static Game Load(string filename)
    {
        var game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));
        game.Player = game.World.SpawnPlayer();

        return game;
    }
}