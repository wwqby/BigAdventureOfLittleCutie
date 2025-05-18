using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private Cash cashPrefab;

    void Awake()
    {
        BaseEnemy.onDie += DropCandy;
    }

    void OnDestroy()
    {
        BaseEnemy.onDie -= DropCandy;
    }
    public void DropCandy(Vector2 postion)
    {
        bool shouldDropCandy = Random.Range(0, 100) < 60;
        GameObject prefab = shouldDropCandy ? candyPrefab.gameObject : cashPrefab.gameObject;
        Instantiate(prefab, postion, Quaternion.identity);
    }
}
