using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RengedEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private EnemyBullet bulletPrefab;

    [Header("Attack")]
    [SerializeField] private int damage;
    private float attackDelay;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackTimer;
    [SerializeField] private Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1 / attackRate;
        attackTimer = attackDelay;
    }



    public void TryAttack(Player player)
    {
        attackDelay = 1 / attackRate;
        if (attackTimer >= attackDelay)//计算攻击间隔
        {
            Shoot(player);//攻击
            attackTimer = 0;
        }
        attackTimer += Time.deltaTime;
    }

    private void Shoot(Player player)
    {
        Vector2 direction = (player.GetCenterPoint() - (Vector2)shootPoint.position).normalized;
        EnemyBullet bullet = EnemyBulletManager.pool.Get();
        bullet.Shoot(shootPoint.position,direction, damage);
    }



}
