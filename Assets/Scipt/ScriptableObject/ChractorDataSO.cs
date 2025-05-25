using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactorData", menuName = "ScriptableObjects/CharactorData", order = 0)]
public class CharactorDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }



    [HorizontalLine]
    [NaughtyAttributes.BoxGroup("Stats")]
    [SerializeField] private float attack;
    [SerializeField] private float AttackSpeed;
    [SerializeField] private float CriticalChance;
    [SerializeField] private float CriticalPercent;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MoveSpeedPercent;
    [SerializeField] private float MaxHealth;
    [SerializeField] private float Range;
    [SerializeField] private float HealthRecoverySpeed;
    [SerializeField] private float Armor;
    [SerializeField] private float Luck;
    [SerializeField] private float Dodge;
    [SerializeField] private float LifeSteel;

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
            {Stats.MoveSpeed, MoveSpeed},
            {Stats.MoveSpeedPercent, MoveSpeedPercent},
            {Stats.MaxHealth, MaxHealth},
            {Stats.Range, Range},
            {Stats.HealthRecoverySpeed, HealthRecoverySpeed},
            {Stats.Armor, Armor},
            {Stats.Luck, Luck},
            {Stats.Dodge, Dodge},
            {Stats.LifeSteel, LifeSteel},
        };
        }
        private set { }
    }


}
