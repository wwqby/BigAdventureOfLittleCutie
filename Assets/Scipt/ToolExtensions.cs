using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolExtensions
{
    /*
    * Check if layer is in layer mask
    */
    public static bool IsInLayerMask(this int layer, LayerMask mask)
    {
        return ((1 << layer) & mask.value) != 0;
    }

    /*
    * Clear all children of transform
    */
    public static void Clear(this Transform transform)
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Object.Destroy(child.gameObject);
        }
    }
}
