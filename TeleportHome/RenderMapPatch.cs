using HarmonyLib;
using UnityEngine;

namespace TeleportHome;

[HarmonyPatch(typeof(RenderMap), nameof(RenderMap.CheckIfNeedsIcon))]
internal static class RenderMapPatch
{
    [HarmonyPostfix]
    private static void Postfix(int xPos, int yPos)
    {
        var num = WorldManager.Instance.onTileMap[xPos, yPos];
        if (num <= -1)
        {
            return;
        }

        var tileObject = WorldManager.Instance.allObjects[num];
        if (!tileObject.displayPlayerHouseTiles || !tileObject.displayPlayerHouseTiles.isPlayerHouse)
        {
            return;
        }

        var position = new Vector2(xPos * 2f, yPos * 2f);
        switch (RealWorldTimeLight.time.CurrentWorldArea)
        {
            case WorldArea.MAIN_ISLAND:
                Plugin.HomePosition = position;
                break;
            case WorldArea.UNDERGROUND:
                Plugin.MinePosition = position;
                break;
        }
    }
}