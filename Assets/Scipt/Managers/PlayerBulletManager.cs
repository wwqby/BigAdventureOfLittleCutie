using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CottonCandyBullet prefab;
    public static ObjectPool<CottonCandyBullet> bulletPool;

    void Awake()
    {
        bulletPool = new ObjectPool<CottonCandyBullet>(CreateFun, ActionOnGet, ActionOnRealse, ActionOnDestroy);
    }


    #region  BulletPool
    private void ActionOnDestroy(CottonCandyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ActionOnRealse(CottonCandyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnGet(CottonCandyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private CottonCandyBullet CreateFun()
    {
        return Instantiate(prefab, transform);
    }

    #endregion


}
