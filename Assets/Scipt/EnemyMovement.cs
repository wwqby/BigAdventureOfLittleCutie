using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Spawn Related")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spawnerIndicator;
    [SerializeField] private float scaleTime;
    [SerializeField] private Player player;
    private bool hasSpawned;

    [Header("Attack")]
    [SerializeField] private int damage;
    private float attackDelay;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackTimer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float playerDetectionRadius;

    [Header("Effects")]
    [SerializeField] private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            Destroy(gameObject);
        }
        //hide the enemy
        spriteRenderer.enabled = false;
        //show the indicator
        spawnerIndicator.enabled = true;

        //scale up and down the indicator

        Vector3 targetScale = spawnerIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnerIndicator.gameObject, targetScale, scaleTime)
        .setLoopPingPong(4)
        .setOnComplete(SpawnCompleted);
        //prevent the enemy follow and attack during the update()
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay = 1f/attackRate;
        if (!hasSpawned)
        {
            return;
        }
        FollowPlayer();
        if (attackTimer >= attackDelay)
        {
            TryAttack();
        }
        else
        {
            Wait();
        }
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }

    private void SpawnCompleted()
    {
        //show the enemy
        spriteRenderer.enabled = true;
        //hide the indicator
        spawnerIndicator.enabled = false;
        hasSpawned = true;
    }



    private void TryAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < playerDetectionRadius)
        {
            // PassAway();
            Debug.Log("Attacking");
            attackTimer = 0;
        }
    }

    private void PassAway()
    {
        if (particle != null)
        {
            particle.transform.parent = null;
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
