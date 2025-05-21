using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

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


    public static string FormatEnumName(this PlayerAttr attr)
    {
        string result = "";
        string name = attr.ToString();
        result += name[0];
        for (int i = 1; i < name.Length; i++)
        {
            char c = name[i];
            if (char.IsUpper(c))
            {
                result += " ";
            }
            result += c;
        }
        return result;
    }
}
