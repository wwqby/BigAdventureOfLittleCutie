using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RengedEnemyAttack))]
public class RengedEnemy : BaseEnemy
{
    [Header("components")]
    [SerializeField] private RengedEnemyAttack attack;


    protected override void Awake()
    {
        base.Awake();
        attack = GetComponent<RengedEnemyAttack>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!CanAttack())
        {
            return;
        }
        attackDelay = 1f / attackRate;
        ManageAttack();
    }

    private void ManageAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > playerDetectionRadius)
        {
            enemyMovement.FollowPlayer(player);
            return;
        }
        attack.TryAttack(player);
    }


}
