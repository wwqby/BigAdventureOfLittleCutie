using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(RengedEnemyAttack))]
public class RengedEnemy : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private RengedEnemyAttack attack;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private Collider2D collider2D;

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
    [Header("Actions")]
    public static Action<int, Vector2> OnTakeDamage;

    [Header("Debug")]
    [SerializeField] private bool gismos;

    void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemyMovement = GetComponent<EnemyMovement>();
        attack = GetComponent<RengedEnemyAttack>();
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


    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            return;
        }
        attackDelay = 1f / attackRate;
        ManageAttack();
    }

    private void ManageAttack()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > playerDetectionRadius)
        {
            enemyMovement.FollowPlayer(player);
            return;
        }
        attack.TryAttack(player);
    }

    private void SetSpriteRendererVisible(bool visible = true)
    {
        spriteRenderer.enabled = visible;
        spawnerIndicator.enabled = !visible;
    }


    private void SpawnCompleted()
    {
        SetSpriteRendererVisible();
        hasSpawned = true;
        collider2D.enabled = true;
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
        OnTakeDamage?.Invoke(damage, transform.position);
    }

    void OnDrawGizmosSelected()
    {
        if (!gismos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
