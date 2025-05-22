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
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(PlayerAttr)).Length);
            PlayerAttr attr = (PlayerAttr)Enum.GetValues(typeof(PlayerAttr)).GetValue(randomIndex);
            int value = Random.Range(1, 20);
            Action action = GetActionByAttr(attr,out  string valueString);
            btn.ConfigUpgradeBtn(null, attr, valueString);
            btn.Button.onClick.AddListener(() => action?.Invoke());
            btn.Button.onClick.AddListener(() => GameManager.instance.WaveCompleteCallback());
        }
    }
    
    private Action GetActionByAttr(PlayerAttr attr, out string valueString)
    {
        float value = Random.Range(1, 10);
        valueString = $"+{value}%";
        Action action = null;
        switch (attr)
        {
            case PlayerAttr.Attack:
                valueString = $"+{value}";
                action = () => Debug.Log("attack");
                break;
            case PlayerAttr.AttackSpeed:
                action = () => Debug.Log("AttackSpeed");
                break;
            case PlayerAttr.CriticalChance:
                action = () => Debug.Log("CriticalChance");
                break;
            case PlayerAttr.CriticalPercent:
                action = () => Debug.Log("CriticalPercent");
                break;
            case PlayerAttr.MoveSpeed:
                action = () => Debug.Log("MoveSpeed");
                break;
            case PlayerAttr.MaxHealth:
                valueString = $"+{value}";
                action = () => Debug.Log("MaxHealth");
                break;
            case PlayerAttr.Range:
                action = () => Debug.Log("Range");
                break;
            case PlayerAttr.HealthRecoverySpeed:
                valueString = $"+{value}";
                action = () => Debug.Log("HealthRecoverySpeed");
                break;
            case PlayerAttr.Armor:
                valueString = $"+{value}";
                action = () => Debug.Log("Armor");
                break;
            case PlayerAttr.Luck:
                valueString = $"+{value}";
                action = () => Debug.Log("Luck");
                break;
            case PlayerAttr.Dodge:
                action = () => Debug.Log("Dodge");
                break;
            case PlayerAttr.LifeSteel:
                action = () => Debug.Log("LifeSteel");
                break;
            default:
                return () => Debug.Log("default:" + attr.ToString());
        }
        return action;
    }

}
