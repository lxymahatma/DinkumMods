using HarmonyLib;
using UnityEngine;

namespace Teleport.Patches;

[HarmonyPatch(typeof(EntryExit), "OnEnable")]
internal static class EntryExitPatch
{
    [HarmonyPostfix]
    private static void Postfix(EntryExit __instance)
    {
        if (__instance.myLocation is NPCSchedual.Locations.Mine)
        {
            Plugin.MinePosition = Plugin.TeleportInsideHouse.Value
                ? __instance.linkedTo.transform.position + Vector3.forward * 2f
                : __instance.transform.position + Vector3.back * 2f;
        }
        else if (__instance.isPlayerHouseDoor)
        {
            Plugin.HomePosition = Plugin.TeleportInsideHouse.Value
                ? __instance.linkedTo.transform.position + Vector3.forward * 2f
                : __instance.transform.position + Vector3.back * 2f;
        }
    }
}