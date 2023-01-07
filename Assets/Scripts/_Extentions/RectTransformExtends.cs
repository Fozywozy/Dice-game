using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtends
{
    public static Vector2Int PixelPosition(this RectTransform C_Transform)
    {
        return new Vector2Int((int)C_Transform.position.x, (int)C_Transform.position.y);
    }

    public static Vector2Int PixelScale(this RectTransform C_Transform)
    {
        return Vector2Int.RoundToInt(new Vector2(C_Transform.sizeDelta.x * C_Transform.lossyScale.x, C_Transform.sizeDelta.y * C_Transform.lossyScale.y));
    }
}
