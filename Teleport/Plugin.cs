using BepInEx;
using BepInEx.Configuration;
using TinyResort;
using UnityEngine;

namespace Teleport;

[BepInPlugin(PluginBuildInfo.PLUGIN_GUID, PluginBuildInfo.PLUGIN_NAME, PluginBuildInfo.PLUGIN_VERSION)]
[BepInDependency("dev.TinyResort.TRTools")]
public sealed partial class Plugin : BaseUnityPlugin
{
    private static TRPlugin _plugin;
    internal static Vector3 HomePosition { get; set; }
    internal static Vector3 MinePosition { get; set; }
    internal static Dictionary<string, Transform> FriendsDictionary { get; } = [];
    internal static ConfigEntry<bool> TeleportInsideHouse { get; private set; }

    private void Awake()
    {
        _plugin = this.Initialize(-1, "tp");

        _plugin.AddCommands("friend", ["f"], "Teleport to a friend", TeleportToFriend, "Friend Name");
        _plugin.AddCommands("position", ["p"], "Teleport to coordinates", TeleportToPosition, "X Y [Z]");
        _plugin.AddCommands("home", ["h"], "Teleport to home (Only works in Main Land)", TeleportToHome);
        _plugin.AddCommands("mine", ["m"], "Teleport to mine (Only works in Underground)", TeleportToMine);

        TeleportInsideHouse = Config.Bind("Teleport", "TeleportInsideHouse", true, "Teleport inside house if possible");

        _plugin.harmony.PatchAll();
    }

    private static string TeleportToFriend(string[] args)
    {
        if (args.Length != 1)
        {
            return "Please provide one friend's name.";
        }

        var friendName = args[0];
        if (!FriendsDictionary.TryGetValue(friendName, out var friendTransform))
        {
            return "Friend not found.";
        }

        UpdateCharacterPosition(friendTransform.position);
        return $"Teleported to {friendName}.";
    }

    private static string TeleportToPosition(string[] args)
    {
        switch (args.Length)
        {
            case < 2:
                return "Please provide at least horizontal and vertical coordinates.";
            case > 3:
                return "Please provide only horizontal, vertical coordinates and optionally height.";
        }

        if (!float.TryParse(args[0], out var x) || !float.TryParse(args[1], out var y))
        {
            return "Invalid coordinates.";
        }

        var z = 5f;
        if (args.Length == 3)
        {
            if (!float.TryParse(args[2], out z))
            {
                return "Invalid coordinates for height.";
            }
        }

        UpdateCharacterPosition(new Vector3(x, z, y));
        return $"Teleported to coordinates: {args[0]}, {args[1]}{(args.Length == 3 ? $", {args[2]}" : string.Empty)}.";
    }

    private static string TeleportToHome(string[] args)
    {
        UpdateCharacterPosition(HomePosition);
        return "Teleported to home.";
    }

    private static string TeleportToMine(string[] args)
    {
        UpdateCharacterPosition(MinePosition);
        return "Teleported to mine.";
    }
}