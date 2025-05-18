using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDitectation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CircleCollider2D playerCollider;

    // void FixedUpdate()
    // {
    //     Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(playerCollider.gameObject.transform.position, playerCollider.radius);
    //     foreach (Collider2D collision in collider2Ds)
    //     {
    //         if (collision.TryGetComponent(out Candy candy))
    //         {
    //             Destroy(candy.gameObject);
    //         }
    //     }
    // }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Candy candy))
        {
            if (!collision.IsTouching(playerCollider))
            {
                return;
            }
            Destroy(candy.gameObject);
        }
    }
}
