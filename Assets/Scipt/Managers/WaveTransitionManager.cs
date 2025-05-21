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
            btn.ConfigUpgradeBtn(null, attr, value);
            Action action = GetActionByAttr(attr);
            btn.Button.onClick.AddListener(() => action?.Invoke());
            btn.Button.onClick.AddListener(() => GameManager.instance.WaveCompleteCallback());
        }
    }
    //TODO 添加属性
    private Action GetActionByAttr(PlayerAttr attr)
    {
        switch (attr)
        {
            case PlayerAttr.Attack:
                return () => Debug.Log("attack");
            case PlayerAttr.AttackSpeed:
                return () => Debug.Log("AttackSpeed");
            case PlayerAttr.CriticalChance:
                return () => Debug.Log("CriticalChance");
            case PlayerAttr.CriticalPercent:
                return () => Debug.Log("CriticalPercent");
            default:
                return () => Debug.Log("default:" + attr.ToString());
        }
    }

}
