using UnityEngine;

namespace SaveLoadPosition;

internal sealed class PositionRecord
{
    internal static Dictionary<string, Vector3> SavedPositions { get; } = [];
}