﻿namespace Zork;

public class Command : IEquatable<Command>
{
    public string Name { get; set; }

    public string[] Verbs { get; }

    public Action<Game, CommandContext> Action { get; }

    public Command(string name, IEnumerable<string> verbs, Action<Game, CommandContext> action)
    {
        Name = name;
        Verbs = verbs.ToArray();
        Action = action;
    }

    public Command(string name, string verb, Action<Game, CommandContext> action)
        : this(name, new string[] { verb }, action)
    {
    }

    public static bool operator ==(Command lhs, Command rhs)
    {
        if (ReferenceEquals(lhs, rhs))
            return true;

        if (lhs is null || rhs is null)
            return false;

        return lhs.Name == rhs.Name;
    }

    public static bool operator !=(Command lhs, Command rhs) => !(lhs == rhs);

    public override bool Equals(object obj) => obj is Command command && Equals(command);

    public bool Equals(Command other) => other != null && Name == other.Name;

    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() => Name;
}