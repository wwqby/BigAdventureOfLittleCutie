using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{

    [Header("Elements")]
    [field: SerializeField] public BaseWeapon Weapon { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    public void AddWeapon(WeaponDataSO weapon, int level)
    {
        Level = level;
        Weapon = Instantiate(weapon.Prefab, transform);
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.identity;
        Weapon.ConfigureWeapon(level);
    }


}
