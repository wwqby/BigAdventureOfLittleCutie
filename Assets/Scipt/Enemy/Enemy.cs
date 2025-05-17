using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Enemy
{
    GameObject gameObject { get; }

    void TakeDamage(int damage);
}
