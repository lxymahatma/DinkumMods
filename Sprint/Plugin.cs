using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Sprint;

[BepInPlugin(PluginBuildInfo.PLUGIN_GUID, PluginBuildInfo.PLUGIN_NAME, PluginBuildInfo.PLUGIN_VERSION)]
public sealed class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new(PluginBuildInfo.PLUGIN_NAME);
    internal static ConfigEntry<float> SprintBoostMultiplier { get; private set; }
    internal static ConfigEntry<KeyCode> SprintKey { get; private set; }

    private void Awake()
    {
        SprintBoostMultiplier = Config.Bind("Settings", "SprintBoostMultiplier", 2.0f,
            "The multiplier for the sprint speed boost. Default is 2.0f.");
        SprintKey = Config.Bind("Settings", "SprintKey", KeyCode.LeftControl,
            "The key to hold for sprinting. Default is Left Control.");

        Harmony.PatchAll();
    }
}