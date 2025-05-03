using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Player player;


    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0;


    // Update is called once per frame
    void Update()
    {
        if (player != null)
            FollowPlayer();
    }


    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

}
