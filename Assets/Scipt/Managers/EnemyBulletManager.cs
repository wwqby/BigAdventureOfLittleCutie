using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletManager : MonoBehaviour
{

    [SerializeField] private EnemyBullet bulletPrefab;
    public static ObjectPool<EnemyBullet> pool;
    void Awake()
    {
        pool = new ObjectPool<EnemyBullet>(
            CreatePool(),
            ActionOnGet(),
            ActionOnRelease(),
            ActionOnDestroy()
        );
    }

    private Action<EnemyBullet> ActionOnDestroy()
    {
        return (bullet) =>
        {
            Destroy(bullet.gameObject);
        };
    }

    private Action<EnemyBullet> ActionOnGet()
    {
        return (bullet) =>
        {
            bullet.gameObject.SetActive(true);
        };
    }

    private Action<EnemyBullet> ActionOnRelease()
    {
        return (bullet) =>
        {
            bullet.gameObject.SetActive(false);
        };
    }

    private Func<EnemyBullet> CreatePool()
    {
        return () => Instantiate(bulletPrefab, transform);
    }
}
