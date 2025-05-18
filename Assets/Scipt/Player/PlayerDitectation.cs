using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDitectation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CircleCollider2D collecableCollider;

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
        if (collision.TryGetComponent(out ICollecable collecable))
        {
            if (!collision.IsTouching(collecableCollider))
            {
                return;
            }
            collecable.Collect(FindAnyObjectByType<Player>());
        }
    }
}
