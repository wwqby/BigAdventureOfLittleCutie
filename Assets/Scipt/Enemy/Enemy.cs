using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private TextMeshPro healthText;

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

    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [Header("Effects")]
    [SerializeField] private ParticleSystem particle;

    [Header("Debug")]
    [SerializeField] private bool gismos;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Start()
    {

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

        health = maxHealth;
        healthText.text = health.ToString();
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
            attackTimer = 0;
            player.TakeDamage(damage);
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


    public void TakeDamage(int damage)
    {
        int realHealth = Mathf.Min(damage, health);
        health -= realHealth;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            PassAway();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!gismos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
