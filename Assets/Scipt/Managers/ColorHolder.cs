using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ColorHolder : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private PaletteSO paletteSO;

    [Header("settings")]
    public static ColorHolder instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static Color GetColorByLevel(int level)
    {
        int i = math.clamp(level, 0, instance.paletteSO.colors.Length);
        return instance.paletteSO.colors[i];
    }
}
