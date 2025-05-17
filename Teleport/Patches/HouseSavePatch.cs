using HarmonyLib;
using UnityEngine;

namespace Teleport.Patches;

[HarmonyPatch(typeof(HouseSave), nameof(HouseSave.loadDetails))]
internal static class HouseSavePatch
{
    [HarmonyPostfix]
    private static void Postfix(HouseDetails copyTo)
    {
        if (Plugin.TeleportInsideHouse.Value || !copyTo.isThePlayersHouse)
        {
            return;
        }

        Plugin.HomePosition = new Vector3(copyTo.xPos * 2f, 5f, copyTo.yPos * 2f);
    }
}