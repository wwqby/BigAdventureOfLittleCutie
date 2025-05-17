using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MeleeEnemy : BaseEnemy
{







    // Update is called once per frame
    void Update()
    {
        if (!CanAttack())
        {
            return;
        }
        attackDelay = 1f / attackRate;
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
        enemyMovement.FollowPlayer(player);
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < playerDetectionRadius)
        {
            attackTimer = 0;
            player.TakeDamage(damage);
        }
    }

}
