using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("components")]
    private EnemyMovement enemyMovement;

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
    [SerializeField] private float playerDetectionRadius;

    [Header("Effects")]
    [SerializeField] private ParticleSystem particle;

    [Header("Debug")]
    [SerializeField] private bool gismos;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            Destroy(gameObject);
        }
        SetSpriteRendererVisible(false);
        Vector3 targetScale = spawnerIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnerIndicator.gameObject, targetScale, scaleTime)
        .setLoopPingPong(4)
        .setOnComplete(SpawnCompleted);
    }

    private void SetSpriteRendererVisible(bool visible = true)
    {
        spriteRenderer.enabled = visible;
        spawnerIndicator.enabled = !visible;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            return;
        }
        enemyMovement.StorePlayer(player);
        attackDelay = 1f / attackRate;
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
        SetSpriteRendererVisible();
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



    void OnDrawGizmosSelected()
    {
        if (!gismos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
