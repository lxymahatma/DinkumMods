using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace Sprint;

[HarmonyPatch(typeof(CharMovement), "charMoves")]
internal static class CharMovementPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        var codes = new List<CodeInstruction>(instructions);

        var targetIndex = -1;
        for (var i = 0; i < codes.Count; i++)
        {
            if (codes[i].opcode == OpCodes.Stloc_3 &&
                codes[i - 1].opcode == OpCodes.Call &&
                codes[i - 1].operand.ToString().Contains("op_Multiply"))
            {
                targetIndex = i + 1;
                break;
            }
        }

        if (targetIndex == -1)
        {
            Debug.LogError("Cannot apply SpeedBoost patch: target index not found.");
            return codes;
        }

        var targetLabel = generator.DefineLabel();
        codes[targetIndex].labels.Add(targetLabel);

        var injectCodes = new List<CodeInstruction>
        {
            new(OpCodes.Call, AccessTools.Method(typeof(CharMovementPatch), nameof(ShouldApplySpeedBoost))),
            new(OpCodes.Brfalse, targetLabel),
            new(OpCodes.Ldloc_3),
            new(OpCodes.Ldc_R4, Plugin.SprintBoostMultiplier.Value),
            new(OpCodes.Call, AccessTools.Method(typeof(Vector3), "op_Multiply", [typeof(Vector3), typeof(float)])),
            new(OpCodes.Stloc_3)
        };

        codes.InsertRange(targetIndex, injectCodes);
        return codes;
    }

    private static bool ShouldApplySpeedBoost() =>
        Input.GetKey(Plugin.SprintKey.Value) &&
        NetworkMapSharer.Instance.localChar &&
        !NetworkMapSharer.Instance.localChar.usingHangGlider;
}