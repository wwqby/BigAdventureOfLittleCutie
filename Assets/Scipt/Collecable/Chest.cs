using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ICollecable
{

    [Header("Action")]
    public static Action<Chest> OnCollected;
    
    public void Collect(Player player)
    {
        Destroy(gameObject);
    }
}
