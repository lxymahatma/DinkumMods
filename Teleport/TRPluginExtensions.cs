using TinyResort;

namespace Teleport;

public static class TRPluginExtensions
{
    public static void AddCommands(this TRPlugin plugin,
        string command,
        string[] alias,
        string description,
        Func<string[], string> method,
        params string[] argumentNames)
    {
        plugin.AddCommand(command, description, method, argumentNames);
        foreach (var aliasCommand in alias)
        {
            plugin.AddCommand(aliasCommand, description, method, argumentNames);
        }
    }
}