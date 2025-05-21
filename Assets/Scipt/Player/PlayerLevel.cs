using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLevel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider levelBar;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Settings")]
    [SerializeField] private int candyCount;
    [SerializeField] private int level;
    [SerializeField] private int currentXp;
    [SerializeField] private int requirXp;
    [SerializeField] private int levelUpInWave;
    public static PlayerLevel instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void OnEnable()
    {
        Candy.OnCollected += LevelGain;
        UpdateLevel();
    }

    void OnDisable()
    {
        Candy.OnCollected -= LevelGain;
    }

    void Start()
    {
        UpdateRequiredXp();
    }


    private void LevelGain(Candy candy)
    {
        candyCount++;
        currentXp++;
        if (currentXp >= requirXp)
        {
            level++;
            currentXp -= requirXp;
            levelUpInWave++;
            UpdateRequiredXp();
        }
        UpdateLevel();
    }

    private void UpdateRequiredXp()
    {
        requirXp = (level + 1) * 5;
    }
    private void UpdateLevel()
    {
        levelBar.value = currentXp / (float)requirXp;
        levelText.text = "Lvl " + level;
    }

    public bool HasLevelUpInWave()
    {
        if (levelUpInWave > 0)
        {
            levelUpInWave--;
            return true;
        }
        return false;
    }
}
