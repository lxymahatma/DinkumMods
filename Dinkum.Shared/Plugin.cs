using UnityEngine;

#if SaveLoadPosition
namespace SaveLoadPosition;
#elif Teleport
namespace Teleport;
#elif TeleportHome
namespace TeleportHome;
#endif

public sealed partial class Plugin
{
    private static void UpdateCharacterPosition(Vector3 targetPosition)
    {
        NetworkMapSharer.Instance.localChar.transform.position = targetPosition;
        CameraController.control.transform.position = targetPosition;
        NewChunkLoader.loader.forceInstantUpdateAtPos();
    }
}