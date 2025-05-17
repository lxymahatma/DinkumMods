using BepInEx;
using TinyResort;

namespace SaveLoadPosition;

[BepInPlugin(PluginBuildInfo.PLUGIN_GUID, PluginBuildInfo.PLUGIN_NAME, PluginBuildInfo.PLUGIN_VERSION)]
[BepInDependency("dev.TinyResort.TRTools")]
public sealed partial class Plugin : BaseUnityPlugin
{
    private static TRPlugin _plugin;

    private void Awake()
    {
        _plugin = this.Initialize(-1, "sl");

        _plugin.AddCommand("save", "Save current position", SavePosition, "Slot Name");
        _plugin.AddCommand("s", "Save current position", SavePosition, "Slot Name");

        _plugin.AddCommand("load", "Load saved position", LoadPosition, "Slot Name");
        _plugin.AddCommand("l", "Load saved position", LoadPosition, "Slot Name");

        _plugin.AddCommand("list", "List saved positions", ListSavedPositions);

        _plugin.harmony.PatchAll();
    }

    private void OnDestroy()
    {
    }

    private static string SavePosition(string[] args)
    {
        switch (args.Length)
        {
            case 0:
            {
                return "Please provide a slot name.";
            }
            case > 1:
                return "Only one argument expected.";
        }

        var slotName = args[0];
        var characterPosition = NetworkMapSharer.Instance.localChar.transform.position;
        PositionRecord.SavedPositions[slotName] = characterPosition;
        return $"Saved position in slot {slotName}: {characterPosition}";
    }

    private static string LoadPosition(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                return "Please provide a slot name.";
            case > 1:
                return "Only one argument expected.";
        }

        var slotName = args[0];
        var savedPosition = PositionRecord.SavedPositions[slotName];
        UpdateCharacterPosition(savedPosition);
        return $"Loaded position for slot {slotName}: {savedPosition}";
    }

    private static string ListSavedPositions(string[] args)
    {
        if (args.Length > 0)
        {
            return "No arguments expected.";
        }

        return string.Join(Environment.NewLine, PositionRecord.SavedPositions.Select(kvp => $"Slot {kvp.Key}: {kvp.Value}"));
    }
}