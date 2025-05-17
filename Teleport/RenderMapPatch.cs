using HarmonyLib;
using UnityEngine;

namespace Teleport;

[HarmonyPatch(typeof(RenderMap))]
internal static class RenderMapPatch
{
    [HarmonyPatch(nameof(RenderMap.trackOtherPlayers))]
    [HarmonyPostfix]
    private static void trackOtherPlayersPatch(Transform trackMe)
    {
        var playerName = trackMe.GetComponent<EquipItemToChar>().playerName;

        if (!Plugin.FriendsDictionary.ContainsKey(playerName))
        {
            Plugin.FriendsDictionary.Add(playerName, trackMe);
        }
    }

    /*[HarmonyPatch(nameof(RenderMap.ConnectMainChar))]
    [HarmonyPostfix]
    private static void Postfix(RenderMap __instance)
    {
        Console.WriteLine(__instance.mapIcons.Count);
        foreach (var mapIcon in __instance.mapIcons)
        {
            if (mapIcon.CurrentIconType is not mapIcon.iconType.TileObject)
            {
                continue;
            }

            Console.WriteLine(mapIcon.TileObjectId);
            Console.WriteLine(mapIcon.IconName);
            var tileObject = WorldManager.Instance.allObjects[mapIcon.TileObjectId];
            if (!tileObject.displayPlayerHouseTiles || !tileObject.displayPlayerHouseTiles.isPlayerHouse)
            {
                return;
            }

            Console.WriteLine($"House found at {mapIcon.PointingAtPosition}");
            Plugin.HomePosition = mapIcon.PointingAtPosition;
        }
    }*/

    /*[HarmonyPatch(nameof(RenderMap.ChangeWorldArea))]
    [HarmonyPostfix]
    private static void ChangeWorldAreaPatch(WorldArea area, RenderMap __instance)
    {
        Console.WriteLine("Triggered");
        foreach (var mapIcon in __instance.mapIcons)
        {
            if (mapIcon.CurrentIconType is not mapIcon.iconType.TileObject)
            {
                continue;
            }

            Console.WriteLine(mapIcon.TileObjectId);
            var tileObject = WorldManager.Instance.allObjects[mapIcon.TileObjectId];
            if (!tileObject.displayPlayerHouseTiles || !tileObject.displayPlayerHouseTiles.isPlayerHouse)
            {
                return;
            }

            Console.WriteLine($"House found at {mapIcon.PointingAtPosition}");
            Console.WriteLine(mapIcon.localPointingAtPosition);
            Console.WriteLine(mapIcon.NetworkPointingAtPosition);

            if (area == WorldArea.MAIN_ISLAND)
            {
                Plugin.HomePosition = mapIcon.PointingAtPosition;
            }
        }
    }*/
}