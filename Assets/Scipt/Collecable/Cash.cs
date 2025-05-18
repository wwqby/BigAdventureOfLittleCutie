using System;
using UnityEngine;

public class Cash : BaseCollecable
{

   [Header("Action")]
   public static Action<BaseCollecable> OnCollected;

    protected override void Collected()
    {
        OnCollected?.Invoke(this);
    }
}
