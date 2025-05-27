using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaletteSO",
menuName = "ScriptableObjects/PaletteSO", order = 0)]
public class PaletteSO : ScriptableObject
{
    public Color[] colors;
    public Color[] outlineColors;
}
