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
    private int damage;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (!ReleaseBullet())
            {
                return;
            }
            LeanTween.cancel(gameObject);
            enemy.TakeDamage(damage);
        }
    }

    public void Shoot(Vector2 from, Vector2 direction, int damage)
    {
        this.damage = damage;
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
        if (!gameObject.activeSelf)
        {
            return false;//说明已经被其他碰撞销毁
        }
        PlayerBulletManager.bulletPool.Release(this);
        return true;
    }
}
