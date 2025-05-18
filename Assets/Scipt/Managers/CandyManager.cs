using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] private Candy candyPrefab;

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
        Candy candyInstance = Instantiate(candyPrefab, postion, Quaternion.identity);
    }
}
