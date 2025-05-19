using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ICollecable
{

    [Header("Action")]
    public static Action<Chest> OnCollected;
    [Header("Settings")]
    [SerializeField] private bool isCollected;

    void OnEnable()
    {
        isCollected = false;
    }

    public void Collect(Player player)
    {
        if (isCollected)
        {
            return;
        }
        isCollected = true;
        Destroy(gameObject);

    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
