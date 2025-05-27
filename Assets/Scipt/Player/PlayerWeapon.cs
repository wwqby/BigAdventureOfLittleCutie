using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private WeaponPosition[] weaponPositions;
    public void AddWeapon(WeaponDataSO weapon, int level)
    {
        Debug.Log($"Add weapon {weapon.WeaponName},level {level}");
        foreach (WeaponPosition weaponPosition in weaponPositions)
        {
            if (weaponPosition.Weapon == null)
            {
                weaponPosition.AddWeapon(weapon, level);
                break;
            }
        }
    }
}
