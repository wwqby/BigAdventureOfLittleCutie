using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [Header("Pooling")]
    private ObjectPool<BaseCollecable> candyPool;
    private ObjectPool<BaseCollecable> cashPool;

    void Awake()
    {
        candyPool = new ObjectPool<BaseCollecable>(
            () => Instantiate(candyPrefab, transform),
            (collecable) => collecable.gameObject.SetActive(true),
            (collecable) => collecable.gameObject.SetActive(false),
            (collecable) => Destroy(collecable.gameObject)
        );
        cashPool = new ObjectPool<BaseCollecable>(
            () => Instantiate(cashPrefab, transform),
            (collecable) => collecable.gameObject.SetActive(true),
            (collecable) => collecable.gameObject.SetActive(false),
            (collecable) => Destroy(collecable.gameObject)
        );
        BaseEnemy.onDie += DropCandy;
        Candy.OnCollected += ReleaseCandy;
        Cash.OnCollected += ReleaseCash;
    }

    void OnDestroy()
    {
        BaseEnemy.onDie -= DropCandy;
        Candy.OnCollected -= ReleaseCandy;
        Cash.OnCollected -= ReleaseCash;
    }

    #region Actions
    private void ReleaseCash(BaseCollecable collecable)
    {
        cashPool.Release(collecable);
    }

    private void ReleaseCandy(BaseCollecable collecable)
    {
        candyPool.Release(collecable);
    }
    public void DropCandy(Vector2 postion)
    {
        bool shouldDropCandy = Random.Range(0, 100) < 60;
        BaseCollecable collecable = shouldDropCandy ? candyPool.Get() : cashPool.Get();
        collecable.transform.position = postion;
    }
    #endregion
}
