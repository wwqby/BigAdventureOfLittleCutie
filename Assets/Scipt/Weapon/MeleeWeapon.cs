using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{


    [Header("Components")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Attack")]
    [SerializeField] private List<Enemy> damagedEnemies;



    protected override void Start()
    {
        damagedEnemies = new List<Enemy>();
        base.Start();
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



    #region Attack
    private void AutoAim()
    {
        Vector3 targetVector = Vector3.up;
        Enemy enemy = GetClosetEnemy();
        if (enemy != null)
        {
            targetVector = (enemy.CenterPoint - (Vector2)hitDetection.position).normalized;
            transform.up = targetVector;
            ManageAttack();
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
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(hitDetection.position, boxCollider.bounds.size, hitDetection.localEulerAngles.z, targetMask);
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
        state = State.Idel;
        damagedEnemies.Clear();
    }

    #endregion

}
