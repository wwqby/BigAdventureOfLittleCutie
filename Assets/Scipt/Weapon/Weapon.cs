using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    protected enum State
    {
        GamePause,
        Idel,
        Attack
    }
    [Header("State")]
    [SerializeField] protected State state;

    [Header("Components")]
    [SerializeField] protected Animator animator;

    [Header("Attack")]
    [SerializeField] protected Transform hitDetection;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRatePerSecond;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float attackTimer;
    [Header("Settings")]
    [SerializeField] protected float aimRadius;
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitDetection.position, aimRadius, targetMask);
        if (enemies.Length == 0)
        {
            return null;
        }
        float minDistance = aimRadius;
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


    protected int GetDamage(out bool isCritical)
    {
        if (UnityEngine.Random.Range(1, 101) <= 50)
        {
            isCritical = true;
            return damage * 2;
        }
        isCritical = false;
        return damage;
    }

    #endregion


    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aimRadius);

    }

}
