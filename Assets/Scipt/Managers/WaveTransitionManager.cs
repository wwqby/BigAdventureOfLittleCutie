using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private UpgradeContainerBtn[] btnList;
    [SerializeField] private PlayerStatsManager playerStatsManager;


    public void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.WAVETRANSITION)
        {
            ConfigUpgradeButtons();
        }
    }

    [NaughtyAttributes.Button]
    public void ConfigUpgradeButtons()
    {
        foreach (UpgradeContainerBtn btn in btnList)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stats)).Length);
            Stats statsString = (Stats)Enum.GetValues(typeof(Stats)).GetValue(randomIndex);
            int value = Random.Range(1, 20);
            Action action = GetActionByStats(statsString, out string valueString);
            btn.ConfigUpgradeBtn(null, statsString, valueString);
            btn.Button.onClick.AddListener(() => action?.Invoke());
            btn.Button.onClick.AddListener(() => GameManager.instance.WaveCompleteCallback());
        }
    }

    private Action GetActionByStats(Stats stats, out string valueString)
    {
        float value = Random.Range(1, 10);
        valueString = $"+{value}%";
        Action action = () => playerStatsManager.UpgradeStats(stats, value);
        switch (stats)
        {
            case Stats.Attack:
                valueString = $"+{value}";
                break;
            case Stats.AttackSpeed:
                break;
            case Stats.CriticalChance:
                break;
            case Stats.CriticalPercent:
                break;
            case Stats.MoveSpeed:
            case Stats.MoveSpeedPercent:
                break;
            case Stats.MaxHealth:
                valueString = $"+{value}";
                break;
            case Stats.Range:
                break;
            case Stats.HealthRecoverySpeed:
                valueString = $"+{value}";
                break;
            case Stats.Armor:
                valueString = $"+{value}";
                break;
            case Stats.Luck:
                valueString = $"+{value}";
                break;
            case Stats.Dodge:
                break;
            case Stats.LifeSteel:
                break;
            default:
                action = () => Debug.LogWarning("unknown stats:" + stats.ToString());
                break;
        }
        return action;
    }

}
