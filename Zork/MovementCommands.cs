namespace Zork;

public static class MovementCommands
{
    public static void North(Game game, CommandContext commandContext) => Move(game, Directions.NORTH);
    public static void South(Game game, CommandContext commandContext) => Move(game, Directions.SOUTH);
    public static void East(Game game, CommandContext commandContext) => Move(game, Directions.EAST);
    public static void West(Game game, CommandContext commandContext) => Move(game, Directions.WEST);


    private static void Move(Game game, Directions directions)
    {
        var playerMoved = game.Player.Move(directions);

        if (playerMoved)
            Program.Output($"You moved {directions}.");

        else
            Program.Output("The way is shut!");
    }
}