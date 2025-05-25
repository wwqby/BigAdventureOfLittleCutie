using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IPlayerStatsListener
{
    [Header("components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [Header("Elements")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float lifeSteel;
    [SerializeField] private float armor;
    [SerializeField] private float dodge;
    [SerializeField] private float healthRecoverySpeed;
    [SerializeField] private float healthRecoveryTimer;
    [Header("Actions")]
    public static Action<Vector2> OnAttackDodged;

    void OnEnable()
    {
        BaseEnemy.OnTakeDamage += OnAttackLifeSteel;
    }

    void OnDisable()
    {
        BaseEnemy.OnTakeDamage -= OnAttackLifeSteel;
    }


    void Update()
    {
        if (GameManager.instance.GameState != GameState.GAME)
        {
            return;
        }
        ManageHealthRecovery();
    }

    private void ManageHealthRecovery()
    {
        if (health >= maxHealth)
        {
            return;
        }
        if (healthRecoverySpeed <= 0)
        {
            return;
        }
        healthRecoveryTimer += Time.deltaTime;
        if (healthRecoveryTimer >= 1f)
        {
            healthRecoveryTimer -= 1f;
            health += Math.Clamp(healthRecoverySpeed, 0, maxHealth - health);
            UpdateHealthBar();
        }
    }

    private void OnAttackLifeSteel(float damage, bool isCritical, Vector2 pos)
    {
        if (health > maxHealth)
            return;

        float healthSteel = damage * lifeSteel;
        healthSteel = Mathf.Clamp(healthSteel, 0, maxHealth - health);
        health += healthSteel;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (ShouldDodge())
        {
            OnAttackDodged?.Invoke(transform.position);
            return;
        }
        float realDamage = damage * Math.Clamp(1 - armor / 1000, 0.1f, 100);
        health -= math.min(realDamage, health);
        UpdateHealthBar();
        if (health <= 0)
        {
            PassAway();
        }
    }

    private bool ShouldDodge()
    {
        return Random.Range(0f, 100f) <= Math.Clamp(dodge, 0f, 90f);
    }

    private void UpdateHealthBar()
    {
        float healthRate = health / maxHealth;
        healthBar.value = healthRate;
        healthText.text = (int)health + " / " + maxHealth;
    }
    private void PassAway()
    {
        GameManager.instance.SetGameState(GameState.GAMEOVER);
    }



    public void OnPlayerStatsChanged(PlayerStatsManager playerStatsManager)
    {
        float newMaxHealth = playerStatsManager.GetValue(Stats.MaxHealth);
        maxHealth = Math.Max(newMaxHealth, 1);
        health = maxHealth;//TODO 
        healthRecoverySpeed = playerStatsManager.GetValue(Stats.HealthRecoverySpeed)/100f;
        armor = playerStatsManager.GetValue(Stats.Armor);
        dodge = playerStatsManager.GetValue(Stats.Dodge);
        lifeSteel = playerStatsManager.GetValue(Stats.LifeSteel) / 100f;

        UpdateHealthBar();
    }
}
