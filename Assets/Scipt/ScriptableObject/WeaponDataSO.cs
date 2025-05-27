using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO",
menuName = "ScriptableObjects/WeaponDataSO", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }
    [field: SerializeField] public BaseWeapon Prefab { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float CriticalChance;
    [SerializeField] private float CriticalPercent;
    [SerializeField] private float Range;


    public Dictionary<Stats, float> BaseStats
    {
        get
        {
            return new Dictionary<Stats, float>()
        {
            {Stats.Attack, attack},
            {Stats.AttackSpeed, AttackSpeed},
            {Stats.CriticalChance, CriticalChance},
            {Stats.CriticalPercent, CriticalPercent},
            {Stats.Range, Range},
        };
        }
        private set { }
    }

    public float GetValue(Stats stats)
    {
        foreach (KeyValuePair<Stats, float> item in BaseStats)
        {
            if (item.Key == stats)
            {
                return item.Value;
            }
        }
        Debug.LogError("No stats found");
        return 0f;
    }
}
