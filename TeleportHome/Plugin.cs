using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace TeleportHome;

[BepInPlugin(PluginBuildInfo.PLUGIN_GUID, PluginBuildInfo.PLUGIN_NAME, PluginBuildInfo.PLUGIN_VERSION)]
public sealed partial class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new(PluginBuildInfo.PLUGIN_NAME);
    internal static Vector2 HomePosition { get; set; }
    internal static Vector2 MinePosition { get; set; }
    private static ConfigEntry<KeyCode> TeleportHomeKey { get; set; }
    private static ConfigEntry<KeyCode> TeleportMineKey { get; set; }

    private void Awake()
    {
        TeleportHomeKey = Config.Bind("Settings", "TeleportKey", KeyCode.H,
            "The key to press to teleport home. Default is H.");
        TeleportMineKey = Config.Bind("Settings", "TeleportMineKey", KeyCode.F3,
            "The key to press to teleport to the mine. Default is F3.");

        Harmony.PatchAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(TeleportHomeKey.Value))
        {
            TeleportToHome();
        }
        else if (Input.GetKeyDown(TeleportMineKey.Value))
        {
            TeleportToMine();
        }
    }

    private static void TeleportToHome() =>
        UpdateCharacterPosition(new Vector3(HomePosition.x, 2f, HomePosition.y));

    private static void TeleportToMine() =>
        UpdateCharacterPosition(new Vector3(MinePosition.x, 2f, MinePosition.y));
}