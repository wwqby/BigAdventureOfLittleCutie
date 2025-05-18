using System;
using UnityEngine;
public class RangeWeapon : BaseWeapon
{

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idel:
                AutoAim();
                break;
            case State.Attack:
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
            targetVector = (enemy.CenterPoint - (Vector2)transform.position).normalized;
            transform.up = targetVector;
            ManageAttack();
            return;
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
        ShootBullet();
    }

    private void ShootBullet()
    {
        int damage = GetDamage(out bool isCritical);
        CottonCandyBullet cottonCandyBullet = PlayerBulletManager.bulletPool.Get();
        cottonCandyBullet.Shoot(
            hitDetection.position,
            transform.up.normalized,
            damage,isCritical);
        state = State.Idel;
    }



    #endregion


}
