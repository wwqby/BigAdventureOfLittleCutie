using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [Header("Elements")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player took " + damage + " damage");
        int realDamage= math.min(damage, health);
        health -= realDamage;
        UpdateHealthBar();
        if (health <= 0){
            PassAway();
        }
    }

    private void UpdateHealthBar()
    {
        float healthRate = (float)health / maxHealth;
        healthBar.value = healthRate;
        healthText.text = health + " / " + maxHealth;
    }
    private void PassAway()
    {
        // Debug.Log("Player died");
       GameManager.instance.SetGameState(GameState.GAMEOVER);
    }
}
