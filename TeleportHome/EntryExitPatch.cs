using HarmonyLib;
using UnityEngine;

namespace TeleportHome;

[HarmonyPatch(typeof(EntryExit), "OnEnable")]
internal static class EntryExitPatch
{
    [HarmonyPostfix]
    private static void Postfix(EntryExit __instance)
    {
        if (!__instance.isPlayerHouseDoor)
        {
            return;
        }

        Plugin.HomePosition = __instance.transform.position + Vector3.back * 2f;
    }
}