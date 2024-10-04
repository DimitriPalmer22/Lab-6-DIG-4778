namespace Zork;

public class CommandManager
{
    private HashSet<Command> _commands;

    public CommandManager() => _commands = new HashSet<Command>();

    public CommandManager(IEnumerable<Command> commands) =>
        _commands = new HashSet<Command>(commands);

    public CommandContext Parse(string commandString)
    {
        var commandQuery =
            from command in _commands
            where command.Verbs.Contains(commandString, StringComparer.OrdinalIgnoreCase)
            select new CommandContext(commandString, command);

        return commandQuery.FirstOrDefault();
    }

    public bool PerformCommand(Game game, string commandString)
    {
        bool result;

        CommandContext commandContext = Parse(commandString);

        if (commandContext.Command != null)
        {
            commandContext.Command.Action(game, commandContext);
            result = true;
        }
        else
            result = false;

        return result;
    }

    public void AddCommand(Command command) => _commands.Add(command);

    public void RemoveCommand(Command command) => _commands.Remove(command);

    public void AddCommands(IEnumerable<Command> commands) => _commands.UnionWith(commands);

    public void ClearCommands() => _commands.Clear();
}