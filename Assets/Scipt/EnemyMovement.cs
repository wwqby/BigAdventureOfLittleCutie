using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    [Header("Movement")]
    private float moveSpeed = 0;
    [SerializeField]
    private float playerDetectionRadius;
    [Header("Effects")]
    [SerializeField]
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        player  = FindAnyObjectByType<Player>();
        if (player == null){
            Debug.LogWarning("Player not found!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < playerDetectionRadius){
            PassAway();
        }
    }

    private void PassAway()
    {
        if(particle!=null){
            particle.transform.parent = null ;
            particle.Play();
        }
        Destroy(gameObject);
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
