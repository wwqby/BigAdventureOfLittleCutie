using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum State
    {
        GamePause,
        Idel,
        Attack
    }
    [Header("State")]
    [SerializeField] private State state;

    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Attack")]
    [SerializeField] private Transform hitDetection;
    [SerializeField] private float hitDetectionRadius;
    [SerializeField] private int damage;
    //攻击速度
    [SerializeField] private float attackRatePerSecond;
    //攻击间隔
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackTimer;
    [SerializeField] private List<Enemy> damagedEnemies;
    [Header("Settings")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float aniLerp;


    void Start()
    {
        state = State.Idel;
        damagedEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idel:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }
        //只要不是暂停状态，就始终增加攻击计时器
        if (state != State.GamePause)
        {
            IncreaseTimer();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetection.position, hitDetectionRadius);
    }


    #region Attack
    private void AutoAim()
    {
        Debug.Log("Auto Aim");
        Vector3 targetVector = Vector3.up;
        Enemy enemy = GetClosetEnemy();
        if (enemy != null)
        {
            targetVector = (enemy.transform.position - transform.position).normalized;
            ManageAttack();
            Debug.Log("Auto Aiming");
        }
        transform.up = Vector3.Lerp(transform.up, targetVector, aniLerp);
    }
    private void ManageAttack()
    {
        if (attackTimer >= attackDelay)
        {
            attackTimer = 0;
            StartAttack();
        }
    }
    private void StartAttack()
    {
        state = State.Attack;
        attackDelay = 1f / attackRatePerSecond;
        animator.speed = attackRatePerSecond;
        animator.Play("Attack");
        damagedEnemies.Clear();
    }

    private void Attacking()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitDetection.position, hitDetectionRadius, targetMask);
        foreach (Collider2D hitEnem in hitEnemies)
        {
            if (damagedEnemies.Contains(hitEnem.GetComponent<Enemy>()))
            {
                continue;
            }
            hitEnem.GetComponent<Enemy>().TakeDamage(damage);
            damagedEnemies.Add(hitEnem.GetComponent<Enemy>());
        }
    }

    private void StopAttack()
    {
        Debug.Log("Stop Attack");
        state = State.Idel;
        damagedEnemies.Clear();
    }



    private void IncreaseTimer()
    {
        attackTimer += Time.deltaTime;
    }

    private Enemy GetClosetEnemy()
    {
        Enemy closetEnemy = null;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        if (enemies.Length == 0)
        {
            return null;
        }
        float minDistance = radius;
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= minDistance)
            {
                minDistance = distance;
                closetEnemy = enemy;
            }
        }
        return closetEnemy;
    }

    #endregion

}
