using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    GameObject gameObject { get; }
    Vector2 CenterPoint { get; }

    void TakeDamage(int damage);
}
