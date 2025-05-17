using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public abstract class BaseEnemy : MonoBehaviour, Enemy
{

    [Header("components")]
    [SerializeField] protected EnemyMovement enemyMovement;
    [SerializeField] protected TextMeshPro healthText;
    [SerializeField] protected Collider2D collider2D;

    [Header("Spawn Related")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected SpriteRenderer spawnerIndicator;
    [SerializeField] protected float scaleTime;
    [SerializeField] protected Player player;
    protected bool hasSpawned;

    [Header("Attack")]
    [SerializeField] protected int damage;
    protected float attackDelay;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected float playerDetectionRadius;


    [Header("Health")]
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [Header("Effects")]
    [SerializeField] protected ParticleSystem particle;
    [Header("Actions")]
    public static Action<int, Vector2> OnTakeDamage;

    [Header("Debug")]
    [SerializeField] protected bool gismos;



    public void TakeDamage(int damage)
    {
        int realHealth = Mathf.Min(damage, health);
        health -= realHealth;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            PassAway();
        }
        OnTakeDamage?.Invoke(damage,transform.position);
    }


    protected virtual void Awake()
    {
        player = FindAnyObjectByType<Player>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    protected virtual void Start()
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



    protected void SetSpriteRendererVisible(bool visible = true)
    {
        spriteRenderer.enabled = visible;
        spawnerIndicator.enabled = !visible;
    }

    protected void SpawnCompleted()
    {
        SetSpriteRendererVisible();
        hasSpawned = true;
        collider2D.enabled = true;
    }

    protected bool CanAttack(){
        return hasSpawned;
    }

    protected void PassAway()
    {
        if (particle != null)
        {
            particle.transform.parent = null;
            particle.Play();
        }
        Destroy(gameObject);
    }



    protected void OnDrawGizmosSelected()
    {
        if (!gismos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
