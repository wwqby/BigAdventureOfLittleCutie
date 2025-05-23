using System;
using UnityEngine;

public class Candy : BaseCollecable
{

    [Header("Action")]
    public static Action<Candy> OnCollected;

    protected override void Collected()
    {
        OnCollected?.Invoke(this);
    }
}
