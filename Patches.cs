using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(MFDRadarUI), nameof(MFDRadarUI.OnInputAxis))]
class MFDRadarUI_OnInputAxis_Patch
{
    [HarmonyPostfix]
    static void Postfix(MFDRadarUI __instance, Vector3 axis)
    {
        if (__instance.boresightMode)
		{
			return;
		}
        Vector3 vector = __instance.cursorTransform.localPosition;
        float currentAzimuthAdjust = __instance.radarCtrlr.currentAzimuthAdjust;
        float num = __instance.AngleToBscopeX(currentAzimuthAdjust);
        float num2 = __instance.fovs[0] / __instance.uiAngle;
        float num3 = VectorUtils.SignedAngle(Vector3.up, vector, Vector3.right) * num2;
        if (__instance.updateAdvAzimuth && __instance.fovIdx > 0 && !__instance.lockingRadar.IsLocked())
        {
            if (__instance.bScope)
            {
                if (vector.x < num)
                {
                    __instance.radarCtrlr.OnAzimuthInput(-1f);
                    __instance.UpdateAzimuthBounds();
                }
                else if (vector.x > num)
                {
                    __instance.radarCtrlr.OnAzimuthInput(1f);
                    __instance.UpdateAzimuthBounds();
                }
            }
            else
            {
                if (num3 < currentAzimuthAdjust)
				{
					__instance.radarCtrlr.OnAzimuthInput(-1f);
					__instance.UpdateAzimuthBounds();
				}
				else if (num3 > currentAzimuthAdjust)
				{
					__instance.radarCtrlr.OnAzimuthInput(1f);
					__instance.UpdateAzimuthBounds();
				}
            }
        }
    }
}