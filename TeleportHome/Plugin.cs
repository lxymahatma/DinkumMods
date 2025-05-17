using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace TeleportHome;

[BepInPlugin(PluginBuildInfo.PLUGIN_GUID, PluginBuildInfo.PLUGIN_NAME, PluginBuildInfo.PLUGIN_VERSION)]
public sealed partial class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new(PluginBuildInfo.PLUGIN_NAME);
    internal static Vector3 HomePosition { get; set; }
    private static ConfigEntry<KeyCode> TeleportHomeKey { get; set; }

    private void Awake()
    {
        TeleportHomeKey = Config.Bind("Settings", "TeleportKey", KeyCode.H,
            "The key to press to teleport home. Default is H.");

        Harmony.PatchAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(TeleportHomeKey.Value))
        {
            TeleportToHome();
        }
    }

    private static void TeleportToHome() =>
        UpdateCharacterPosition(HomePosition);
}