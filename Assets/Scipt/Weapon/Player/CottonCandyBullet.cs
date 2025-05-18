using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CottonCandyBullet : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rig;
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Enemy target;
    [SerializeField] private bool isCritical;
    private int damage;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null)
        {
            return;
        }
        // if (collision.gameObject.TryGetComponent(out Enemy enemy))
        if (IsInLayer(collision.gameObject.layer, LayerMask))
        {

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage,isCritical);
            target = enemy;
            LeanTween.cancel(gameObject);
            ReleaseBullet();
        }
    }

    private bool IsInLayer(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask.value) != 0;
    }

    public void Shoot(Vector2 from, Vector2 direction, int damage, bool isCritical)
    {
        this.damage = damage;
        this.isCritical = isCritical;
        transform.position = from;
        transform.up = direction;
        rig.velocity = direction * moveSpeed;
        LeanTween.delayedCall(gameObject, 2, () =>
        {
            ReleaseBullet();
        });

    }

    private bool ReleaseBullet()
    {
        target = null;
        isCritical = false;
        if (!gameObject.activeSelf)
        {
            return false;//说明已经被其他碰撞销毁
        }
        PlayerBulletManager.bulletPool.Release(this);
        return true;
    }
}
