using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IPlayerStatsListener
{

    [Header("State")]
    [SerializeField] protected State state;

    [Header("Components")]
    [field: SerializeField] protected WeaponDataSO WeaponDataSO { get; private set; }
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform hitDetection;

    [Header("Attack")]
    [SerializeField] protected int level;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRatePerSecond;
    [SerializeField] protected float cirticalChance;
    [SerializeField] protected float cirticalPercent;
    [SerializeField] protected float range;
    [Header("Timer")]
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackTimer;
    [Header("Settings")]
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected float aniLerp;



    protected virtual void Start()
    {
        state = State.Idel;
    }


    #region Attack


    protected void IncreaseTimer()
    {
        attackTimer += Time.deltaTime;
    }

    protected Enemy GetClosetEnemy()
    {
        Enemy closetEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, targetMask);
        if (enemies.Length == 0)
        {
            return null;
        }
        float minDistance = range;
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            float distance = Vector3.Distance(enemy.gameObject.transform.position, transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                closetEnemy = enemy;
            }
        }
        return closetEnemy;
    }


    protected float GetDamage(out bool isCritical)
    {
        if (UnityEngine.Random.Range(1, 101) <= cirticalChance)
        {
            isCritical = true;
            return damage * cirticalPercent / 100f;
        }
        isCritical = false;
        return damage;
    }

    protected void ConfigureStats()
    {
        float multiplier = 1 + level / 3f;
        damage = WeaponDataSO.GetValue(Stats.Attack) * multiplier;
        attackRatePerSecond = WeaponDataSO.GetValue(Stats.AttackSpeed);
        attackDelay = 1f / attackRatePerSecond;
        cirticalChance = WeaponDataSO.GetValue(Stats.CriticalChance);
        cirticalPercent = WeaponDataSO.GetValue(Stats.CriticalPercent);
        range = WeaponDataSO.GetValue(Stats.Range);
    }

    #endregion

    public void OnPlayerStatsChanged(PlayerStatsManager playerStatsManager)
    {
        ConfigureStats();
        damage *= 1f + playerStatsManager.GetValue(Stats.Attack) / 100f;
        attackRatePerSecond *= 1f + playerStatsManager.GetValue(Stats.AttackSpeed) / 100f;
        attackDelay = 1f / attackRatePerSecond;
        cirticalChance *= 1f + playerStatsManager.GetValue(Stats.CriticalChance) / 100f;
        cirticalPercent += playerStatsManager.GetValue(Stats.CriticalPercent);
        range *= 1f + playerStatsManager.GetValue(Stats.Range) / 100f;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    public void ConfigureWeapon(int level)
    {
        this.level = level;
        OnPlayerStatsChanged(PlayerStatsManager.instance);
    }



    protected enum State
    {
        GamePause,
        Idel,
        Attack
    }

}


