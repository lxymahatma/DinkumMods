using HarmonyLib;
using UnityEngine;

namespace Teleport.Patches;

[HarmonyPatch(typeof(RenderMap), nameof(RenderMap.trackOtherPlayers))]
internal static class RenderMapPatch
{
    [HarmonyPostfix]
    private static void Postfix(Transform trackMe)
    {
        var playerName = trackMe.GetComponent<EquipItemToChar>().playerName;

        if (!Plugin.FriendsDictionary.ContainsKey(playerName))
        {
            Plugin.FriendsDictionary.Add(playerName, trackMe);
        }
    }
}