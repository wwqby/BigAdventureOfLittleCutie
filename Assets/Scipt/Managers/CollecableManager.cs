using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CollecableManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;
    [SerializeField] private Chest chestPrefab;
    [SerializeField][Range(0, 100)] private int spawnCashChance;
    [SerializeField][Range(0, 100)] private int spawnChestChance;
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
        bool shouldSpawnCash = Random.Range(0, 100) < spawnCashChance;
        BaseCollecable collecable = shouldSpawnCash ? cashPool.Get() : candyPool.Get();
        collecable.transform.position = postion;
        TrySpawnChest(postion);
    }

    private void TrySpawnChest(Vector2 postion)
    {
        bool shouldSpawnChest = Random.Range(0, 100) < spawnChestChance;
        if (!shouldSpawnChest)
        {
            return;
        }
        Instantiate(chestPrefab, postion, Quaternion.identity,transform);
    }
    #endregion
}
